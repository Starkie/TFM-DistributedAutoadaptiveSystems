using Climatisation.Monitor.Service.ApiClient.Api;
using Climatisation.Monitor.Service.ApiClient.Model;
using NBomber.Contracts;
using NBomber.CSharp;

var random = new Random();

var measurementApi = new MeasurementApi("http://localhost:7001");

Guid probeId = Guid.NewGuid();

var step = Step.Create("step", async context =>
{
    // you can define and execute any logic here,
    // for example: send http request, SQL query etc
    // NBomber will measure how much time it takes to execute your logic
    double temperature = random.Next(17, 25);
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

    return Response.Ok();
});

// second, we add our step to the scenario
var scenario = ScenarioBuilder.CreateScenario("Load test Temperature measurements", step);

NBomberRunner
    .RegisterScenarios(scenario)
    .Run();
