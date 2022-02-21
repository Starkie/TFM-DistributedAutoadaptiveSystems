namespace Climatisation.Rules.Events;

using MediatR;

public class PropertyChangedEvent : INotification
{
    public string Name { get; init; }
}
