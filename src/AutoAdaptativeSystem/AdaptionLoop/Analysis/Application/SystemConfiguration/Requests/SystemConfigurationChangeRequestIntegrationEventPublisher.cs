namespace Analysis.Service.SystemConfiguration.Requests;

using Analysis.Contracts.IntegrationEvents;
using Core.Bus.Publisher;
using Rebus.Bus;

public class SystemConfigurationChangeRequestPublisher
    : RequestPublisher<SystemConfigurationChangeRequest>
{
    public SystemConfigurationChangeRequestPublisher(IBus bus)
        : base(bus)
    {
    }
}
