namespace Analysis.Contracts.Attributes;

public class RuleKnowledgePropertyDependencyAttribute : Attribute
{
    public RuleKnowledgePropertyDependencyAttribute(params string[] propertyNames)
    {
        PropertyNames = propertyNames;
    }

    private RuleKnowledgePropertyDependencyAttribute()
    {
        PropertyNames = Enumerable.Empty<string>();
    }

    public IEnumerable<string> PropertyNames { get; }
}
