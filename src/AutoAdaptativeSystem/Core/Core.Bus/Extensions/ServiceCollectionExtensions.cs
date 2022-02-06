namespace Core.Bus.Extensions;

using Core.Bus.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing;
using Rebus.Routing.TypeBased;

public static class ServiceCollectionExtensions
{
    // TODO: Use a convention registry of the publishers and handlers.
    // TODO: Defer the connection to the bus with a background service.
    public static IServiceCollection AddBus(this IServiceCollection services, IConfiguration configuration, Action<StandardConfigurer<IRouter>> mapEventsAction)
    {
        var busOptions =
            configuration.BindOptions<BusConfiguration>(BusConfiguration.ConfigurationPath);

        services.AddRebus(configure =>
            configure.Logging(l => l.Serilog())
            .Options(o => o.EnableDiagnosticSources())
            .Transport(t => t.UseRabbitMqAsOneWayClient(busOptions.ServiceUri))
            .Routing(mapEventsAction));

        return services;
    }
}