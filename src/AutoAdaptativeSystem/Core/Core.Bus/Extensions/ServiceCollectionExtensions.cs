namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using Core.Bus.Configuration;
using Core.Bus.Publisher;
using Microsoft.Extensions.Configuration;
using Rebus.Config;
using Rebus.Routing;

public static class ServiceCollectionExtensions
{
    // TODO: Use a convention registry of the publishers and handlers.
    // TODO: Defer the connection to the bus with a background service.
    public static IServiceCollection AddBus(this IServiceCollection services, IConfiguration configuration, Assembly assembly, Action<StandardConfigurer<IRouter>>? mapEventsAction = null)
    {
        var busOptions = configuration.BindOptions<BusConfiguration>(BusConfiguration.ConfigurationPath);

        services.AddRebus(configure =>
        {
            configure.Logging(l => l.Serilog())
                .Options(o => o.EnableDiagnosticSources())
                .Transport(t => t.UseRabbitMqAsOneWayClient(busOptions.ServiceUri));

            if (mapEventsAction != null)
            {
                configure.Routing(mapEventsAction);
            }

            return configure;
        });

        services.RegisterServicesFromAssembly(typeof(IIntegrationEventPublisher<>), assembly, lifetime: ServiceLifetime.Scoped);

        return services;
    }
}
