using HtmlAgilityPack;
using System.Net;

namespace OruMongoDB.Core.Helpers
{
 public static class HtmlCleaner
 {
 public static string ToPlainText(string html)
 {
 if (string.IsNullOrWhiteSpace(html)) return string.Empty;

 // First decode entities in case the whole string is encoded
 var decoded = WebUtility.HtmlDecode(html);

 var doc = new HtmlDocument();
 doc.LoadHtml(decoded);

 // Extract inner text (HtmlAgilityPack also decodes common entities)
 var text = doc.DocumentNode.InnerText;

 // Safety: decode any remaining entities
 text = WebUtility.HtmlDecode(text);

 return text.Trim();
 }
 }
}
