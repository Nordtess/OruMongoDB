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
 /// <summary>
 /// Application service for managing podcast categories (validation + transactional writes).
 /// </summary>
 public class CategoryService
 {
 private readonly ICategoryRepository _categoryRepository;
 private readonly MongoConnector _connector;

 public CategoryService(ICategoryRepository categoryRepository, MongoConnector connector)
 {
 _categoryRepository = categoryRepository;
 _connector = connector;
 }

 /// <summary>
 /// Returns all categories.
 /// </summary>
 public async Task<IEnumerable<Kategori>> GetAllCategoriesAsync() =>
 await _categoryRepository.GetAllAsync();

 /// <summary>
 /// Creates a new category if it does not already exist (case-insensitive).
 /// </summary>
 /// <param name="name">Category name.</param>
 /// <exception cref="ValidationException">Thrown when name is empty or already exists.</exception>
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

 /// <summary>
 /// Updates the name of an existing category.
 /// </summary>
 /// <param name="categoryId">The category identifier.</param>
 /// <param name="newName">The new category name.</param>
 /// <exception cref="ValidationException">Thrown when inputs are invalid.</exception>
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

 /// <summary>
 /// Deletes a category and removes the assignment from all feeds that reference it.
 /// </summary>
 /// <param name="categoryId">The category identifier.</param>
 /// <exception cref="ValidationException">Thrown when categoryId is empty.</exception>
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
