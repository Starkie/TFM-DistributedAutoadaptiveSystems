namespace KnowledgeService;

using Core.Bus.Extensions;
using KnowledgeService.Controllers.IntegrationEvents;
using KnowledgeService.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rebus.Routing.TypeBased;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

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

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwagger(
            "Knowledge Service",
            "Demonstrates all the existing operations to access and manage Knowledge properties.",
            "v1");

        services.AddBus(Configuration, r =>
            r.TypeBased()
            .Map<PropertyChangedIntegrationEvent>("Knowledge.Property.Changed"));

        services.AddTracing(Configuration, KnowledgeServiceConstants.AppName, "v1.0");

        services.AddSingleton<KnowledgeServiceDiagnostics>();

        // TODO: Register by its interface.
        services.AddScoped<PropertyChangedIntegrationEventPublisher>();
    }
}