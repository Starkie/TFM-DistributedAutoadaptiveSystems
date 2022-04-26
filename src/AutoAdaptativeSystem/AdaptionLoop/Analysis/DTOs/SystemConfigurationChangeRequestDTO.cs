namespace Analysis.Service.DTOs.Configuration;

using System;
using System.Collections.Generic;

public class SystemConfigurationChangeRequestDTO
{
    public DateTime Timestamp { get; set; }

    public IList<SymptomDTO> Symptoms { get; set; } = new List<SymptomDTO>();

    public IList<ServiceConfigurationDTO> ServiceConfiguration { get; set; } = new List<ServiceConfigurationDTO>();
}
