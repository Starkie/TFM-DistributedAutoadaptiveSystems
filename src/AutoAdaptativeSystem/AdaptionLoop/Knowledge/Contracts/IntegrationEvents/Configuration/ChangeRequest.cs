namespace Knowledge.Contracts.IntegrationEvents.Configuration;

public sealed class ChangeRequest
{
    public string ServiceName { get; }

    public string PropertyName { get; }

    public string PropertyNewValue { get; }

    public ChangeRequest(string serviceName, string propertyName, string propertyNewValue)
    {
        ServiceName = serviceName;
        PropertyName = propertyName;
        PropertyNewValue = propertyNewValue;
    }
}
