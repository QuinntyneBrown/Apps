// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Holding;

/// <summary>
/// Query to get all holdings.
/// </summary>
public record GetHoldingsQuery : IRequest<List<HoldingDto>>;

/// <summary>
/// Handler for GetHoldingsQuery.
/// </summary>
public class GetHoldingsQueryHandler : IRequestHandler<GetHoldingsQuery, List<HoldingDto>>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<GetHoldingsQueryHandler> _logger;

    public GetHoldingsQueryHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<GetHoldingsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<HoldingDto>> Handle(GetHoldingsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all holdings");

        var holdings = await _context.Holdings
            .OrderBy(h => h.Symbol)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Retrieved {Count} holdings", holdings.Count);

        return holdings.Select(h => h.ToDto()).ToList();
    }
}
