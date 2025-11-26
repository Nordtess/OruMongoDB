using OruMongoDB.Domain;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using OruMongoDB.Core.Helpers;

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
        public async Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchAndParseAsync(string url)
        {
            // Create async-capable XML reader for streaming network input.
            using var reader = XmlReader.Create(url, new XmlReaderSettings { Async = true });

            // SyndicationFeed.Load blocks; offload to background thread to keep async flow responsive.
            var feed = await Task.Run(() => SyndicationFeed.Load(reader));

            var poddflode = new Poddflöden
            {
                displayName = feed.Title?.Text ?? "Unknown podcast feed",
                rssUrl = url,
            };

            var avsnittList = new List<PoddAvsnitt>();

            // Map feed items to domain episodes, sanitizing description HTML.
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