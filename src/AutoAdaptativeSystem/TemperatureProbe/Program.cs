using System;

namespace TemperatureProbe
{
    using System.Net.Http;
    using System.Threading;
    using RoomMonitor.ApiClient.Api;
    using RoomMonitor.ApiClient.Client;
    using RoomMonitor.ApiClient.Model;

    class Program
    {
        static void Main(string[] args)
        {
            MeasurementApi measurementApi = new MeasurementApi("http://localhost:5000");

            Random random = new Random();

            while (true)
            {
                double temperature = random.NextDouble() * 35;
                string formatedTemperature = temperature.ToString("F2");

                Console.WriteLine($"[Temperature] - Reporting {formatedTemperature}ºC");

                measurementApi.MeasurementTemperaturePost(new TemperatureMeasurementDTO(temperature, TemperatureUnit.CELSIUS));

                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }
    }
}