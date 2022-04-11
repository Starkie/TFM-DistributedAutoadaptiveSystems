namespace Knowledge.Contracts.IntegrationEvents;

public sealed class Symptom
{
    public string Name { get; }

    public string Value { get; }

    public Symptom(string name, string value)
    {
        Name = name;
        Value = value;
    }
}
