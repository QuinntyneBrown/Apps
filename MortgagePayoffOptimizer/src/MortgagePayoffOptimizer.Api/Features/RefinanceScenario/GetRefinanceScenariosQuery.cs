// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.RefinanceScenario;

/// <summary>
/// Query to get all refinance scenarios.
/// </summary>
public record GetRefinanceScenariosQuery : IRequest<List<RefinanceScenarioDto>>;

/// <summary>
/// Handler for GetRefinanceScenariosQuery.
/// </summary>
public class GetRefinanceScenariosQueryHandler : IRequestHandler<GetRefinanceScenariosQuery, List<RefinanceScenarioDto>>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public GetRefinanceScenariosQueryHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<List<RefinanceScenarioDto>> Handle(GetRefinanceScenariosQuery request, CancellationToken cancellationToken)
    {
        var scenarios = await _context.RefinanceScenarios
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);

        return scenarios.Select(s => s.ToDto()).ToList();
    }
}
