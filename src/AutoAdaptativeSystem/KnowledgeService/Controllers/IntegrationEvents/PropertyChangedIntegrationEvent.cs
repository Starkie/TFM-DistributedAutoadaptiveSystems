namespace KnowledgeService.Controllers.IntegrationEvents;

using Core.Bus.Events;
using KnowledgeService.DTOs;

public class PropertyChangedIntegrationEvent: IIntegrationEvent
{
    public string PropertyName { get; }

    public PropertyChangedIntegrationEvent(string propertyName)
    {
        PropertyName = propertyName;
    }
}
