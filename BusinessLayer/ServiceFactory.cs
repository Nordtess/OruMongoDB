using OruMongoDB.Infrastructure;
using OruMongoDB.BusinessLayer.Rss;

namespace OruMongoDB.BusinessLayer
{
    public static class ServiceFactory
    {
        public static IPoddService CreatePoddService()
        {
            var connector = MongoConnector.Instance;
            var db = connector.GetDatabase();
            var poddRepo = new PoddflodeRepository(db);
            var avsnittRepo = new PoddAvsnittRepository(db);
            var rssParser = new RssParser();
            return new PoddService(poddRepo, avsnittRepo, rssParser, connector);
        }

        public static CategoryService CreateCategoryService()
        {
            var connector = MongoConnector.Instance;
            var db = connector.GetDatabase();
            var categoryRepo = new CategoryRepository(db);
            return new CategoryService(categoryRepo, connector);
        }
    }
}