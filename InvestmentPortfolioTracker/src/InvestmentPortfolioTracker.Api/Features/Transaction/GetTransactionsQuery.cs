// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Api.Features.Transaction;

/// <summary>
/// Query to get all transactions.
/// </summary>
public record GetTransactionsQuery : IRequest<List<TransactionDto>>;

/// <summary>
/// Handler for GetTransactionsQuery.
/// </summary>
public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
{
    private readonly IInvestmentPortfolioTrackerContext _context;
    private readonly ILogger<GetTransactionsQueryHandler> _logger;

    public GetTransactionsQueryHandler(
        IInvestmentPortfolioTrackerContext context,
        ILogger<GetTransactionsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all transactions");

        var transactions = await _context.Transactions
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Retrieved {Count} transactions", transactions.Count);

        return transactions.Select(t => t.ToDto()).ToList();
    }
}
