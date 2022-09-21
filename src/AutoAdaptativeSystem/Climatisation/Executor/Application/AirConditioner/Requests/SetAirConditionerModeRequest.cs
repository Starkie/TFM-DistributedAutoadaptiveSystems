namespace Climatisation.Executor.Service.Application.AirConditioner.Requests;

using Climatisation.Executor.Service.Application.Execution.Requests;

public class SetAirConditionerModeRequest : SetParameterRequest
{
    public SetAirConditionerModeRequest(string propertyName, string value)
        : base(propertyName, value)
    {
    }
}
