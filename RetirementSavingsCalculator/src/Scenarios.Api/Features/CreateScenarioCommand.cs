using Scenarios.Core;
using Scenarios.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Scenarios.Api.Features;

public record CreateScenarioCommand(
    Guid TenantId,
    string Name,
    int CurrentAge,
    int RetirementAge,
    int LifeExpectancyAge,
    decimal CurrentSavings,
    decimal AnnualContribution,
    decimal ExpectedReturnRate,
    decimal InflationRate) : IRequest<ScenarioDto>;

public class CreateScenarioCommandHandler : IRequestHandler<CreateScenarioCommand, ScenarioDto>
{
    private readonly IScenariosDbContext _context;
    private readonly ILogger<CreateScenarioCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateScenarioCommandHandler(IScenariosDbContext context, ILogger<CreateScenarioCommandHandler> logger, IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ScenarioDto> Handle(CreateScenarioCommand request, CancellationToken cancellationToken)
    {
        var scenario = new RetirementScenario
        {
            RetirementScenarioId = Guid.NewGuid(),
            TenantId = request.TenantId,
            Name = request.Name,
            CurrentAge = request.CurrentAge,
            RetirementAge = request.RetirementAge,
            LifeExpectancyAge = request.LifeExpectancyAge,
            CurrentSavings = request.CurrentSavings,
            AnnualContribution = request.AnnualContribution,
            ExpectedReturnRate = request.ExpectedReturnRate,
            InflationRate = request.InflationRate,
            CreatedAt = DateTime.UtcNow
        };

        _context.RetirementScenarios.Add(scenario);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Scenario created: {ScenarioId}", scenario.RetirementScenarioId);

        return scenario.ToDto();
    }
}
