namespace AnalysisService;

using AnalysisService.Configurations;
using AnalysisService.Diagnostics;
using Knowledge.Contracts.Controllers.IntegrationEvents;
using KnowledgeService.ApiClient.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        services.AddControllers();

        services.AddSwagger(
            "Analysis Service",
            "Demonstrates all the existing operations to access and manage Adaption Rules.",
            "v1");

        services.AddBus(
            Configuration,
            this.GetType().Assembly,
            registerSubscriptions: async bus =>
            {
                await bus.Subscribe<PropertyChangedIntegrationEvent>();
            });

        services.AddTracing(Configuration, AnalysisServiceConstants.AppName, "v1.0");

        services.AddScoped<IPropertyApi, PropertyApi>(_ =>
        {
            var configuration =
                Configuration.BindOptions<KnowledgeServiceConfiguration>(KnowledgeServiceConfiguration.ConfigurationPath);

            return new PropertyApi(configuration.ServiceUri);
        });

        services.AddSingleton<AnalysisServiceDiagnostics>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KnowledgeService v1"));

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}