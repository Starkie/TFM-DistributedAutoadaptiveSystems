namespace Climatisation.Effectors.Service.Application.AirConditioner.Requests;

using Climatisation.Effectors.Service.Application.Execution.Requests;

public class SetAirConditionerModeRequest : SetParameterRequest
{
    public SetAirConditionerModeRequest(string propertyName, string value)
        : base(propertyName, value)
    {
    }
}
