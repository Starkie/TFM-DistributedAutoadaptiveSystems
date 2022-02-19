namespace KnowledgeService;

using Core.Bus.Publisher;
using Knowledge.Contracts.Controllers.IntegrationEvents;
using Knowledge.Contracts.Queues;
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

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwagger(
            "Knowledge Service",
            "Demonstrates all the existing operations to access and manage Knowledge properties.",
            "v1");

        services.AddBus(Configuration, this.GetType().Assembly, r =>
            r.TypeBased()
            .Map<PropertyChangedIntegrationEvent>(KnowledgeQueues.PropertyChanged));

        services.AddTracing(Configuration, KnowledgeServiceConstants.AppName, "v1.0");

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
