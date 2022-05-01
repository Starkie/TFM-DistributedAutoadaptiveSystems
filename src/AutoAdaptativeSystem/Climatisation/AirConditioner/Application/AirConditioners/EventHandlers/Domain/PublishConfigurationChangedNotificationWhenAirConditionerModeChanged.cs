namespace Climatisation.AirConditioner.Application.AirConditioners.EventHandlers.Domain;

using System.Diagnostics;
using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Domain.AirConditioners.Events.Domain;
using Climatisation.Monitor.Service.ApiClient.Api;
using Climatisation.Monitor.Service.ApiClient.Model;

public class PublishConfigurationChangedNotificationWhenAirConditionerModeChanged
    : IDomainEventHandler<AirConditionerModeChanged>
{
    private readonly IServiceApi _serviceApi;

    public PublishConfigurationChangedNotificationWhenAirConditionerModeChanged(
        IServiceApi serviceApi)
    {
        _serviceApi = serviceApi;
    }

    public async Task Handle(AirConditionerModeChanged notification, CancellationToken cancellationToken)
    {
        // TODO: Declare activity.
        // using Activity activity = _diag

        await _serviceApi.ServiceServiceNameConfigurationConfigurationNamePutAsync(
            ClimatisationAirConditionerConstants.AppName,
            ClimatisationAirConditionerConstants.Configuration.Mode,
            new SetPropertyDTO(notification.Mode.ToString()),
            cancellationToken);
    }
}
