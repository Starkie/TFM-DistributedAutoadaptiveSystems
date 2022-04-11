namespace Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Analysis.Contracts.Attributes;
using Analysis.Service.ApiClient.Api;
using AnalysisService.Configurations;
using Climatisation.Rules.EventHandlers.Rules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdaptionLoopAnalysisServices(this IServiceCollection services, IConfiguration configuration, Assembly rulesAssembly)
    {
        services.AddBus(
            configuration,
            rulesAssembly,
            registerSubscriptions: async bus =>
            {
                var ruleTypes = rulesAssembly.GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(RuleBase)));

                var propertyNames = ruleTypes.Select(t =>
                    {
                        return GetRulePropertyDependencies(t);
                    })
                    .SelectMany(p => p)
                    .Distinct();

                foreach (var propertyName in propertyNames)
                {
                    await bus.Advanced.Topics.Subscribe(propertyName);
                }
            });

        services.AddScoped<IPropertyApi, PropertyApi>(_ =>
        {
            var apiConfiguration =
                configuration.BindOptions<AnalysisServiceConfiguration>(AnalysisServiceConfiguration.ConfigurationPath);

            return new PropertyApi(apiConfiguration.ServiceUri);
        });

        services.AddScoped<IConfigurationApi, ConfigurationApi>(_ =>
        {
            var apiConfiguration =
                configuration.BindOptions<AnalysisServiceConfiguration>(AnalysisServiceConfiguration.ConfigurationPath);

            return new ConfigurationApi(apiConfiguration.ServiceUri);
        });

        return services;
    }

    public static IEnumerable<string> GetRulePropertyDependencies(this Type t)
    {
        var attribute = t.GetCustomAttribute(typeof(RuleKnowledgePropertyDependencyAttribute)) as RuleKnowledgePropertyDependencyAttribute;

        return attribute?.PropertyNames ?? Enumerable.Empty<string>();
    }
}
