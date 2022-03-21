namespace Knowledge.Service.DTOs.Configuration;

using System;

public class ConfigurationDTO
{
    public string ServiceName { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public DateTime LastModification { get; set; }
}
