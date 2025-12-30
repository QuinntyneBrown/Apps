// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Transaction;

/// <summary>
/// Command to delete a transaction.
/// </summary>
public record DeleteTransactionCommand(Guid TransactionId) : IRequest<Unit>;

/// <summary>
/// Handler for DeleteTransactionCommand.
/// </summary>
public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Unit>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<DeleteTransactionCommandHandler> _logger;

    public DeleteTransactionCommandHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<DeleteTransactionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting transaction: {TransactionId}", request.TransactionId);

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.TransactionId == request.TransactionId, cancellationToken);

        if (transaction == null)
        {
            _logger.LogWarning("Transaction not found: {TransactionId}", request.TransactionId);
            throw new KeyNotFoundException($"Transaction with ID {request.TransactionId} not found.");
        }

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Transaction deleted: {TransactionId}", request.TransactionId);

        return Unit.Value;
    }
}
