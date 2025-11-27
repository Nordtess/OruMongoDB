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
 Provides the RSS parsing abstraction (IRssParser) and implementation (RssParser).
 RssParser:
 - Asynchronously loads an RSS/Atom feed from a URL.
 - Projects feed metadata to Poddflöden and items to PoddAvsnitt.
 - Strips HTML markup from episode descriptions via HtmlCleaner.
 Design notes:
 - Custom interface enables testability.
 - XmlReader created with Async=true; SyndicationFeed.Load is synchronous so it is executed inside Task.Run to avoid blocking the caller.
 - No persistence or transactions here; higher layers handle MongoDB Atlas storage and ACID transactions.
 - Exceptions bubble up for caller handling (UI/service layer) to maintain resilience.
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
                var rawDesc = item.Summary?.Text ?? item.Content?.ToString() ?? "No description available.";
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
    }
}