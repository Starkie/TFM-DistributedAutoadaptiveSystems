namespace Knowledge.Service.DTOs.Configuration;

using System;
using System.Collections.Generic;
using Knowledge.Contracts.IntegrationEvents;

public class ConfigurationChangeRequestDTO
{
    public DateTime Timestamp { get; set; }

    public IList<SymptomDTO> Symptoms { get; set; }

    public IList<ChangeRequestDTO> RequestedChanges { get; set; }

}
