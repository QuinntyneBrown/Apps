// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Transaction;

/// <summary>
/// Command to update an existing transaction.
/// </summary>
public record UpdateTransactionCommand : IRequest<TransactionDto>
{
    public Guid TransactionId { get; set; }
    public Guid AccountId { get; set; }
    public Guid? HoldingId { get; set; }
    public DateTime TransactionDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public string? Symbol { get; set; }
    public decimal? Shares { get; set; }
    public decimal? PricePerShare { get; set; }
    public decimal Amount { get; set; }
    public decimal? Fees { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for UpdateTransactionCommand.
/// </summary>
public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionDto>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<UpdateTransactionCommandHandler> _logger;

    public UpdateTransactionCommandHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<UpdateTransactionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating transaction: {TransactionId}", request.TransactionId);

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.TransactionId == request.TransactionId, cancellationToken);

        if (transaction == null)
        {
            _logger.LogWarning("Transaction not found: {TransactionId}", request.TransactionId);
            throw new KeyNotFoundException($"Transaction with ID {request.TransactionId} not found.");
        }

        transaction.AccountId = request.AccountId;
        transaction.HoldingId = request.HoldingId;
        transaction.TransactionDate = request.TransactionDate;
        transaction.TransactionType = request.TransactionType;
        transaction.Symbol = request.Symbol;
        transaction.Shares = request.Shares;
        transaction.PricePerShare = request.PricePerShare;
        transaction.Amount = request.Amount;
        transaction.Fees = request.Fees;
        transaction.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Transaction updated: {TransactionId}", transaction.TransactionId);

        return transaction.ToDto();
    }
}
