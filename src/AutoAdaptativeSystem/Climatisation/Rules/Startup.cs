namespace Climatisation.Rules.Service;

using Climatisation.Rules.Service.Diagnostics;
using Climatisation.Rules.Service.Services;
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
            "Climatisation Rules Service",
            "Demonstrates all the existing operations to access and manage Adaption Rules.",
            "v1");

        services.AddAdaptionLoopAnalysisServices(Configuration, this.GetType().Assembly);

        services.AddTelemetry(Configuration, ClimatisationRulesConstants.AppName, "v1.0");

        services.AddSingleton<ClimatisationRulesDiagnostics>();

        services.AddScoped<IConfigurationService, ConfigurationService>();
        services.AddScoped<IPropertyService, PropertyService>();

        services.AddMediatR(typeof(Startup).Assembly);
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
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ClimatisationRulesConstants.AppName} v1"));

        app.UseRouting();

        app.UseHttpMetrics();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
