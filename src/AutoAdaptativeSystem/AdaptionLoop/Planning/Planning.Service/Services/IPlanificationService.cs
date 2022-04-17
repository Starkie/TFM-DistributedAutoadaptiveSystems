namespace Planning.Service.Services;

using System.Threading;
using System.Threading.Tasks;
using Analysis.Contracts.IntegrationEvents;

public interface IPlanificationService
{
    Task PlanNextConfiguration(SystemConfigurationChangeRequestIntegrationEvent systemConfigurationChangeRequestIntegrationEvent);
}
