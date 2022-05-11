namespace Execute.Contracts.IntegrationEvents;

using Core.Bus.Contracts.Events;
using Core.Bus.Contracts.Requests;
using Planning.Contracts.IntegrationEvents.AdaptionActions;

public class ExecutionRequest : Request
{
    public string ServiceName { get; init; }

    public IEnumerable<AdaptionAction> Actions { get; init; }
}
