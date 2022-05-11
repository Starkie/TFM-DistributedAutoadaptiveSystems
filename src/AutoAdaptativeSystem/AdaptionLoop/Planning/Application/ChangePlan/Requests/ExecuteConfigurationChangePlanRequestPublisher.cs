namespace Planning.Service.Application.ChangePlan.Requests;

using Core.Bus.Publisher;
using Planning.Contracts.IntegrationEvents;
using Rebus.Bus;

public class ExecuteConfigurationChangePlanRequestPublisher
 : RequestPublisher<ExecuteChangePlanRequest>
{
    public ExecuteConfigurationChangePlanRequestPublisher(IBus bus)
        : base(bus)
    {
    }
}
