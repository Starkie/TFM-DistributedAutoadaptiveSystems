namespace Planning.Service.Application.ChangePlan.Profiles;

using AutoMapper;
using Planning.Contracts.IntegrationEvents;

public class ChangePlanMapProfile : Profile
{
    public ChangePlanMapProfile()
    {
        CreateMap<Analysis.Contracts.IntegrationEvents.Symptom, Symptom>();
    }
}
