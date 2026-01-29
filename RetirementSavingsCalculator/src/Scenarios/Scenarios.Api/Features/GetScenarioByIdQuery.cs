using Scenarios.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Scenarios.Api.Features;

public record GetScenarioByIdQuery(Guid ScenarioId) : IRequest<ScenarioDto?>;

public class GetScenarioByIdQueryHandler : IRequestHandler<GetScenarioByIdQuery, ScenarioDto?>
{
    private readonly IScenariosDbContext _context;

    public GetScenarioByIdQueryHandler(IScenariosDbContext context)
    {
        _context = context;
    }

    public async Task<ScenarioDto?> Handle(GetScenarioByIdQuery request, CancellationToken cancellationToken)
    {
        var scenario = await _context.RetirementScenarios
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.RetirementScenarioId == request.ScenarioId, cancellationToken);

        return scenario?.ToDto();
    }
}
