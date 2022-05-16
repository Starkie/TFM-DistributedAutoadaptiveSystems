namespace Climatisation.AirConditioner.Service.Configurations;

public class AirConditionerConfiguration
{
    public const string ConfigurationName = "AirConditioner";

    public double ColdTemperatureThreshold { get; set; }

    public double HotTemperatureThreshold { get; set; }

    public double TargetTemperature { get; set; }
}
