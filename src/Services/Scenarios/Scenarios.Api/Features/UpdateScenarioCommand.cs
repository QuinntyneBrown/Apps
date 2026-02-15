using Scenarios.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Scenarios.Api.Features;

public record UpdateScenarioCommand(
    Guid RetirementScenarioId,
    string Name,
    int CurrentAge,
    int RetirementAge,
    decimal CurrentSavings,
    decimal AnnualContribution,
    decimal ExpectedReturnRate) : IRequest<ScenarioDto?>;

public class UpdateScenarioCommandHandler : IRequestHandler<UpdateScenarioCommand, ScenarioDto?>
{
    private readonly IScenariosDbContext _context;

    public UpdateScenarioCommandHandler(IScenariosDbContext context)
    {
        _context = context;
    }

    public async Task<ScenarioDto?> Handle(UpdateScenarioCommand request, CancellationToken cancellationToken)
    {
        var scenario = await _context.RetirementScenarios
            .FirstOrDefaultAsync(s => s.RetirementScenarioId == request.RetirementScenarioId, cancellationToken);

        if (scenario == null) return null;

        scenario.Name = request.Name;
        scenario.CurrentAge = request.CurrentAge;
        scenario.RetirementAge = request.RetirementAge;
        scenario.CurrentSavings = request.CurrentSavings;
        scenario.AnnualContribution = request.AnnualContribution;
        scenario.ExpectedReturnRate = request.ExpectedReturnRate;
        scenario.LastUpdated = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return scenario.ToDto();
    }
}
