using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;

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

            var all = await _categoryRepository.GetAllAsync();
            if (all.Any(c => c.Namn.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new ValidationException($"Category '{name}' already exists.");

            var cat = new Kategori { Namn = name.Trim() };

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

            await _connector.RunTransactionAsync(async session =>
            {
                await _categoryRepository.UpdateCategoryNameAsync(session, categoryId, newName.Trim());
            });
        }

        public async Task DeleteCategoryAsync(string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                throw new ValidationException("Category ID cannot be empty.");

            await _connector.RunTransactionAsync(async session =>
            {
                await _categoryRepository.DeleteCategoryAsync(session, categoryId);
            });
        }
    }
}
