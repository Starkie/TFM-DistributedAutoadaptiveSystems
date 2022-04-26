using System;
using System.Threading;
using Climatisation.Monitor.Service.ApiClient.Api;
using Climatisation.Monitor.Service.ApiClient.Model;
using Microsoft.Extensions.Configuration;
using Climatisation.Probes.Temperature.Configuration;

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

var roomMonitorConfiguration =
    configuration.BindOptions<RoomMonitorConfiguration>(RoomMonitorConfiguration.ConfigurationPath);

var measurementApi = new MeasurementApi(roomMonitorConfiguration.ServiceUri);

var probeId = new Guid("c02234d3-329c-4b4d-aee0-d220dc25276b");

var random = new Random();

while (true)
{
    double temperature = random.NextDouble() * 35;
    string formatedTemperature = temperature.ToString("F2");

    Console.WriteLine($"[Temperature] - Reporting {formatedTemperature}ºC");

    try
    {
        measurementApi.MeasurementTemperaturePost(new TemperatureMeasurementDTO(temperature, TemperatureUnit.CELSIUS, probeId, DateTime.UtcNow));
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }

    Thread.Sleep(TimeSpan.FromSeconds(15));
}
