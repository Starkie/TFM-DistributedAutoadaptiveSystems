namespace Climatisation.Effectors.Service.Application.AirConditioner.Requests;

using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Service.ApiClient.Api;
using MediatR;

public class SetAirConditionerModeRequestHandler
    : IRequestHandler<SetAirConditionerModeRequest>
{
    private readonly IAirConditionerApi _airConditionerApi;

    public SetAirConditionerModeRequestHandler(IAirConditionerApi airConditionerApi)
    {
        _airConditionerApi = airConditionerApi;
    }

    public async Task<Unit> Handle(SetAirConditionerModeRequest notification, CancellationToken cancellationToken)
    {
        // TODO: Diagnostics.
        var succeeded = Enum.TryParse(notification.Value, out AirConditioningMode mode);

        if (!succeeded)
        {
            // TODO diagnostics.
            return Unit.Value;
        }

        switch (mode)
        {
            case AirConditioningMode.Disabled:
                await _airConditionerApi.AirConditionerTurnOffPostAsync(cancellationToken);
                break;

            case AirConditioningMode.Cooling:
                await _airConditionerApi.AirConditionerCoolPostAsync(cancellationToken);
                break;

            case AirConditioningMode.Heating:
                await _airConditionerApi.AirConditionerHeatPostAsync(cancellationToken);
                break;
        }

        return Unit.Value;
    }
}
