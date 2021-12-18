using System.Threading.Tasks;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Services
{
    public interface IConfigurationService
    {
        Task<T> GetConfig<T>();
    }
}
