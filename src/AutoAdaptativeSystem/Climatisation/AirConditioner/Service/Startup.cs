namespace Climatisation.AirConditioner.Service;

using System.Text.Json.Serialization;
using Climatisation.AirConditioner.Application;
using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Service.BackgroundServices;
using Climatisation.AirConditioner.Service.Configurations;
using Climatisation.AirConditioner.Service.Diagnostics;
using Climatisation.Monitor.Service.ApiClient.Api;
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
        services.AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddSwagger("Monitoring Service", string.Empty, "v1");

        services.AddTelemetry(Configuration, ClimatisationAirConditionerConstants.AppName, "v1.0");

        services.AddSingleton<ClimatisationAirConditionerServiceDiagnostics>();

        services.AddAirConditionerApplicationServices();

        services.AddScoped<IMeasurementApi, MeasurementApi>(_ =>
        {
            var configuration =
                Configuration.BindOptions<ClimatisationMonitorConfiguration>(ClimatisationMonitorConfiguration.ConfigurationPath);

            return new MeasurementApi(configuration.ServiceUri);
        });

        services.AddScoped<IServiceApi, ServiceApi>(_ =>
        {
            var configuration =
                Configuration.BindOptions<ClimatisationMonitorConfiguration>(ClimatisationMonitorConfiguration.ConfigurationPath);

            return new ServiceApi(configuration.ServiceUri);
        });

        services.Configure<AirConditionerConfiguration>(
            Configuration.GetSection(AirConditionerConfiguration.ConfigurationName));

        services.AddHostedService<AirConditionerBackgroundService>();

        services.AddHostedService<AdaptionLoopRegistrationHostedService>();
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
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ClimatisationAirConditionerConstants.AppName} v1"));

        app.UseRouting();

        app.UseHttpMetrics();

        app.UseAuthentication();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
