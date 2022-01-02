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

            Guid probeId = new Guid("c02234d3-329c-4b4d-aee0-d220dc25276b");

            Random random = new Random();

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

                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }
    }
}
