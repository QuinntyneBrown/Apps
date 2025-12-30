using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.RetirementScenarios;

public record CreateRetirementScenarioCommand : IRequest<RetirementScenarioDto>
{
    public string Name { get; init; } = string.Empty;
    public int CurrentAge { get; init; }
    public int RetirementAge { get; init; }
    public int LifeExpectancyAge { get; init; }
    public decimal CurrentSavings { get; init; }
    public decimal AnnualContribution { get; init; }
    public decimal ExpectedReturnRate { get; init; }
    public decimal InflationRate { get; init; }
    public decimal ProjectedAnnualIncome { get; init; }
    public decimal ProjectedAnnualExpenses { get; init; }
    public string? Notes { get; init; }
}

public class CreateRetirementScenarioCommandHandler : IRequestHandler<CreateRetirementScenarioCommand, RetirementScenarioDto>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<CreateRetirementScenarioCommandHandler> _logger;

    public CreateRetirementScenarioCommandHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<CreateRetirementScenarioCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RetirementScenarioDto> Handle(CreateRetirementScenarioCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating retirement scenario: {Name}",
            request.Name);

        var scenario = new RetirementScenario
        {
            RetirementScenarioId = Guid.NewGuid(),
            Name = request.Name,
            CurrentAge = request.CurrentAge,
            RetirementAge = request.RetirementAge,
            LifeExpectancyAge = request.LifeExpectancyAge,
            CurrentSavings = request.CurrentSavings,
            AnnualContribution = request.AnnualContribution,
            ExpectedReturnRate = request.ExpectedReturnRate,
            InflationRate = request.InflationRate,
            ProjectedAnnualIncome = request.ProjectedAnnualIncome,
            ProjectedAnnualExpenses = request.ProjectedAnnualExpenses,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
        };

        _context.RetirementScenarios.Add(scenario);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created retirement scenario {RetirementScenarioId}",
            scenario.RetirementScenarioId);

        return scenario.ToDto();
    }
}
