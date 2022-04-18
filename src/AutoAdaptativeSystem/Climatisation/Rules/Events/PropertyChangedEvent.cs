namespace Climatisation.Rules.Service.Events;

using MediatR;

public class PropertyChangedEvent : INotification
{
    public string Name { get; init; }
}
