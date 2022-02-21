namespace Climatisation.Rules.EventHandlers;

using System.Threading.Tasks;
using AnalysisService.Contracts.IntegrationEvents;
using Climatisation.Rules.Diagnostics;
using Climatisation.Rules.Events;
using Core.Bus.Handlers;
using MediatR;
using Rebus.Bus;

public class PropertyChangedIntegrationEventHandler : IntegrationEventHandler<PropertyChangedIntegrationEvent>
{
    private readonly ClimatisationRulesDiagnostics _climatisationRulesDiagnostics;

    private readonly IMediator _mediator;

    public PropertyChangedIntegrationEventHandler(ClimatisationRulesDiagnostics climatisationRulesDiagnostics, IMediator mediator)
    {
        _climatisationRulesDiagnostics = climatisationRulesDiagnostics;
        _mediator = mediator;
    }

    public override async Task Handle(PropertyChangedIntegrationEvent message)
    {
        _climatisationRulesDiagnostics.PropertyChangeEventReceived(message);

        await _mediator.Publish(new PropertyChangedEvent { Name = message.PropertyName });
    }
}
