namespace Analysis.Service.DTOs.Configuration;

using Analysis.Contracts.IntegrationEvents;
using AutoMapper;

public class SystemConfigurationChangeRequestMapProfile : Profile
{
    public SystemConfigurationChangeRequestMapProfile()
    {
        CreateMap<SystemConfigurationChangeRequestDTO, SystemConfigurationChangeRequestIntegrationEvent>()
            .ForMember(dst => dst.ConfigurationRequests, opts => opts.MapFrom(src => src.ServiceConfiguration));

        CreateMap<ServiceConfigurationDTO, SystemConfigurationRequest>();

        CreateMap<ConfigurationProperty, Analysis.Contracts.IntegrationEvents.ConfigurationProperty>();

        CreateMap<BindingConfiguration, Analysis.Contracts.IntegrationEvents.BindingConfiguration>();

        CreateMap<SymptomDTO, Symptom>();
    }
}
