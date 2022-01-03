using System.Threading.Tasks;

using Qapo.DeFi.Bot.Core.Interfaces.Dto;

namespace Qapo.DeFi.Bot.Core.Interfaces.Services
{
    public interface IConfigurationService<TEntity>
        where TEntity : IAppConfig
    {
        Task<TEntity> GetConfig();
    }
}
