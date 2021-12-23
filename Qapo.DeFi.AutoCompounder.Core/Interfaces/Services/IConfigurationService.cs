using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Dto;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Services
{
    public interface IConfigurationService<TEntity>
        where TEntity : IAppConfig
    {
        Task<TEntity> GetConfig();
    }
}
