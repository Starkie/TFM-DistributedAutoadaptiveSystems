namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.OpenApi.Models;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, string title, string description, string version)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = title,
                Description = description,
                Version = version,
            });

            // Set the comments path for the Swagger JSON and UI.
            // Obtained from https://github.com/domaindrivendev/Swashbuckle.WebApi/issues/93#issuecomment-458690098.
            List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                .ToList();

            xmlFiles.ForEach(xmlFile => c.IncludeXmlComments(xmlFile));
        });

        return services;
    }
}
