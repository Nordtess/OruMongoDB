using OruMongoDB.Domain;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using OruMongoDB.Core.Helpers;

namespace OruMongoDB.BusinessLayer.Rss
{
    public interface IRssParser
    {
        Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchAndParseAsync(string url);
    }

    public class RssParser : IRssParser
    {
        public async Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchAndParseAsync(string url)
        {
            using var reader = XmlReader.Create(url, new XmlReaderSettings { Async = true });

            var feed = await Task.Run(() => SyndicationFeed.Load(reader));

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