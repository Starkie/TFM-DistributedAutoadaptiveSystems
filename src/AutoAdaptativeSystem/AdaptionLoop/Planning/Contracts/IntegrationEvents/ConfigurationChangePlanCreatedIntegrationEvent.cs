namespace Planning.Contracts.IntegrationEvents;

using Core.Bus.Contracts.Events;

public class ConfigurationChangePlanCreatedIntegrationEvent  : IIntegrationEvent
{
    public ConfigurationChangePlan ChangePlan { get; init; }
}
