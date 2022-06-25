namespace Analysis.Contracts.Attributes;

public class RuleServiceConfigurationDependencyAttribute : Attribute
{
    public RuleServiceConfigurationDependencyAttribute(string serviceName, params string[] configurationKeys)
    {
        ServiceName = serviceName;
        ConfigurationKeys = configurationKeys;
    }

    private RuleServiceConfigurationDependencyAttribute()
    {
        ConfigurationKeys = Enumerable.Empty<string>();
    }

    public string ServiceName { get; }

    public IEnumerable<string> ConfigurationKeys { get; }
}
