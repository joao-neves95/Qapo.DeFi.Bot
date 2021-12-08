
namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Services
{
    public interface IConfigurationService
    {
        T GetConfig<T>();

        string GetValue(string key);

        T GetValueAs<T>(string key);
    }
}
