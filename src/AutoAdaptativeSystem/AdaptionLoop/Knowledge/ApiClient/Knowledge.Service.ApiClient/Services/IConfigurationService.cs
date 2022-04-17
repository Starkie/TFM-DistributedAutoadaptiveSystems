namespace Knowledge.Service.ApiClient.Services;

using System.Threading;
using System.Threading.Tasks;

public interface IConfigurationService
{
    Task<T> GetConfigurationKey<T>(string serviceName, string configurationName, CancellationToken cancellationToken = default);
}
