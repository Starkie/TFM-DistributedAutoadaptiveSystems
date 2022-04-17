namespace Planning.Contracts.IntegrationEvents.AdaptionActions;

public class SetParameterAction : AdaptionAction
{
    public string PropertyName { get; }

    public string PropertyValue { get; }

    public SetParameterAction(string serviceName, string propertyName, string propertyValue)
        : base(AdaptionActionType.SetParameter, serviceName)
    {
        PropertyName = propertyName;
        PropertyValue = propertyValue;
    }

    public override string ToString()
    {
        return ServiceName + "." + PropertyName + " = " + PropertyValue;
    }
}
