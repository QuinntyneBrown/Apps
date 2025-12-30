// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Holding;

/// <summary>
/// Command to update an existing holding.
/// </summary>
public record UpdateHoldingCommand : IRequest<HoldingDto>
{
    public Guid HoldingId { get; set; }
    public Guid AccountId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Shares { get; set; }
    public decimal AverageCost { get; set; }
    public decimal CurrentPrice { get; set; }
}

/// <summary>
/// Handler for UpdateHoldingCommand.
/// </summary>
public class UpdateHoldingCommandHandler : IRequestHandler<UpdateHoldingCommand, HoldingDto>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<UpdateHoldingCommandHandler> _logger;

    public UpdateHoldingCommandHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<UpdateHoldingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HoldingDto> Handle(UpdateHoldingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating holding: {HoldingId}", request.HoldingId);

        var holding = await _context.Holdings
            .FirstOrDefaultAsync(h => h.HoldingId == request.HoldingId, cancellationToken);

        if (holding == null)
        {
            _logger.LogWarning("Holding not found: {HoldingId}", request.HoldingId);
            throw new KeyNotFoundException($"Holding with ID {request.HoldingId} not found.");
        }

        holding.AccountId = request.AccountId;
        holding.Symbol = request.Symbol;
        holding.Name = request.Name;
        holding.Shares = request.Shares;
        holding.AverageCost = request.AverageCost;
        holding.UpdatePrice(request.CurrentPrice);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Holding updated: {HoldingId}", holding.HoldingId);

        return holding.ToDto();
    }
}
