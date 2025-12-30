// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.Mortgage;

/// <summary>
/// Query to get all mortgages.
/// </summary>
public record GetMortgagesQuery : IRequest<List<MortgageDto>>;

/// <summary>
/// Handler for GetMortgagesQuery.
/// </summary>
public class GetMortgagesQueryHandler : IRequestHandler<GetMortgagesQuery, List<MortgageDto>>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public GetMortgagesQueryHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<List<MortgageDto>> Handle(GetMortgagesQuery request, CancellationToken cancellationToken)
    {
        var mortgages = await _context.Mortgages
            .OrderByDescending(m => m.StartDate)
            .ToListAsync(cancellationToken);

        return mortgages.Select(m => m.ToDto()).ToList();
    }
}
