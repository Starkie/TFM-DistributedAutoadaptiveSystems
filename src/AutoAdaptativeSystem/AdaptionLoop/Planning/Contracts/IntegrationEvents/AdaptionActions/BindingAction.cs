namespace Planning.Contracts.IntegrationEvents.AdaptionActions;

using System;

public class BindingAction : AdaptionAction
{
    public string TargetService { get; }

    public BindingAction(AdaptionActionType actionType, string serviceName, string targetService)
        : base(actionType, serviceName)
    {
        if (actionType is not (AdaptionActionType.Bind or AdaptionActionType.Unbind))
        {
            throw new ArgumentException("Invalid operation type. Only bind and unbind are allowed.");
        }

        TargetService = targetService;
    }

    public override string ToString()
    {
        return ServiceName + " ---> " + TargetService;
    }
}
