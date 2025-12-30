// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.RefinanceScenario;

/// <summary>
/// Query to get a refinance scenario by ID.
/// </summary>
public record GetRefinanceScenarioByIdQuery : IRequest<RefinanceScenarioDto?>
{
    public Guid RefinanceScenarioId { get; set; }
}

/// <summary>
/// Handler for GetRefinanceScenarioByIdQuery.
/// </summary>
public class GetRefinanceScenarioByIdQueryHandler : IRequestHandler<GetRefinanceScenarioByIdQuery, RefinanceScenarioDto?>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public GetRefinanceScenarioByIdQueryHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<RefinanceScenarioDto?> Handle(GetRefinanceScenarioByIdQuery request, CancellationToken cancellationToken)
    {
        var scenario = await _context.RefinanceScenarios
            .FirstOrDefaultAsync(s => s.RefinanceScenarioId == request.RefinanceScenarioId, cancellationToken);

        return scenario?.ToDto();
    }
}
