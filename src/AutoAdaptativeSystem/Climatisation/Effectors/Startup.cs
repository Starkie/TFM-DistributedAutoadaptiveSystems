namespace Climatisation.Effectors.Service;

using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Service.ApiClient.Api;
using Climatisation.Effectors.Service.Configurations;
using Climatisation.Effectors.Service.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
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
            "Climatisation Effector Service",
            "Demonstrates all the existing operations to access and manage the effectors of the AirConditioner Service.",
            "v1");

        services.AddTelemetry(Configuration, ClimatisationEffectorConstants.AppName, "v1.0");

        services.AddBus(
            Configuration,
            this.GetType().Assembly,
            registerSubscriptions: async bus =>
            {
                await bus.Advanced.Topics.Subscribe(ClimatisationAirConditionerConstants.AppName);
            });

        services.AddMediatR(typeof(Startup).Assembly);

        services.AddSingleton<ClimatisationEffectorServiceDiagnostics>();

        services.AddScoped<IAirConditionerApi, AirConditionerApi>(_ =>
        {
            var configuration =
                Configuration.BindOptions<ClimatisationAirConditionerConfiguration>(ClimatisationAirConditionerConfiguration.ConfigurationPath);

            return new AirConditionerApi(configuration.ServiceUri);
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSerilogRequestLogging();

        app.UseMetricServer();

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ClimatisationEffectorConstants.AppName} v1"));

        app.UseRouting();

        app.UseHttpMetrics();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
