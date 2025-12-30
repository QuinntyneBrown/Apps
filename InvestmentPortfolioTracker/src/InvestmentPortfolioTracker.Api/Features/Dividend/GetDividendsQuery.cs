// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Dividend;

/// <summary>
/// Query to get all dividends.
/// </summary>
public record GetDividendsQuery : IRequest<List<DividendDto>>;

/// <summary>
/// Handler for GetDividendsQuery.
/// </summary>
public class GetDividendsQueryHandler : IRequestHandler<GetDividendsQuery, List<DividendDto>>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<GetDividendsQueryHandler> _logger;

    public GetDividendsQueryHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<GetDividendsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<DividendDto>> Handle(GetDividendsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all dividends");

        var dividends = await _context.Dividends
            .OrderByDescending(d => d.PaymentDate)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Retrieved {Count} dividends", dividends.Count);

        return dividends.Select(d => d.ToDto()).ToList();
    }
}
