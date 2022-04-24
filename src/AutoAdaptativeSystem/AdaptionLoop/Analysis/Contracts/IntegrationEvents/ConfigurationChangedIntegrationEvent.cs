namespace Analysis.Service.Contracts.IntegrationEvents;

using Core.Bus.Contracts.Events;

public class ConfigurationChangedIntegrationEvent : IIntegrationEvent
{
    public string ServiceName { get; }

    public string PropertyName { get; }

    public ConfigurationChangedIntegrationEvent(string serviceName, string propertyName)
    {
        PropertyName = propertyName;
        ServiceName = serviceName;
    }
}
