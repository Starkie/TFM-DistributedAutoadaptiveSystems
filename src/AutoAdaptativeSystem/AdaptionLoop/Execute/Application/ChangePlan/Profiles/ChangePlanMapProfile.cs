namespace Execute.Service.Application.ChangePlan.Profiles;

using AutoMapper;
using Execute.Contracts.IntegrationEvents;

public class ChangePlanMapProfile : Profile
{
    public ChangePlanMapProfile()
    {
        CreateMap<Planning.Contracts.IntegrationEvents.Symptom, Symptom>();
    }
}
