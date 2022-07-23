namespace Analysis.Service.ApiClient.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using Analysis.Service.ApiClient.Api;
using Analysis.Service.ApiClient.Model;

public class SystemService : ISystemService
{
    private readonly ISystemApi _systemApi;

    public SystemService(ISystemApi systemApi)
    {
        _systemApi = systemApi;
    }

    public async Task RequestConfigurationChange(Action<SystemConfigurationChangeRequestBuilder> configuration)
    {
        var changeRequestBuilder = SystemConfigurationChangeRequestBuilder.Configure();

        configuration.Invoke(changeRequestBuilder);

        var changeRequest = changeRequestBuilder.Build();

        Validate(changeRequest);

        await _systemApi.SystemRequestChangePostAsync(changeRequest, CancellationToken.None);
    }

    private void Validate(SystemConfigurationChangeRequestDTO changeRequest)
    {
        if (changeRequest.Symptoms.Count == 0)
        {
            throw new ArgumentException("At least one symptom must be specified");
        }

        if (changeRequest.ServiceConfiguration.Count == 0)
        {
            throw new ArgumentException("At least one service configuration must be specified.");
        }
    }
}
