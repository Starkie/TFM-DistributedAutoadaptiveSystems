namespace Climatisation.AirConditioner.Service.BackgroundServices;

using Climatisation.AirConditioner.Application.AirConditioners;
using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Service.Configurations;
using Climatisation.AirConditioner.Service.Diagnostics;
using Climatisation.Contracts;
using Climatisation.Monitor.Service.ApiClient.Api;
using Climatisation.Monitor.Service.ApiClient.Model;
using Planning.Contracts;

public class AdaptionLoopRegistrationHostedService : IHostedService
{
    private readonly IServiceProvider _rootServiceProvider;

    private readonly IConfiguration _configuration;

    private readonly ClimatisationAirConditionerServiceDiagnostics _diagnostics;

    private IAirConditionerService _airConditionerService;

    private IServiceApi _serviceApi;

    public AdaptionLoopRegistrationHostedService(
        IServiceProvider rootServiceProvider,
        IConfiguration configuration,
        ClimatisationAirConditionerServiceDiagnostics diagnostics)
    {
        _rootServiceProvider = rootServiceProvider;
        _configuration = configuration;
        _diagnostics = diagnostics;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _rootServiceProvider.CreateScope();

        using var activity = _diagnostics.StartSeedAdaptionLoopConfiguration();

        ResolveDependencies(scope.ServiceProvider);

        var remainingAttempts = 5;
        bool seedSucceeded = false;

        do
        {
            remainingAttempts--;

            try
            {
                var airConditionerConfiguration =
                    _configuration.BindOptions<AirConditionerConfiguration>(AirConditionerConfiguration.ConfigurationName);

                seedSucceeded = await SeedConfiguration(airConditionerConfiguration);
            }
            catch (Exception exception)
            {
                // TODO: Diagnostics.
                seedSucceeded = false;

                if (remainingAttempts <= 0)
                {
                    throw;
                }

                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        } while (!seedSucceeded && remainingAttempts > 0);
    }

    private async Task<bool> SeedConfiguration(AirConditionerConfiguration configuration)
    {
        var currentMode = _airConditionerService.GetCurrentMode();

        var configurationKeys = new List<SetPropertyDTO>();

        configurationKeys.Add(new SetPropertyDTO(
            ClimatisationAirConditionerConstants.Configuration.Mode,
            currentMode.ToString()));

        configurationKeys.Add(new SetPropertyDTO(
            AdaptionLoopPlanningConstants.Configuration.IsDeployed,
            "true"));

        configurationKeys.Add(new SetPropertyDTO(
            ClimatisationConstants.Configuration.ColdTemperatureThreshold,
            configuration.ColdTemperatureThreshold.ToString()));

        configurationKeys.Add(new SetPropertyDTO(
            ClimatisationConstants.Configuration.HotTemperatureThreshold,
            configuration.HotTemperatureThreshold.ToString()));

        configurationKeys.Add(new SetPropertyDTO(
            ClimatisationConstants.Configuration.TargetTemperature,
            configuration.TargetTemperature.ToString()));

        await _serviceApi.ServiceServiceNameConfigurationPutAsync(
            ClimatisationAirConditionerConstants.AppName,
            configurationKeys);

        return true;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void ResolveDependencies(IServiceProvider serviceProvider)
    {
        _airConditionerService = serviceProvider.GetRequiredService<IAirConditionerService>();
        _serviceApi = serviceProvider.GetRequiredService<IServiceApi>();
    }
}
