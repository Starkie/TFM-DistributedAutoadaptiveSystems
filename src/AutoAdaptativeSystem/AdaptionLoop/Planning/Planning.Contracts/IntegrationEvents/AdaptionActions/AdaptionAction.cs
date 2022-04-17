namespace Planning.Contracts.IntegrationEvents.AdaptionActions;

public abstract class AdaptionAction
{
    public AdaptionAction(AdaptionActionType type, string serviceName)
    {
        Type = type;
        ServiceName = serviceName;
    }

    public AdaptionActionType Type { get; }

    public string ServiceName { get; }
}
