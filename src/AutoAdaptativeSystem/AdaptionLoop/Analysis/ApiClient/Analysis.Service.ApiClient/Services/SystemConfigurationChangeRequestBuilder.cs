namespace Analysis.Service.ApiClient.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using Analysis.Service.ApiClient.Model;

public class SystemConfigurationChangeRequestBuilder
{
    private List<SymptomDTO> _symptoms;

    private List<ServiceConfigurationBuilder> _systemConfiguration;

    private SystemConfigurationChangeRequestBuilder()
    {
        _symptoms = new List<SymptomDTO>();
        _systemConfiguration = new List<ServiceConfigurationBuilder>();
    }

    internal static SystemConfigurationChangeRequestBuilder Configure()
    {
        return new SystemConfigurationChangeRequestBuilder();
    }

    public SystemConfigurationChangeRequestBuilder ForSymptom(string name, string value = "true")
    {
        _symptoms.Add(new SymptomDTO(name, value));

        return this;
    }

    public SystemConfigurationChangeRequestBuilder WithService(string serviceName, Action<ServiceConfigurationBuilder> serviceConfiguration)
    {
        var serviceConfigurationBuilder = ServiceConfigurationBuilder.Configure(serviceName);

        serviceConfiguration.Invoke(serviceConfigurationBuilder);

        _systemConfiguration.Add(serviceConfigurationBuilder);

        return this;
    }

    public SystemConfigurationChangeRequestDTO Build()
    {
        var serviceConfigurations = _systemConfiguration
            .Select(sc => sc.Build())
            .ToList();

        return new SystemConfigurationChangeRequestDTO(DateTime.UtcNow, _symptoms, serviceConfigurations);
    }
}
