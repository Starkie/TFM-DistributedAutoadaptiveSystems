namespace Planning.Service;

using Analysis.Contracts.IntegrationEvents;
using MediatR;
using Planning.Service.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Planning.Service.Services;
using Serilog;

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
            "Planning Service",
            "Demonstrates all the existing operations to access and manage the Planning processes.",
            "v1");

        services.AddBus(
            Configuration,
            this.GetType().Assembly,
            registerSubscriptions: async bus =>
            {
                await bus.Subscribe<SystemConfigurationChangeRequestIntegrationEvent>();
            });

        services.AddTelemetry(Configuration, PlanningServiceConstants.AppName, "v1.0");

        services.AddMediatR(this.GetType().Assembly);

        services.AddKnowledgeServices(Configuration);

        services.AddSingleton<PlanningServiceDiagnostics>();

        services.AddScoped<IPlanificationService, PlanificationService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSerilogRequestLogging();

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{PlanningServiceConstants.AppName} v1"));

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
