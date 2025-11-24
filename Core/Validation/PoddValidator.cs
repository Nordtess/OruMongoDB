using System;
using System.ComponentModel.DataAnnotations;
using OruMongoDB.Domain;

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
            {
                throw new ValidationException("RSS URL is not valid (must start with http or https).");
            }
        }

        public static void ValidatePoddNamn(string namn)
        {
            if (string.IsNullOrWhiteSpace(namn))
                throw new ValidationException("You must enter a name for the podcast feed.");
        }

        

        public static void ValidateCategoryName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Category name must not be empty.");
        }

        public static void EnsureFeedSelected(Poddflöden? feed)
        {
            if (feed == null)
                throw new ValidationException("Please select a podcast feed first.");
        }

        public static void EnsureCategorySelected(Kategori? category, string contextLabel)
        {
            if (category == null)
                throw new ValidationException($"Please select a category in '{contextLabel}' first.");
        }
    }
}
