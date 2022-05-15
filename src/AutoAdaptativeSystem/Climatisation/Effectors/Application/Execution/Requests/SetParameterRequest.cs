namespace Climatisation.Effectors.Service.Application.Execution.Requests;

using Execute.Contracts.IntegrationEvents;
using MediatR;

public class SetParameterRequest : IRequest
{
    public SetParameterRequest(string propertyName, string value)
    {
        PropertyName = propertyName;
        Value = value;
    }

    public string PropertyName { get; }

    public string Value { get; }
}
