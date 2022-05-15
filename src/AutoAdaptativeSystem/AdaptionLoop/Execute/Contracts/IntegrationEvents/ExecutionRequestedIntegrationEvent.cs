namespace Execute.Contracts.IntegrationEvents;

using Core.Bus.Contracts.Events;
using Core.Bus.Contracts.Requests;
using Planning.Contracts.IntegrationEvents.AdaptionActions;

public class ExecutionRequestedIntegrationEvent : IIntegrationEvent
{
    public string ServiceName { get; init; }

    public IEnumerable<AdaptionAction> Actions { get; init; }

    public IEnumerable<Symptom> Symptoms { get; set; }
}
