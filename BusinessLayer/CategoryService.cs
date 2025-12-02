using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;

/*
 Summary
 -------
 CategoryService coordinates category management operations for the application:
 - Reads all categories asynchronously via ICategoryRepository.
 - Creates categories with case-insensitive uniqueness and trimmed names.
 - Renames categories.
 - Deletes categories and clears that category from any feeds that reference it.
 All write operations are executed inside MongoDB ACID transactions (compatible with
 MongoDB Atlas). Public APIs are asynchronous and rely on the official MongoDB .NET driver.
 */

namespace OruMongoDB.BusinessLayer
{

    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly MongoConnector _connector;

        public CategoryService(ICategoryRepository categoryRepository, MongoConnector connector)
        {
            _categoryRepository = categoryRepository;
            _connector = connector;
        }


        public async Task<IEnumerable<Kategori>> GetAllCategoriesAsync() =>
        await _categoryRepository.GetAllAsync();


        public async Task CreateCategoryAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Category name cannot be empty.");

            var normalized = name.Trim();

            // Ensure case-insensitive uniqueness before inserting
            var all = await _categoryRepository.GetAllAsync();
            if (all.Any(c => c.Namn.Equals(normalized, StringComparison.OrdinalIgnoreCase)))
                throw new ValidationException($"Category '{normalized}' already exists.");

            var cat = new Kategori { Namn = normalized };

            // Insert within a transaction (ACID)
            await _connector.RunTransactionAsync(async session =>
            {
                await _categoryRepository.InsertAsync(session, cat);
            });
        }


        public async Task UpdateCategoryNameAsync(string categoryId, string newName)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                throw new ValidationException("Category ID cannot be empty.");
            if (string.IsNullOrWhiteSpace(newName))
                throw new ValidationException("New category name cannot be empty.");

            var normalized = newName.Trim();

            // Update within a transaction (ACID)
            await _connector.RunTransactionAsync(async session =>
            {
                await _categoryRepository.UpdateCategoryNameAsync(session, categoryId, normalized);
            });
        }


        public async Task DeleteCategoryAsync(string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                throw new ValidationException("Category ID cannot be empty.");

            var feeds = _connector.GetCollection<Poddflöden>("Poddflöden");

            // Delete + unassign in a single transaction (keeps state consistent)
            await _connector.RunTransactionAsync(async session =>
            {
                await _categoryRepository.DeleteCategoryAsync(session, categoryId);

                // Unassign the deleted category from all feeds (set to "no category")
                var feedFilter = Builders<Poddflöden>.Filter.Eq(f => f.categoryId, categoryId);
                var feedUpdate = Builders<Poddflöden>.Update.Set(f => f.categoryId, string.Empty);
                await feeds.UpdateManyAsync(session, feedFilter, feedUpdate);
            });
        }
    }
}
