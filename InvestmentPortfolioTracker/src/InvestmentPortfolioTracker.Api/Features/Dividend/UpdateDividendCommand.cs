// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Dividend;

/// <summary>
/// Command to update an existing dividend.
/// </summary>
public record UpdateDividendCommand : IRequest<DividendDto>
{
    public Guid DividendId { get; set; }
    public Guid HoldingId { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime ExDividendDate { get; set; }
    public decimal AmountPerShare { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsReinvested { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for UpdateDividendCommand.
/// </summary>
public class UpdateDividendCommandHandler : IRequestHandler<UpdateDividendCommand, DividendDto>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<UpdateDividendCommandHandler> _logger;

    public UpdateDividendCommandHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<UpdateDividendCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DividendDto> Handle(UpdateDividendCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating dividend: {DividendId}", request.DividendId);

        var dividend = await _context.Dividends
            .FirstOrDefaultAsync(d => d.DividendId == request.DividendId, cancellationToken);

        if (dividend == null)
        {
            _logger.LogWarning("Dividend not found: {DividendId}", request.DividendId);
            throw new KeyNotFoundException($"Dividend with ID {request.DividendId} not found.");
        }

        dividend.HoldingId = request.HoldingId;
        dividend.PaymentDate = request.PaymentDate;
        dividend.ExDividendDate = request.ExDividendDate;
        dividend.AmountPerShare = request.AmountPerShare;
        dividend.TotalAmount = request.TotalAmount;
        dividend.IsReinvested = request.IsReinvested;
        dividend.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Dividend updated: {DividendId}", dividend.DividendId);

        return dividend.ToDto();
    }
}
