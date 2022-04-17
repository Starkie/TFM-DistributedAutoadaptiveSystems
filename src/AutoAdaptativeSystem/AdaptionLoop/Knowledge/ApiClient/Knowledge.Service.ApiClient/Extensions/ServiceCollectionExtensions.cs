namespace Microsoft.Extensions.DependencyInjection;

using Knowledge.Service.ApiClient.Api;
using Knowledge.Service.ApiClient.Configurations;
using Knowledge.Service.ApiClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKnowledgeServices(this IServiceCollection services, IConfiguration configuration)
    {
        var knowledgeConfiguration =
            configuration.BindOptions<KnowledgeServiceConfiguration>(KnowledgeServiceConfiguration.ConfigurationPath);

        services.AddScoped<IPropertyApi, PropertyApi>(_ => new PropertyApi(knowledgeConfiguration.ServiceUri));
        services.AddScoped<IPropertyService, PropertyService>();

        services.AddScoped<IServiceApi, ServiceApi>(_ => new ServiceApi(knowledgeConfiguration.ServiceUri));
        services.AddScoped<IConfigurationService, ConfigurationService>();

        return services;
    }
}
