namespace Analysis.Service.ApiClient.Extensions;

using Analysis.Service.ApiClient.Api;
using Analysis.Service.ApiClient.Configurations;
using Analysis.Service.ApiClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAnalysisServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPropertyApi, PropertyApi>(_ =>
        {
            var apiConfiguration =
                configuration.BindOptions<AnalysisServiceConfiguration>(AnalysisServiceConfiguration.ConfigurationPath);

            return new PropertyApi(apiConfiguration.ServiceUri);
        });

        services.AddScoped<IServiceApi, ServiceApi>(_ =>
        {
            var apiConfiguration =
                configuration.BindOptions<AnalysisServiceConfiguration>(AnalysisServiceConfiguration.ConfigurationPath);

            return new ServiceApi(apiConfiguration.ServiceUri);
        });

        services.AddScoped<ISystemApi, SystemApi>(_ =>
        {
            var apiConfiguration =
                configuration.BindOptions<AnalysisServiceConfiguration>(AnalysisServiceConfiguration.ConfigurationPath);

            return new SystemApi(apiConfiguration.ServiceUri);
        });

        services.AddScoped<ISystemService, SystemService>();

        return services;
    }
}
