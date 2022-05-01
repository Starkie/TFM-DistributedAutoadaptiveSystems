namespace Climatisation.AirConditioner.Service.BackgroundServices;

using Climatisation.AirConditioner.Application.AirConditioners;
using Climatisation.AirConditioner.Contracts;
using Climatisation.Monitor.Service.ApiClient.Api;
using Climatisation.Monitor.Service.ApiClient.Model;

public class AdaptionLoopRegistrationHostedService : IHostedService
{
    private readonly IServiceProvider _rootServiceProvider;

    private IAirConditionerService _airConditionerService;

    private IServiceApi _serviceApi;

    public AdaptionLoopRegistrationHostedService(IServiceProvider rootServiceProvider)
    {
        _rootServiceProvider = rootServiceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // TODO: Diagnostics.
        using var scope = _rootServiceProvider.CreateScope();

        ResolveDependencies(scope.ServiceProvider);

        var remainingAttempts = 5;
        bool seedSucceeded = false;

        do
        {
            remainingAttempts--;

            try
            {
                seedSucceeded = await SeedConfiguration();
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

    private async Task<bool> SeedConfiguration()
    {
        var currentMode = _airConditionerService.GetCurrentMode();

        await _serviceApi.ServiceServiceNameConfigurationConfigurationNamePutAsync(
            ClimatisationAirConditionerConstants.AppName,
            ClimatisationAirConditionerConstants.Configuration.Mode,
            new SetPropertyDTO(currentMode.ToString()));

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
