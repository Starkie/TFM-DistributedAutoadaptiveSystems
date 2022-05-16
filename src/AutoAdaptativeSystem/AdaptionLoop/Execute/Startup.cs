namespace Execute.Service;

using System.Reflection;
using Execute.Service.Contracts;
using Execute.Service.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Planning.Contracts.IntegrationEvents;
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
            "Execute Service",
            "Demonstrates all the existing operations to access and manage the Execute processes.",
            "v1");

        services.AddBus(
            Configuration,
            this.GetType().Assembly,
            registerSubscriptions: async bus =>
            {
                await bus.Subscribe<ExecuteChangePlanRequest>();
            });

        services.AddTelemetry(Configuration, ExecuteServiceConstants.AppName, "v1.0");

        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddSingleton<ExecuteServiceDiagnostics>();
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
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ExecuteServiceConstants.AppName} v1"));

        app.UseRouting();

        app.UseHttpMetrics();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
