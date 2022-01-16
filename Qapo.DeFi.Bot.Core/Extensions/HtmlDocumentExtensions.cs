using HtmlAgilityPack;

namespace Qapo.DeFi.Bot.Core.Extensions
{
    public static class HtmlDocumentExtensions
    {
        public static string GetElementInnerTextById(this HtmlDocument htmlDoc, string elementId)
        {
            return htmlDoc.GetElementbyId(elementId).InnerText;
        }
    }
}
