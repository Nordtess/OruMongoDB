using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using OruMongoDB.Domain;
using System.Collections.Generic;

namespace OruMongoDB.Core.Validation
{
    public static class PoddValidator
    {
        public static void ValidateRssUrl(string rssUrl)
        {
            if (string.IsNullOrWhiteSpace(rssUrl))
                throw new ValidationException("RSS URL must not be empty.");

            if (!Uri.TryCreate(rssUrl, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                throw new ValidationException("RSS URL is not valid (must start with http or https).");
        }

        public static void ValidateFeedName(string namn)
        {
            if (string.IsNullOrWhiteSpace(namn))
                throw new ValidationException("Feed name cannot be empty.");
            if (namn.Length > 200)
                throw new ValidationException("Feed name is too long (max 200 characters).");
        }

        public static void EnsureFeedSelected(Poddflöden? feed)
        {
            if (feed == null)
                throw new ValidationException("Please select a podcast feed first.");
        }

        public static void EnsureCategorySelected(Kategori? category, string contextLabel)
        {
            if (category == null)
                throw new ValidationException($"Please select a category in '{contextLabel}'.");
        }

        public static void EnsureFeedNotAlreadySaved(Poddflöden feed)
        {
            if (feed.IsSaved)
                throw new ValidationException("This feed is already saved in the database.");
        }

        public static void EnsureFeedRenameValid(Poddflöden feed, string newName)
        {
            ValidateFeedName(newName);
            if (string.Equals(feed.displayName, newName, StringComparison.Ordinal))
                throw new ValidationException("The new feed name is the same as the current name.");
        }

        public static void ValidateCategoryName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Category name cannot be empty.");
            if (name.Length > 100)
                throw new ValidationException("Category name is too long (max 100 characters).");
        }

        public static void EnsureCategoryRenameValid(Kategori category, string newName, IEnumerable<Kategori> allCategories)
        {
            ValidateCategoryName(newName);
            if (string.Equals(category.Namn, newName, StringComparison.Ordinal))
                throw new ValidationException("The new category name is the same as the current name.");
            if (allCategories.Any(c => c.Namn.Equals(newName, StringComparison.OrdinalIgnoreCase)))
                throw new ValidationException($"Category '{newName}' already exists.");
        }

        public static void EnsureCategoryAssignmentAllowed(Poddflöden feed, Kategori category)
        {
            if (feed.categoryId == category.Id)
                throw new ValidationException("Feed already has this category.");
        }

        
        public static void EnsureEpisodesExist(IEnumerable<PoddAvsnitt> episodes)
        {
            if (episodes == null || !episodes.Any())
                throw new ValidationException("There are no episodes to operate on.");
        }

        
        public static void EnsureFeedHasCategory(Poddflöden feed)
        {
            if (string.IsNullOrWhiteSpace(feed.categoryId))
                throw new ValidationException("Feed has no category to remove.");
        }
    }
}
