namespace Climatisation.Rules;

using Analysis.Service.ApiClient.Api;
using AnalysisService.Configurations;
using Climatisation.Rules.Diagnostics;
using Climatisation.Rules.Services;
using MediatR;
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
            "Climatisation Rules Service",
            "Demonstrates all the existing operations to access and manage Adaption Rules.",
            "v1");

        services.AddBus(
            Configuration,
            this.GetType().Assembly,
            registerSubscriptions: async bus =>
            {
                // TODO: Find how to obtain these registrations from the rules in the current assembly.
                await bus.Advanced.Topics.Subscribe("Temperature");
            });

        services.AddScoped<IPropertyApi, PropertyApi>(_ =>
        {
            var configuration =
                Configuration.BindOptions<AnalysisServiceConfiguration>(AnalysisServiceConfiguration.ConfigurationPath);

            return new PropertyApi(configuration.ServiceUri);
        });

        services.AddTracing(Configuration, ClimatisationRulesConstants.AppName, "v1.0");

        services.AddSingleton<ClimatisationRulesDiagnostics>();

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

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Knowledge.Service v1"));

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
