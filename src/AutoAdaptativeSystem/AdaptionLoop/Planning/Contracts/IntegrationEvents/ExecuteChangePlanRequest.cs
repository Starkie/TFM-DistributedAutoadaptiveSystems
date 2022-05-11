namespace Planning.Contracts.IntegrationEvents;

using Core.Bus.Contracts.Events;
using Core.Bus.Contracts.Requests;
using MediatR;

public class ExecuteChangePlanRequest : Request
{
    public ConfigurationChangePlan ChangePlan { get; init; }
}
