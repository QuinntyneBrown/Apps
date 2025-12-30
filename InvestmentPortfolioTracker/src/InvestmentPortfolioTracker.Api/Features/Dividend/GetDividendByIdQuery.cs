// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Dividend;

/// <summary>
/// Query to get a dividend by ID.
/// </summary>
public record GetDividendByIdQuery(Guid DividendId) : IRequest<DividendDto?>;

/// <summary>
/// Handler for GetDividendByIdQuery.
/// </summary>
public class GetDividendByIdQueryHandler : IRequestHandler<GetDividendByIdQuery, DividendDto?>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<GetDividendByIdQueryHandler> _logger;

    public GetDividendByIdQueryHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<GetDividendByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DividendDto?> Handle(GetDividendByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting dividend: {DividendId}", request.DividendId);

        var dividend = await _context.Dividends
            .FirstOrDefaultAsync(d => d.DividendId == request.DividendId, cancellationToken);

        if (dividend == null)
        {
            _logger.LogWarning("Dividend not found: {DividendId}", request.DividendId);
            return null;
        }

        return dividend.ToDto();
    }
}
