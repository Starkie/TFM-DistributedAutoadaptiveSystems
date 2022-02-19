namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServicesFromAssembly<T>(this IServiceCollection services, Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        return services.RegisterServicesFromAssembly(typeof(T), assembly, lifetime);
    }

    public static IServiceCollection RegisterServicesFromAssembly(this IServiceCollection services, Type typeToRegister, Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        var types = assembly.GetTypes()
            .Where(t => DoesTypeSupportInterface(t, typeToRegister));

        foreach (var type in types)
        {
            services.Add(new ServiceDescriptor(type, type, lifetime));

            if (typeToRegister.IsInterface)
            {
                services.Add(new ServiceDescriptor(GetGenericInterface(type, typeToRegister).First(), type, lifetime));
            }
        }

        return services;
    }

    private static bool DoesTypeSupportInterface(Type type, Type inter)
    {
        return type switch
        {
            _ when inter.IsAssignableFrom(type) => true,
            _ when GetGenericInterface(type, inter).Any() => true,
            _ => false,
        };
    }

    private static IEnumerable<Type> GetGenericInterface(Type type, Type inter)
    {
        return type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == inter);
    }
}
