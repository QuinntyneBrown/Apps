// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Dividend;

/// <summary>
/// Command to create a new dividend.
/// </summary>
public record CreateDividendCommand : IRequest<DividendDto>
{
    public Guid HoldingId { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime ExDividendDate { get; set; }
    public decimal AmountPerShare { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsReinvested { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for CreateDividendCommand.
/// </summary>
public class CreateDividendCommandHandler : IRequestHandler<CreateDividendCommand, DividendDto>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<CreateDividendCommandHandler> _logger;

    public CreateDividendCommandHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<CreateDividendCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DividendDto> Handle(CreateDividendCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new dividend for holding: {HoldingId}", request.HoldingId);

        var dividend = new Core.Dividend
        {
            DividendId = Guid.NewGuid(),
            HoldingId = request.HoldingId,
            PaymentDate = request.PaymentDate,
            ExDividendDate = request.ExDividendDate,
            AmountPerShare = request.AmountPerShare,
            TotalAmount = request.TotalAmount,
            IsReinvested = request.IsReinvested,
            Notes = request.Notes
        };

        _context.Dividends.Add(dividend);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Dividend created with ID: {DividendId}", dividend.DividendId);

        return dividend.ToDto();
    }
}
