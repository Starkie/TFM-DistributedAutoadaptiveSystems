namespace Planning.Contracts.IntegrationEvents;

using Core.Bus.Contracts.Events;
using MediatR;

public class ConfigurationChangePlanCreatedIntegrationEvent  : IIntegrationEvent, INotification
{
    public ConfigurationChangePlan ChangePlan { get; init; }
}
