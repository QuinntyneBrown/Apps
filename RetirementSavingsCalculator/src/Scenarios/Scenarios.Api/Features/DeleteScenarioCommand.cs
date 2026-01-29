using Scenarios.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Scenarios.Api.Features;

public record DeleteScenarioCommand(Guid ScenarioId) : IRequest<bool>;

public class DeleteScenarioCommandHandler : IRequestHandler<DeleteScenarioCommand, bool>
{
    private readonly IScenariosDbContext _context;

    public DeleteScenarioCommandHandler(IScenariosDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteScenarioCommand request, CancellationToken cancellationToken)
    {
        var scenario = await _context.RetirementScenarios
            .FirstOrDefaultAsync(s => s.RetirementScenarioId == request.ScenarioId, cancellationToken);

        if (scenario == null) return false;

        _context.RetirementScenarios.Remove(scenario);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
