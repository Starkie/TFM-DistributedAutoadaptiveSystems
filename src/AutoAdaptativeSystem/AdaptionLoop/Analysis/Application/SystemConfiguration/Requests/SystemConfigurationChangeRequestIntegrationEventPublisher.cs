namespace Analysis.Service.SystemConfiguration.Requests;

using Analysis.Contracts.IntegrationEvents;
using Core.Bus.Publisher;
using Planning.Contracts;
using Rebus.Bus;

public class SystemConfigurationChangeRequestPublisher
    : RequestQueuePublisher<SystemConfigurationChangeRequest>
{
    public SystemConfigurationChangeRequestPublisher(IBus bus)
        : base(bus, AdaptionLoopPlanningConstants.Queues.PlanningServiceQueue)
    {
    }
}
