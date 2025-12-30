// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Dividend;

/// <summary>
/// Command to delete a dividend.
/// </summary>
public record DeleteDividendCommand(Guid DividendId) : IRequest<Unit>;

/// <summary>
/// Handler for DeleteDividendCommand.
/// </summary>
public class DeleteDividendCommandHandler : IRequestHandler<DeleteDividendCommand, Unit>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<DeleteDividendCommandHandler> _logger;

    public DeleteDividendCommandHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<DeleteDividendCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteDividendCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting dividend: {DividendId}", request.DividendId);

        var dividend = await _context.Dividends
            .FirstOrDefaultAsync(d => d.DividendId == request.DividendId, cancellationToken);

        if (dividend == null)
        {
            _logger.LogWarning("Dividend not found: {DividendId}", request.DividendId);
            throw new KeyNotFoundException($"Dividend with ID {request.DividendId} not found.");
        }

        _context.Dividends.Remove(dividend);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Dividend deleted: {DividendId}", request.DividendId);

        return Unit.Value;
    }
}
