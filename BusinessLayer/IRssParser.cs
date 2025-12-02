using OruMongoDB.Domain;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using OruMongoDB.Core.Helpers;
using System.Net.Http;
using System.Net;

/*
 Summary
 -------
 Provides the RSS/Atom parsing abstraction (IRssParser) and implementation (RssParser).
 RssParser:
 - Asynchronously loads an RSS or Atom feed from a URL.
 - Projects feed metadata to Poddflöden and items to PoddAvsnitt.
 - Safely extracts text/HTML from TextSyndicationContent (avoids showing type names).
 - Strips HTML markup from episode descriptions via HtmlCleaner.
*/

namespace OruMongoDB.BusinessLayer.Rss
{
    public interface IRssParser
    {
        Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchAndParseAsync(string url);
    }

    public class RssParser : IRssParser
    {
        private static readonly HttpClient _http = new HttpClient();

        public async Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchAndParseAsync(string url)
        {
            using var response = await _http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // Preserve status for PoddService to map to ValidationException
                throw new HttpRequestException("Feed not found.", null, HttpStatusCode.NotFound);
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"HTTP error {(int)response.StatusCode} {response.StatusCode}.", null, response.StatusCode);
            }

            await using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = XmlReader.Create(stream, new XmlReaderSettings { Async = true });
            var feed = SyndicationFeed.Load(reader);

            var poddflode = new Poddflöden
            {
                displayName = feed.Title?.Text ?? "Unknown podcast feed",
                rssUrl = url,
            };

            var avsnittList = new List<PoddAvsnitt>();
            foreach (var item in feed.Items)
            {
                // Prefer Summary text; fall back to Content if Summary empty.
                string rawDesc = ExtractText(item) ?? "No description available.";
                var cleanDesc = HtmlCleaner.ToPlainText(rawDesc);
                avsnittList.Add(new PoddAvsnitt
                {
                    title = item.Title?.Text ?? "Unknown episode",
                    description = cleanDesc,
                    publishDate = item.PublishDate.DateTime.ToShortDateString(),
                    link = item.Links.Count > 0 ? item.Links[0].Uri.ToString() : string.Empty,
                });
            }

            return (poddflode, avsnittList);
        }

        private static string? ExtractText(SyndicationItem item)
        {
            // TextSyndicationContent covers Atom/RSS content types.
            if (item.Summary is TextSyndicationContent summaryContent)
            {
                var text = summaryContent.Text;
                if (!string.IsNullOrWhiteSpace(text)) return text;
            }

            if (item.Content is TextSyndicationContent contentText)
            {
                var text = contentText.Text;
                if (!string.IsNullOrWhiteSpace(text)) return text;
            }

            // Some Atom feeds store content in ElementExtensions.
            foreach (var ext in item.ElementExtensions)
            {
                try
                {
                    var val = ext.GetObject<System.Xml.XmlElement>()?.InnerText;
                    if (!string.IsNullOrWhiteSpace(val)) return val;
                }
                catch { /* ignore malformed extension */ }
            }

            // Fallback: attempt Summary?.Text or Content?.ToString() (avoid showing the type name when empty)
            return item.Summary?.Text ?? (item.Content as TextSyndicationContent)?.Text;
        }
    }
}