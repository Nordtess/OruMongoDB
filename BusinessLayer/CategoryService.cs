using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OruMongoDB.BusinessLayer
{
    public class CategoryService
    {
        private readonly CategoryRepository _repo;
        public CategoryService(CategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task CreateCategoryAsync(string namn)
        {
            if (string.IsNullOrWhiteSpace(namn))
            {
                throw new Exception("Kategorinamn får inte vara tom.");
            }

            var kategori = new Kategori
            {
                Namn = namn
            };
            await _repo.AddAsync(kategori);
        }
        public async Task UpdateCategoryNameAsync(string categoryId, string newName)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                throw new Exception("Kategori-ID får inte vara tomt.");

            if (string.IsNullOrWhiteSpace(newName))
                throw new Exception("Nytt namn får inte vara tomt.");

            await _repo.UpdateCategoryNameAsync(categoryId, newName);
        }



        public Task<IEnumerable<Kategori>> GetAllCategoriesAsync()
        {
            return _repo.GetAllAsync();
        }
    }
}

    
    

