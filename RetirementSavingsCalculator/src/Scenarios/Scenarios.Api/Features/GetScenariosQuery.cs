using Scenarios.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Scenarios.Api.Features;

public record GetScenariosQuery : IRequest<IEnumerable<ScenarioDto>>;

public class GetScenariosQueryHandler : IRequestHandler<GetScenariosQuery, IEnumerable<ScenarioDto>>
{
    private readonly IScenariosDbContext _context;

    public GetScenariosQueryHandler(IScenariosDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ScenarioDto>> Handle(GetScenariosQuery request, CancellationToken cancellationToken)
    {
        return await _context.RetirementScenarios
            .AsNoTracking()
            .Select(s => s.ToDto())
            .ToListAsync(cancellationToken);
    }
}
