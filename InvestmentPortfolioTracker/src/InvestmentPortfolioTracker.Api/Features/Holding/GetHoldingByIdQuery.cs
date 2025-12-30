// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Holding;

/// <summary>
/// Query to get a holding by ID.
/// </summary>
public record GetHoldingByIdQuery(Guid HoldingId) : IRequest<HoldingDto?>;

/// <summary>
/// Handler for GetHoldingByIdQuery.
/// </summary>
public class GetHoldingByIdQueryHandler : IRequestHandler<GetHoldingByIdQuery, HoldingDto?>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<GetHoldingByIdQueryHandler> _logger;

    public GetHoldingByIdQueryHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<GetHoldingByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HoldingDto?> Handle(GetHoldingByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting holding: {HoldingId}", request.HoldingId);

        var holding = await _context.Holdings
            .FirstOrDefaultAsync(h => h.HoldingId == request.HoldingId, cancellationToken);

        if (holding == null)
        {
            _logger.LogWarning("Holding not found: {HoldingId}", request.HoldingId);
            return null;
        }

        return holding.ToDto();
    }
}
