namespace Planning.Contracts.IntegrationEvents;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Bus.Contracts.Events;
using Planning.Contracts.IntegrationEvents.AdaptionActions;

public class ConfigurationChangePlan
{
    public ConfigurationChangePlan()
    {
        Actions = new();
        Timestamp = DateTime.UtcNow;
    }

    public DateTime Timestamp { get; set; }

    public List<AdaptionAction> Actions { get; set; }

    public override string ToString()
    {
        var changePlanDescriptionBuilder = new StringBuilder();

        foreach (var value in Enum.GetValues<AdaptionActionType>())
        {
            AppendAdaptionDescription(Actions, value, changePlanDescriptionBuilder);
        }

        return changePlanDescriptionBuilder.ToString();
    }

    private static void AppendAdaptionDescription(IEnumerable<AdaptionAction> adaptionActions, AdaptionActionType adaptionActionType, StringBuilder changePlanDescriptionBuilder)
    {
        var actionsDescriptions = adaptionActions
            .Where(da => da.Type == adaptionActionType)
            .Select(s => s.ToString())
            .ToList();

        if (actionsDescriptions.Count == 0)
        {
            return;
        }

        changePlanDescriptionBuilder.AppendLine(adaptionActionType + ":");
    
        foreach (var action in actionsDescriptions)
        {
            changePlanDescriptionBuilder.AppendLine("\t" + action);
        }
    }
}
