namespace Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;

public static class ConfigurationExtensions
{
    public static IConfiguration ReplaceVariable(this IConfiguration configuration, string variableName, string variableValue = null)
    {
        if (string.IsNullOrEmpty(variableValue))
        {
            variableValue = FindVariableValue(configuration.GetChildren(), variableName);
        }

        ReplaceVariable(configuration.GetChildren(), variableName, variableValue);

        return configuration;
    }

    private static void ReplaceVariable(IEnumerable<IConfigurationSection> configurationSections, string variableName, string variableValue)
    {
        var placeholderName = "$" + variableName;

        foreach (var section in configurationSections)
        {
            if (section.Value is not null && section.Value.Contains(placeholderName))
            {
                section.Value = section.Value.Replace(placeholderName, variableValue);
            }

            var sectionChildren = section.GetChildren();

            if (sectionChildren.Any())
            {
                ReplaceVariable(sectionChildren, variableName, variableValue);
            }
        }
    }

    private static string FindVariableValue(IEnumerable<IConfigurationSection> configurationSections, string variableName)
    {
        foreach (var section in configurationSections)
        {
            if (section.Key.Equals(variableName))
            {
                return section.Value;
            }

            var sectionChildren = section.GetChildren();

            if (sectionChildren.Any())
            {
                var foundValue = FindVariableValue(sectionChildren, variableName);

                if (foundValue is not null)
                {
                    return foundValue;
                }
            }
        }

        return null;
    }

    public static T BindOptions<T>(this IConfiguration configuration, string configurationPath)
        where T : class
    {
        T instance = typeof(T)
            .GetConstructor(Type.EmptyTypes)
            .Invoke(null) as T;

        configuration.Bind(configurationPath, instance);

        return instance;
    }
}
