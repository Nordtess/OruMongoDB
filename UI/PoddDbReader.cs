using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;
using System.Diagnostics;

namespace OruMongoDB.UI   
{
    public class PoddDbReader
    {
        public async Task<string> ReadAllAsync()
        {
            var sb = new StringBuilder();

            try
            {
                sb.AppendLine("=== Test: Läs data från MongoDB (G20) ===");


                var connector = MongoConnector.Instance;
                IMongoDatabase database = connector.GetDatabase();
                sb.AppendLine(" Ansluten till MongoDB och databasen 'G20'.");
                sb.AppendLine();

                
                IPoddflodeRepository poddflodeRepo = new PoddflodeRepository(database);
                IPoddAvsnittRepository poddAvsnittRepo = new PoddAvsnittRepository(database);

                
                sb.AppendLine("=== Alla Poddflöden (collection: 'Poddfloden') ===");

                IEnumerable<Poddflöden> allaFloden = await poddflodeRepo.GetAllAsync();
                int feedCount = 0;

                foreach (var feed in allaFloden)
                {
                    feedCount++;
                    sb.AppendLine($"[{feedCount}] Id: {feed.Id}");
                    sb.AppendLine($"    Namn:      {feed.displayName}");
                    sb.AppendLine($"    RSS-url:   {feed.rssUrl}");
                    sb.AppendLine($"    CategoryId:{feed.categoryId}");
                    sb.AppendLine();
                }

                if (feedCount == 0)
                {
                    sb.AppendLine("Inga poddflöden hittades i 'Poddfloden'.");
                }

                sb.AppendLine();
                sb.AppendLine("=== Alla Poddavsnitt (collection: 'PoddAvsnitt') ===");

                
                IEnumerable<PoddAvsnitt> allaAvsnitt = await poddAvsnittRepo.GetAllAsync();
                int epCount = 0;

                foreach (var avsnitt in allaAvsnitt)
                {
                    epCount++;
                    sb.AppendLine($"[{epCount}] Id: {avsnitt.Id}");
                    sb.AppendLine($"    FeedId:      {avsnitt.feedId}");
                    sb.AppendLine($"    Titel:       {avsnitt.title}");
                    sb.AppendLine($"    Publicerad:  {avsnitt.publishDate}");
                    sb.AppendLine($"    Länk:        {avsnitt.link}");
                    sb.AppendLine();
                }

                if (epCount == 0)
                {
                    sb.AppendLine("Inga poddavsnitt hittades i 'PoddAvsnitt'.");
                }

                sb.AppendLine();
                sb.AppendLine("=== Läsning klar. ===");
            }
            catch (Exception ex)
            {
                sb.AppendLine();
                sb.AppendLine(" FEL vid läsning från MongoDB.");
                sb.AppendLine($"   Meddelande: {ex.Message}");

                if (ex.InnerException != null)
                {
                    sb.AppendLine($"   Inner: {ex.InnerException.Message}");
                }

                
                Debug.WriteLine(ex.ToString());
            }

            return sb.ToString();
        }
    }
}
