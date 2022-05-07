namespace Analysis.Service.ApiClient.Services;

using System.Collections.Generic;
using Analysis.Service.ApiClient.Model;

public class ServiceConfigurationBuilder
{
    private readonly string _serviceName;

    private bool _isDeployed;

    private List<ConfigurationProperty> _configurationProperties;

    private ServiceConfigurationBuilder(string serviceName)
    {
        _configurationProperties = new List<ConfigurationProperty>();
        _serviceName = serviceName;
    }

    internal static ServiceConfigurationBuilder Configure(string serviceName)
    {
        return new ServiceConfigurationBuilder(serviceName);
    }

    public ServiceConfigurationBuilder MustBePresent()
    {
        _isDeployed = true;

        return this;
    }

    public ServiceConfigurationBuilder MustNotBePresent()
    {
        _isDeployed = false;

        return this;
    }

    public ServiceConfigurationDTO Build()
    {
        return new ServiceConfigurationDTO(_serviceName, _isDeployed, _configurationProperties);
    }

    public ServiceConfigurationBuilder WithParameter(string name, string value)
    {
        _configurationProperties.Add(new ConfigurationProperty(name, value));

        return this;
    }
}
