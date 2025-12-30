// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.RefinanceScenario;

/// <summary>
/// Command to delete a refinance scenario.
/// </summary>
public record DeleteRefinanceScenarioCommand : IRequest<Unit>
{
    public Guid RefinanceScenarioId { get; set; }
}

/// <summary>
/// Handler for DeleteRefinanceScenarioCommand.
/// </summary>
public class DeleteRefinanceScenarioCommandHandler : IRequestHandler<DeleteRefinanceScenarioCommand, Unit>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public DeleteRefinanceScenarioCommandHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteRefinanceScenarioCommand request, CancellationToken cancellationToken)
    {
        var scenario = await _context.RefinanceScenarios
            .FirstOrDefaultAsync(s => s.RefinanceScenarioId == request.RefinanceScenarioId, cancellationToken);

        if (scenario == null)
        {
            throw new Exception($"RefinanceScenario with ID {request.RefinanceScenarioId} not found.");
        }

        _context.RefinanceScenarios.Remove(scenario);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
