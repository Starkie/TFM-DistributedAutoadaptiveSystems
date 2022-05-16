namespace Analysis.Service.ApiClient.Services;

using System;
using System.Threading.Tasks;

public interface ISystemService
{
    Task RequestChangeAsync(Action<SystemConfigurationChangeRequestBuilder> configuration);
}
