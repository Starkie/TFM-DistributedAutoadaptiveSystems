namespace Analysis.Service.DTOs.Configuration;

using System.Collections.Generic;

public class ServiceConfigurationDTO
{
    public string ServiceName { get; set; }

    public bool IsActive { get; set; }

    public IList<ConfigurationProperty> ConfigurationProperties { get; set; } = new List<ConfigurationProperty>();

    public IList<BindingConfiguration> Bindings { get; set; } = new List<BindingConfiguration>();
}

public class ConfigurationProperty
{
    public string Name { get; set; }

    public string Value { get; set; }
}

public class BindingConfiguration
{
    public string TargetService { get; set; }

    public bool Active { get; set; }
}
