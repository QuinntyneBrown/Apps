// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Holding;

/// <summary>
/// Command to delete a holding.
/// </summary>
public record DeleteHoldingCommand(Guid HoldingId) : IRequest<Unit>;

/// <summary>
/// Handler for DeleteHoldingCommand.
/// </summary>
public class DeleteHoldingCommandHandler : IRequestHandler<DeleteHoldingCommand, Unit>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<DeleteHoldingCommandHandler> _logger;

    public DeleteHoldingCommandHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<DeleteHoldingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteHoldingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting holding: {HoldingId}", request.HoldingId);

        var holding = await _context.Holdings
            .FirstOrDefaultAsync(h => h.HoldingId == request.HoldingId, cancellationToken);

        if (holding == null)
        {
            _logger.LogWarning("Holding not found: {HoldingId}", request.HoldingId);
            throw new KeyNotFoundException($"Holding with ID {request.HoldingId} not found.");
        }

        _context.Holdings.Remove(holding);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Holding deleted: {HoldingId}", request.HoldingId);

        return Unit.Value;
    }
}
