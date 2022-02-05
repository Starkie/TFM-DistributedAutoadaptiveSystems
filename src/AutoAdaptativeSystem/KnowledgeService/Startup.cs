namespace KnowledgeService;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KnowledgeService.Diagnostics;
using KnowledgeService.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

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

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Knowledge Service",
                Description = "Demonstrates all the existing operations to access and manage Knowledge properties.",
                Version = "v1",
            });

            // Set the comments path for the Swagger JSON and UI.
            // Obtained from https://github.com/domaindrivendev/Swashbuckle.WebApi/issues/93#issuecomment-458690098.
            List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                .ToList();

            xmlFiles.ForEach(xmlFile => c.IncludeXmlComments(xmlFile));
        });

        services.AddOpenTelemetryTracing(builder =>
        {
            builder.SetResourceBuilder(ResourceBuilder
                    .CreateDefault()
                    .AddService(KnowledgeServiceConstants.AppName, serviceVersion: "ver1.0"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource(KnowledgeServiceConstants.AppName)
                .AddJaegerExporter(configure =>
                {
                    var jaegerOptions = Configuration.BindOptions<JaegerOptions>(JaegerOptions.ConfigurationPath);

                    configure.AgentHost = jaegerOptions.Host;
                    configure.AgentPort = jaegerOptions.Port;
                });
        });

        services.AddSingleton<KnowledgeServiceDiagnostics>();
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
