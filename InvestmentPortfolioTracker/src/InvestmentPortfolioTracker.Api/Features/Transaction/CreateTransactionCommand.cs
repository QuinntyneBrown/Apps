// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Transaction;

/// <summary>
/// Command to create a new transaction.
/// </summary>
public record CreateTransactionCommand : IRequest<TransactionDto>
{
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
/// Handler for CreateTransactionCommand.
/// </summary>
public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<CreateTransactionCommandHandler> _logger;

    public CreateTransactionCommandHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<CreateTransactionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new transaction: {TransactionType}", request.TransactionType);

        var transaction = new Core.Transaction
        {
            TransactionId = Guid.NewGuid(),
            AccountId = request.AccountId,
            HoldingId = request.HoldingId,
            TransactionDate = request.TransactionDate,
            TransactionType = request.TransactionType,
            Symbol = request.Symbol,
            Shares = request.Shares,
            PricePerShare = request.PricePerShare,
            Amount = request.Amount,
            Fees = request.Fees,
            Notes = request.Notes
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Transaction created with ID: {TransactionId}", transaction.TransactionId);

        return transaction.ToDto();
    }
}
