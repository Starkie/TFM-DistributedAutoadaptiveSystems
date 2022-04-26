namespace Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Analysis.Contracts.Attributes;
using Analysis.Service.ApiClient.Api;
using AnalysisService.Configurations;
using Climatisation.Rules.Service.EventHandlers.Rules;
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
                var subscriptions = GetRulesBusTopicNames(rulesAssembly);

                foreach (var subscription in subscriptions)
                {
                    await bus.Advanced.Topics.Subscribe(subscription);
                }
            });

        services.AddScoped<IPropertyApi, PropertyApi>(_ =>
        {
            var apiConfiguration =
                configuration.BindOptions<AnalysisServiceConfiguration>(AnalysisServiceConfiguration.ConfigurationPath);

            return new PropertyApi(apiConfiguration.ServiceUri);
        });

        services.AddScoped<IServiceApi, ServiceApi>(_ =>
        {
            var apiConfiguration =
                configuration.BindOptions<AnalysisServiceConfiguration>(AnalysisServiceConfiguration.ConfigurationPath);

            return new ServiceApi(apiConfiguration.ServiceUri);
        });

        services.AddScoped<ISystemApi, SystemApi>(_ =>
        {
            var apiConfiguration =
                configuration.BindOptions<AnalysisServiceConfiguration>(AnalysisServiceConfiguration.ConfigurationPath);

            return new SystemApi(apiConfiguration.ServiceUri);
        });

        return services;
    }

    public static IEnumerable<string> GetRulePropertyDependencies(this Type t)
    {
        var attribute = t.GetCustomAttribute(typeof(RuleKnowledgePropertyDependencyAttribute)) as RuleKnowledgePropertyDependencyAttribute;

        return attribute?.PropertyNames ?? Enumerable.Empty<string>();
    }

    public static IDictionary<string, IEnumerable<string>> GetRuleConfigurationDependencies(this Type t)
    {
        var attributes = t.GetCustomAttributes<RuleKnowledgeConfigurationDependencyAttribute>();

        var configurationKeys = new Dictionary<string, IEnumerable<string>>();

        foreach (var attribute in attributes)
        {
            configurationKeys[attribute.ServiceName] = attribute.ConfigurationKeys;
        }

        return configurationKeys;
    }

    private static IEnumerable<string> GetRulesBusTopicNames(Assembly rulesAssembly)
    {
        var ruleTypes = rulesAssembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(RuleBase)));

        var subscriptions = new List<string>();

        var propertyNames = ruleTypes
            .Select(GetRulePropertyDependencies)
            .SelectMany(p => p)
            .Distinct();

        subscriptions.AddRange(propertyNames);

        var configurationKeys = ruleTypes
            .Select(GetRuleConfigurationDependencies)
            .SelectMany(GetConfigurationServiceNames)
            .Distinct();

        subscriptions.AddRange(configurationKeys);

        return subscriptions;
    }

    private static IEnumerable<string> GetConfigurationServiceNames(IDictionary<string, IEnumerable<string>> configurationKeys)
    {
        return configurationKeys
            .SelectMany(kvp =>
                kvp.Value.Select(v => kvp.Key + "." + kvp.Value));
    }
}
