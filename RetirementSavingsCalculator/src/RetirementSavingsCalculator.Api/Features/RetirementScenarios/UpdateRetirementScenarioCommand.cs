using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.RetirementScenarios;

public record UpdateRetirementScenarioCommand : IRequest<RetirementScenarioDto?>
{
    public Guid RetirementScenarioId { get; init; }
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

public class UpdateRetirementScenarioCommandHandler : IRequestHandler<UpdateRetirementScenarioCommand, RetirementScenarioDto?>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<UpdateRetirementScenarioCommandHandler> _logger;

    public UpdateRetirementScenarioCommandHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<UpdateRetirementScenarioCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RetirementScenarioDto?> Handle(UpdateRetirementScenarioCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating retirement scenario {RetirementScenarioId}", request.RetirementScenarioId);

        var scenario = await _context.RetirementScenarios
            .FirstOrDefaultAsync(s => s.RetirementScenarioId == request.RetirementScenarioId, cancellationToken);

        if (scenario == null)
        {
            _logger.LogWarning("Retirement scenario {RetirementScenarioId} not found", request.RetirementScenarioId);
            return null;
        }

        scenario.Name = request.Name;
        scenario.CurrentAge = request.CurrentAge;
        scenario.RetirementAge = request.RetirementAge;
        scenario.LifeExpectancyAge = request.LifeExpectancyAge;
        scenario.CurrentSavings = request.CurrentSavings;
        scenario.AnnualContribution = request.AnnualContribution;
        scenario.ExpectedReturnRate = request.ExpectedReturnRate;
        scenario.InflationRate = request.InflationRate;
        scenario.ProjectedAnnualIncome = request.ProjectedAnnualIncome;
        scenario.ProjectedAnnualExpenses = request.ProjectedAnnualExpenses;
        scenario.Notes = request.Notes;
        scenario.LastUpdated = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated retirement scenario {RetirementScenarioId}", request.RetirementScenarioId);

        return scenario.ToDto();
    }
}
