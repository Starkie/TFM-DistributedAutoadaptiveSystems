namespace Planning.Contracts.IntegrationEvents.AdaptionActions;

using System;

public class DeploymentAction : AdaptionAction
{
    public DeploymentAction(AdaptionActionType actionType, string serviceName)
        : base(actionType, serviceName)
    {
        if (actionType is not (AdaptionActionType.Deploy or AdaptionActionType.Undeploy))
        {
            throw new ArgumentException("Invalid operation type. Only deploy and undeploy are allowed.");
        }
    }

    public override string ToString()
    {
        return ServiceName;
    }
}
