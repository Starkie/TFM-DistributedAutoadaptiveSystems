namespace Analysis.Contracts.Attributes;

public class RuleKnowledgeConfigurationDependencyAttribute : Attribute
{
    public RuleKnowledgeConfigurationDependencyAttribute(string serviceName, params string[] configurationKeys)
    {
        ServiceName = serviceName;
        ConfigurationKeys = configurationKeys;
    }

    private RuleKnowledgeConfigurationDependencyAttribute()
    {
        ConfigurationKeys = Enumerable.Empty<string>();
    }

    public string ServiceName { get; }

    public IEnumerable<string> ConfigurationKeys { get; }
}
