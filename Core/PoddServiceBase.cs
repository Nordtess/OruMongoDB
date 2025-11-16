using OruMongoDB.Domain;

namespace OruMongoDB.Core
{
    public abstract class PoddServiceBase
    {
        // Get podflow via RSS-URL 
        
        public abstract Task<(Poddflöden Flode, List<PoddAvsnitt> Avsnitt)>
            HamtaPoddflodeFranUrlAsync(string rssUrl);

        // Save podflow in register 
        public abstract Task SparaPoddflodeAsync(Poddflöden flode);

        // Save episode 
        public abstract Task SparaAvsnittAsync(List<PoddAvsnitt> avsnitt);

        // Debug method
        public virtual void SkrivUt(Poddflöden f, List<PoddAvsnitt> a)
        {
            Console.WriteLine($"Podd: {f.displayName} ({f.rssUrl})");
            Console.WriteLine("Avsnitt:");
            foreach (var av in a)
            {
                Console.WriteLine($" - {av.publishDate} : {av.title}");
            }
        }
    }
}
