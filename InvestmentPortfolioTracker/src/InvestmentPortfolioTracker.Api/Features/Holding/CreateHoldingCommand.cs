// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Holding;

/// <summary>
/// Command to create a new holding.
/// </summary>
public record CreateHoldingCommand : IRequest<HoldingDto>
{
    public Guid AccountId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Shares { get; set; }
    public decimal AverageCost { get; set; }
    public decimal CurrentPrice { get; set; }
}

/// <summary>
/// Handler for CreateHoldingCommand.
/// </summary>
public class CreateHoldingCommandHandler : IRequestHandler<CreateHoldingCommand, HoldingDto>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<CreateHoldingCommandHandler> _logger;

    public CreateHoldingCommandHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<CreateHoldingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HoldingDto> Handle(CreateHoldingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new holding: {Symbol}", request.Symbol);

        var holding = new Core.Holding
        {
            HoldingId = Guid.NewGuid(),
            AccountId = request.AccountId,
            Symbol = request.Symbol,
            Name = request.Name,
            Shares = request.Shares,
            AverageCost = request.AverageCost,
            CurrentPrice = request.CurrentPrice,
            LastPriceUpdate = DateTime.UtcNow
        };

        _context.Holdings.Add(holding);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Holding created with ID: {HoldingId}", holding.HoldingId);

        return holding.ToDto();
    }
}
