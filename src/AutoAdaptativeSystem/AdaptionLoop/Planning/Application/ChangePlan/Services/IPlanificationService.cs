namespace Planning.Service.Application.ChangePlan.Services;

using System.Threading.Tasks;
using Analysis.Contracts.IntegrationEvents;

public interface IPlanificationService
{
    Task PlanNextConfiguration(SystemConfigurationChangeRequest systemConfigurationChangeRequest);
}
