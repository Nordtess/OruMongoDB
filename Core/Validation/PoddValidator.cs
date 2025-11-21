using System;
using System.ComponentModel.DataAnnotations;

namespace OruMongoDB.Core.Validation
{
    public static class PoddValidator
    {
        public static void ValidateRssUrl(string rssUrl)
        {
            if (string.IsNullOrWhiteSpace(rssUrl))
                throw new ValidationException("RSS-URL får inte vara tom.");

            if (!Uri.TryCreate(rssUrl, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                throw new ValidationException("RSS-URL är inte giltig (måste vara http eller https).");
            }
        }

        public static void ValidatePoddNamn(string namn)
        {
            if (string.IsNullOrWhiteSpace(namn))
                throw new ValidationException("Du måste ange ett namn på poddflödet.");
        }
    }
}
