namespace Execute.Contracts.IntegrationEvents;

using Core.Bus.Contracts.Events;
using Planning.Contracts.IntegrationEvents.AdaptionActions;

public class ExecutionRequestIntegrationEvent : IIntegrationEvent
{
    public string ServiceName { get; init; }

    public IEnumerable<AdaptionAction> Actions { get; init; }
}
