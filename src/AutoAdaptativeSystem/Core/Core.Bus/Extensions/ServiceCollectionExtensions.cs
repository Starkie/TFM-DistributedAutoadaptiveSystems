namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using Core.Bus.Configuration;
using Core.Bus.Contracts.Publisher;
using Microsoft.Extensions.Configuration;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Handlers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBus(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly,
        Func<IBus, Task>? registerSubscriptions = null)
    {
        var busOptions = configuration.BindOptions<BusConfiguration>(BusConfiguration.ConfigurationPath);

        bool canReceiveMessages = !string.IsNullOrEmpty(busOptions.InputQueueName);

        services.AddRebus(configure =>
        {
            configure.Logging(l => l.Serilog())
                .Options(o => o.EnableDiagnosticSources())
                .Transport(t =>
                {
                    if (!canReceiveMessages)
                    {
                        t.UseRabbitMqAsOneWayClient(busOptions.ServiceUri);
                    }
                    else
                    {
                        t.UseRabbitMq(busOptions.ServiceUri, busOptions.InputQueueName);
                    }
                });

            return configure;
        },
            onCreated: registerSubscriptions);

        services.RegisterServicesFromAssembly(typeof(IIntegrationEventPublisher<>), assembly, lifetime: ServiceLifetime.Scoped);

        if (canReceiveMessages)
        {
            services.RegisterServicesFromAssembly(typeof(IHandleMessages<>), assembly, lifetime: ServiceLifetime.Scoped);
        }

        return services;
    }
}
