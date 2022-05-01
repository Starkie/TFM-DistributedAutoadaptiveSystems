namespace Climatisation.AirConditioner.Application;

using Climatisation.AirConditioner.Application.AirConditioners;
using Climatisation.AirConditioner.Domain.AirConditioners;
using Climatisation.AirConditioner.Domain.AirConditioners.Fakes;
using Climatisation.AirConditioner.Domain.Thermometers.Fakes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAirConditionerApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<FakeThermometer>();
        services.AddSingleton<AirConditioner, FakeAirConditioner>();

        services.AddScoped<IAirConditionerService, AirConditionerService>();

        services.AddMediatR(typeof(IAirConditionerService).Assembly);

        return services;
    }
}
