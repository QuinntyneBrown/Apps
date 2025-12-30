// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Transaction;

/// <summary>
/// Query to get a transaction by ID.
/// </summary>
public record GetTransactionByIdQuery(Guid TransactionId) : IRequest<TransactionDto?>;

/// <summary>
/// Handler for GetTransactionByIdQuery.
/// </summary>
public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto?>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<GetTransactionByIdQueryHandler> _logger;

    public GetTransactionByIdQueryHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<GetTransactionByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TransactionDto?> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting transaction: {TransactionId}", request.TransactionId);

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.TransactionId == request.TransactionId, cancellationToken);

        if (transaction == null)
        {
            _logger.LogWarning("Transaction not found: {TransactionId}", request.TransactionId);
            return null;
        }

        return transaction.ToDto();
    }
}
