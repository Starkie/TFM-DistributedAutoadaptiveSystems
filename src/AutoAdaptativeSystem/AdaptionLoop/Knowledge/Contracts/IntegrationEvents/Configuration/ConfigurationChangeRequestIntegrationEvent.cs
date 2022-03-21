namespace Knowledge.Contracts.IntegrationEvents.Configuration;

using Core.Bus.Contracts.Events;

public sealed class ConfigurationChangeRequestIntegrationEvent : IIntegrationEvent
{
    public IEnumerable<ChangeRequest> RequestedChanges { get; }

    public DateTime Timestamp { get; }

    public IEnumerable<Symptom> Symptoms { get; }

    public ConfigurationChangeRequestIntegrationEvent(
        IEnumerable<ChangeRequest> requestedChanges,
        IEnumerable<Symptom> symptoms,
        DateTime timestamp)
    {
        RequestedChanges = requestedChanges;
        Timestamp = timestamp;
        Symptoms = symptoms;
    }
}
