namespace Knowledge.Contracts.IntegrationEvents;

using Core.Bus.Contracts.Events;
using MediatR;

public class ConfigurationChangedIntegrationEvent : IIntegrationEvent, INotification
{
    public string ServiceName { get; }

    public string ConfigurationName { get; }

    public ConfigurationChangedIntegrationEvent(string serviceName, string configurationName)
    {
        ServiceName = serviceName;
        ConfigurationName = configurationName;
    }
}
