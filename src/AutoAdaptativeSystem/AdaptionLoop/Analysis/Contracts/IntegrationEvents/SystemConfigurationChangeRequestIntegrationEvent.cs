namespace Analysis.Contracts.IntegrationEvents;

using Core.Bus.Contracts.Events;

public class SystemConfigurationChangeRequestIntegrationEvent : IIntegrationEvent
{
    public DateTime Timestamp { get; set; }

    public IList<Symptom> Symptoms { get; set; }

    public IList<SystemConfigurationRequest> ConfigurationRequests { get; set; }
}

public class SystemConfigurationRequest
{
    public string ServiceName { get; set; }

    public bool IsDeployed { get; set; }

    public IList<ConfigurationProperty> ConfigurationProperties { get; set; }

    public IList<BindingConfiguration> Bindings { get; set; }
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

public class Symptom
{
    public string Name { get; set; }

    public string Value { get; set; }
}

