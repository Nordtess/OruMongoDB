using HtmlAgilityPack;
using System.Net;

/*
 Summary
 -------
 HtmlCleaner turns HTML into plain text for safe display/persistence:
 - Decodes HTML entities (&quot;, &amp;, etc.).
 - Removes all markup via HtmlAgilityPack.
 - Fast path if no '<' present (already plain) to reduce overhead.
 - Returns empty string for null/whitespace input.
*/

namespace OruMongoDB.Core.Helpers
{
    public static class HtmlCleaner
    {

        public static string ToPlainText(string html)
        {
            if (string.IsNullOrWhiteSpace(html)) return string.Empty;

            // Fast path: if input has no markup characters, just decode entities and trim.
            if (!html.Contains('<'))
                return WebUtility.HtmlDecode(html).Trim();

            // Decode entities first (handles cases like &lt;p&gt;...&lt;/p&gt;)
            var decoded = WebUtility.HtmlDecode(html);

            var doc = new HtmlDocument();
            doc.LoadHtml(decoded);

            // Extract inner text (HtmlAgilityPack already decodes some entities)
            var text = doc.DocumentNode.InnerText;

            // Final decode to catch any remaining encoded sequences
            text = WebUtility.HtmlDecode(text);

            return text.Trim();
        }
    }
}
