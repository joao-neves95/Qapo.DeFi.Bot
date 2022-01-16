using System.Threading.Tasks;

namespace Qapo.DeFi.Bot.Core.Interfaces.Services
{
    public interface IGasPriceService
    {
        Task<float> GetStandardGasPrice(int chainId);
    }
}
