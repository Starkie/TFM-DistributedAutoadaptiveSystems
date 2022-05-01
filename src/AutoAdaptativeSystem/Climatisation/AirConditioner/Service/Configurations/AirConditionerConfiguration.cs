namespace Climatisation.AirConditioner.Service.Configurations;

// TODO: Should this configuration be outside the air conditioner?
// Who should seed it into the system?
public class AirConditionerConfiguration
{
    public const string ConfigurationName = "AirConditionerConfiguration";

    public double ColdTemperatureThreshold { get; set; }

    public double HotTemperatureThreshold { get; set; }

    public double TargetTemperature { get; set; }
}
