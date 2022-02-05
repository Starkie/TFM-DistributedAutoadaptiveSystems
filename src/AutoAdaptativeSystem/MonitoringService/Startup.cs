namespace MonitoringService;

using System.Text.Json.Serialization;
using KnowledgeService.ApiClient.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonitoringService.Configurations;
using MonitoringService.Diagnostics;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddSwagger("Monitoring Service", string.Empty, "v1");

        services.AddTracing(MonitoringServiceConstants.AppName, "v1.0");

        services.AddSingleton<MonitoringServiceDiagnostics>();

        services.AddScoped<IPropertyApi, PropertyApi>(_ =>
        {
            var configuration =
                Configuration.BindOptions<KnowledgeServiceConfiguration>(KnowledgeServiceConfiguration.ConfigurationPath);

            return new PropertyApi(configuration.ServiceUri);
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MonitoringService v1"));

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
