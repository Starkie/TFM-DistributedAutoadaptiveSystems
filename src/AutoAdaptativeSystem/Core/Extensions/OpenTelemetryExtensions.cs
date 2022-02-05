namespace Microsoft.Extensions.DependencyInjection;

using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

public static class OpenTelemetryTracingExtensions
{
    public static IServiceCollection AddTracing(this IServiceCollection services, string serviceName, string serviceVersion)
    {
        services.AddOpenTelemetryTracing(builder =>
        {
            builder.SetResourceBuilder(ResourceBuilder
                    .CreateDefault()
                    .AddService(serviceName, serviceVersion: serviceVersion))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource(serviceName)
                .AddJaegerExporter();
        });

        return services;
    }
}
