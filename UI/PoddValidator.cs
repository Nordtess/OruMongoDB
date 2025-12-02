using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using OruMongoDB.Domain;

/*
 Summary:
 Validation utilities used by the UI layer to enforce preconditions before invoking
 operations that persist data in MongoDB Atlas via the business layer. These checks
 keep the WinForms app responsive and robust by failing fast with clear messages,
 complementing async calls, driver usage, and transactional operations upstream.
*/

namespace UI
{
    public static class PoddValidator
    {
        // Validates RSS URL format (non-empty, absolute, http/https)
        public static void ValidateRssUrl(string rssUrl)
        {
            if (string.IsNullOrWhiteSpace(rssUrl))
                throw new ValidationException("RSS URL must not be empty.");
            if (!Uri.TryCreate(rssUrl, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                throw new ValidationException("RSS URL is not valid (must start with http or https).");
        }

        // Validates feed display name (non-empty, length limit)
        public static void ValidateFeedName(string namn)
        {
            if (string.IsNullOrWhiteSpace(namn))
                throw new ValidationException("Feed name cannot be empty.");
            if (namn.Length > 200)
                throw new ValidationException("Feed name is too long (max 200 characters).");
        }

        // Ensures a feed is selected in the UI
        public static void EnsureFeedSelected(Poddflöden? feed)
        {
            if (feed == null)
                throw new ValidationException("Please select a podcast feed first.");
        }

        // Ensures a category is selected in the given context
        public static void EnsureCategorySelected(Kategori? category, string contextLabel)
        {
            if (category == null)
                throw new ValidationException($"Please select a category in '{contextLabel}'.");
        }

        // Guards against saving a feed twice
        public static void EnsureFeedNotAlreadySaved(Poddflöden feed)
        {
            if (feed.IsSaved)
                throw new ValidationException("This feed is already saved in the database.");
        }

        // Validates rename operation for a feed
        public static void EnsureFeedRenameValid(Poddflöden feed, string newName)
        {
            ValidateFeedName(newName);
            if (string.Equals(feed.displayName, newName, StringComparison.Ordinal))
                throw new ValidationException("The new feed name is the same as the current name.");
        }

        // Validates category name (non-empty, length limit)
        public static void ValidateCategoryName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Category name cannot be empty.");
            if (name.Length > 100)
                throw new ValidationException("Category name is too long (max 100 characters).");
        }

        // Validates rename operation for a category and prevents duplicates (case-insensitive)
        public static void EnsureCategoryRenameValid(Kategori category, string newName, IEnumerable<Kategori> allCategories)
        {
            ValidateCategoryName(newName);
            if (string.Equals(category.Namn, newName, StringComparison.Ordinal))
                throw new ValidationException("The new category name is the same as the current name.");
            if (allCategories.Any(c => c.Namn.Equals(newName, StringComparison.OrdinalIgnoreCase)))
                throw new ValidationException($"Category '{newName}' already exists.");
        }

        // Ensures category assignment is not redundant
        public static void EnsureCategoryAssignmentAllowed(Poddflöden feed, Kategori category)
        {
            if (feed.categoryId == category.Id)
                throw new ValidationException("Feed already has this category.");
        }

        // Ensures the operation has episodes to act on
        public static void EnsureEpisodesExist(IEnumerable<PoddAvsnitt> episodes)
        {
            if (episodes == null || !episodes.Any())
                throw new ValidationException("There are no episodes to operate on.");
        }

        // Ensures a feed currently has a category before attempting removal
        public static void EnsureFeedHasCategory(Poddflöden feed)
        {
            if (string.IsNullOrWhiteSpace(feed.categoryId))
                throw new ValidationException("Feed has no category to remove.");
        }
    }
}
