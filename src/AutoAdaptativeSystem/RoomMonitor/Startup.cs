namespace RoomMonitor;

using System;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonitoringService.ApiClient.Api;
using RoomMonitor.Configurations;

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

        services.AddSwagger("RoomMonitor", String.Empty, "v1");

        services.AddTracing(Configuration, RoomMonitorConstants.AppName, "ver1.0");

        services.AddScoped<IPropertyApi, PropertyApi>(_ =>
        {
            var configuration =
                Configuration.BindOptions<MonitoringServiceConfiguration>(MonitoringServiceConfiguration.ConfigurationPath);

            return new PropertyApi(configuration.ServiceUri);
        });

        services.AddScoped<IMonitorApi, MonitorApi>(_ =>
        {
            var configuration =
                Configuration.BindOptions<MonitoringServiceConfiguration>(MonitoringServiceConfiguration.ConfigurationPath);

            return new MonitorApi(configuration.ServiceUri);
        });

        services.AddSingleton<RoomMonitorDiagnostics>();

        services.AddMediatR(typeof(Startup));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RoomMonitor v1"));

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
