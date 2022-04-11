namespace Microsoft.Extensions.DependencyInjection;

using KnowledgeService.Options;
using Microsoft.Extensions.Configuration;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Rebus.OpenTelemetry.Configuration;

public static class OpenTelemetryTracingExtensions
{
    public static IServiceCollection AddTracing(this IServiceCollection services, IConfiguration configuration, string serviceName, string serviceVersion)
    {
        services.AddOpenTelemetryTracing(builder =>
        {
            builder.SetResourceBuilder(ResourceBuilder
                    .CreateDefault()
                    .AddService(serviceName, serviceVersion: serviceVersion))
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
}
