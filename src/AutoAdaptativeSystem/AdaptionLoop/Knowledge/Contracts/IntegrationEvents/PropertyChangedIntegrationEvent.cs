namespace Knowledge.Contracts.IntegrationEvents;

using Core.Bus.Contracts.Events;

public class PropertyChangedIntegrationEvent : IIntegrationEvent
{
    public string PropertyName { get; }

    public PropertyChangedIntegrationEvent(string propertyName)
    {
        PropertyName = propertyName;
    }
}
