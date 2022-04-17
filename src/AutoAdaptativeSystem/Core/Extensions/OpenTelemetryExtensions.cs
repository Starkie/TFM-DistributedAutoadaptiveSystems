namespace Microsoft.Extensions.DependencyInjection;

using KnowledgeService.Options;
using Microsoft.Extensions.Configuration;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Rebus.OpenTelemetry.Configuration;

public static class OpenTelemetryTracingExtensions
{
    public static IServiceCollection AddTelemetry(this IServiceCollection services, IConfiguration configuration, string serviceName, string serviceVersion)
    {
        var resourceBuilder = ResourceBuilder
            .CreateDefault()
            .AddService(serviceName, serviceVersion);

        services.AddTracing(configuration, resourceBuilder, serviceName);

        services.AddMetrics(configuration, resourceBuilder);

        return services;
    }

    public static IServiceCollection AddTracing(this IServiceCollection services, IConfiguration configuration, ResourceBuilder resourceBuilder, string serviceName)
    {
        services.AddOpenTelemetryTracing(builder =>
        {
            builder.SetResourceBuilder(resourceBuilder)
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRebusInstrumentation()
                .AddSource(serviceName)
                .AddJaegerExporter(configure =>
                {
                    var jaegerOptions = configuration.BindOptions<JaegerOptions>(JaegerOptions.ConfigurationPath);

                    configure.AgentHost = jaegerOptions.Host;
                    configure.AgentPort = jaegerOptions.Port;
                });
        });

        return services;
    }

    public static IServiceCollection AddMetrics(this IServiceCollection services, IConfiguration configuration, ResourceBuilder resourceBuilder)
    {
        // TODO: When the metrics support is improved, use this configuration and remove the dependency with prometheus-net.
        // services.AddOpenTelemetryMetrics(builder =>
        // {
        //     builder.SetResourceBuilder(resourceBuilder)
        //         .AddHttpClientInstrumentation()
        //         // TODO: There are some Well-Known ASP.NET Core metrics: https://docs.microsoft.com/en-us/dotnet/core/diagnostics/available-counters
        //         // They are NOT supported yet. See the issue tracking this: https://github.com/open-telemetry/opentelemetry-dotnet-contrib/issues/215
        //         .AddAspNetCoreInstrumentation()
        //         .AddPrometheusExporter();
        // });

        return services;
    }
}
