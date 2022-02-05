namespace RoomMonitor;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MonitoringService.ApiClient.Api;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
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

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "RoomMonitor", Version = "v1" });

            // Set the comments path for the Swagger JSON and UI.
            // Obtained from https://github.com/domaindrivendev/Swashbuckle.WebApi/issues/93#issuecomment-458690098.
            List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                .ToList();

            xmlFiles.ForEach(xmlFile => c.IncludeXmlComments(xmlFile));
        });

        services.AddScoped<IPropertyApi, PropertyApi>(_ =>
        {
            MonitoringServiceConfiguration configuration = GetMonitoringServiceConfiguration(Configuration);

            return new PropertyApi(configuration.ServiceUri);
        });

        services.AddScoped<IMonitorApi, MonitorApi>(_ =>
        {
            MonitoringServiceConfiguration configuration = GetMonitoringServiceConfiguration(Configuration);

            return new MonitorApi(configuration.ServiceUri);
        });

        services.AddOpenTelemetryTracing(builder =>
        {
            builder.SetResourceBuilder(ResourceBuilder
                    .CreateDefault()
                    .AddService(RoomMonitorConstants.AppName, serviceVersion: "ver1.0"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource(RoomMonitorConstants.AppName)
                .AddJaegerExporter();
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
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RoomMonitor v1"));
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private static MonitoringServiceConfiguration GetMonitoringServiceConfiguration(IConfiguration configuration)
    {
        MonitoringServiceConfiguration serviceConfiguration =
            configuration.GetSection(MonitoringServiceConfiguration.ConfigurationPath)
                .Get<MonitoringServiceConfiguration>();

        return serviceConfiguration;
    }
}
