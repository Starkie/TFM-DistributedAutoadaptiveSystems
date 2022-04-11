namespace Core.Bus.Configuration;

public class BusConfiguration
{
    public const string ConfigurationPath = "BusConfiguration";

    public string ServiceUri { get; set; }

    public string InputQueueName { get; set; }
}
