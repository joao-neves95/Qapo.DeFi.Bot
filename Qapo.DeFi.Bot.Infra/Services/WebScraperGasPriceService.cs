using System;
using System.Threading.Tasks;
using System.Net.Http;

using HtmlAgilityPack;

using Qapo.DeFi.Bot.Core.Interfaces.Services;
using Qapo.DeFi.Bot.Core.Constants;
using Qapo.DeFi.Bot.Core.Extensions;

namespace Qapo.DeFi.Bot.Infra.Services
{
    public class WebScraperGasPriceService : IGasPriceService
    {
        private readonly HttpClient _httpClient;

        public WebScraperGasPriceService()
        {
            this._httpClient = new HttpClient();
        }

        public async Task<float> GetStandardGasPrice(int chainId)
        {
            return chainId switch
            {
                ChainId.Polygon => await this.ScrapePolygonStandardGasPrice(),
                ChainId.Fantom => await this.ScrapeFantomStandardGasPrice(),

                _ => throw new TypeAccessException("Unknown type: " + Enum.GetName(typeof(ChainId), chainId))
            };
        }

        private async Task<HtmlDocument> GetHtmlDocument(string url)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(await (await this._httpClient.GetAsync(url)).Content.ReadAsStringAsync());

            return htmlDoc;
        }

        private async Task<float> ScrapePolygonStandardGasPrice()
        {
            HtmlDocument htmlDoc = await this.GetHtmlDocument(Urls.PolygonGasTracker);

            float standardGasPrice = float.Parse(htmlDoc.GetElementInnerTextById("standardgas").Replace(" Gwei", ""));

            return standardGasPrice;
        }

        private async Task<float> ScrapeFantomStandardGasPrice()
        {
            HtmlDocument htmlDoc = await this.GetHtmlDocument(Urls.FantomGasTracker);

            float standardGasPrice = float.Parse(htmlDoc.GetElementInnerTextById("standardgas").Replace(" Gwei", ""));

            return standardGasPrice;
        }
    }
}
