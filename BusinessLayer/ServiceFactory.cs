using OruMongoDB.Infrastructure;
using OruMongoDB.BusinessLayer.Rss;

/*
 Summary
 -------
 ServiceFactory centralizes construction of application services with their concrete
 MongoDB Atlas–backed repositories and supporting components. It ensures:
 - A single shared MongoConnector (singleton) is used per service creation.
 - Proper wiring of repositories and IRssParser implementation for dependency injection style usage.

 Notes
 - Keeps creation logic out of UI layer, aiding testability and adherence to interface requirements.
 - All returned services expose async APIs that will use MongoDB driver + transactions internally.
 - No state is cached here beyond MongoConnector (handled by its own singleton pattern).
*/

namespace OruMongoDB.BusinessLayer
{
    public static class ServiceFactory
    {
        /// <summary>
        /// Creates a podcast service with Mongo-backed repositories and an RSS parser.
        /// </summary>
        public static IPoddService CreatePoddService()
        {
            var connector = MongoConnector.Instance; // shared MongoDB Atlas connector
            var db = connector.GetDatabase(); // obtain database reference once
            var poddRepo = new PoddflodeRepository(db);
            var avsnittRepo = new PoddAvsnittRepository(db);
            var rssParser = new RssParser();
            return new PoddService(poddRepo, avsnittRepo, rssParser, connector);
        }

        /// <summary>
        /// Creates a category service for managing feed categories.
        /// </summary>
        public static CategoryService CreateCategoryService()
        {
            var connector = MongoConnector.Instance; // reuse singleton connector
            var db = connector.GetDatabase();
            var categoryRepo = new CategoryRepository(db);
            return new CategoryService(categoryRepo, connector);
        }
    }
}