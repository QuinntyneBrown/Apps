// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.Mortgage;

/// <summary>
/// Query to get a mortgage by ID.
/// </summary>
public record GetMortgageByIdQuery : IRequest<MortgageDto?>
{
    public Guid MortgageId { get; set; }
}

/// <summary>
/// Handler for GetMortgageByIdQuery.
/// </summary>
public class GetMortgageByIdQueryHandler : IRequestHandler<GetMortgageByIdQuery, MortgageDto?>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public GetMortgageByIdQueryHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<MortgageDto?> Handle(GetMortgageByIdQuery request, CancellationToken cancellationToken)
    {
        var mortgage = await _context.Mortgages
            .FirstOrDefaultAsync(m => m.MortgageId == request.MortgageId, cancellationToken);

        return mortgage?.ToDto();
    }
}
