// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.Transactions;

public class GetTransactionsQuery : IRequest<List<TransactionDto>>
{
    public Guid? WalletId { get; set; }
}

public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public GetTransactionsQueryHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Transactions.AsQueryable();

        if (request.WalletId.HasValue)
        {
            query = query.Where(t => t.WalletId == request.WalletId.Value);
        }

        return await query
            .OrderByDescending(t => t.TransactionDate)
            .Select(t => new TransactionDto
            {
                TransactionId = t.TransactionId,
                WalletId = t.WalletId,
                TransactionDate = t.TransactionDate,
                TransactionType = t.TransactionType,
                Symbol = t.Symbol,
                Quantity = t.Quantity,
                PricePerUnit = t.PricePerUnit,
                TotalAmount = t.TotalAmount,
                Fees = t.Fees,
                Notes = t.Notes,
                TotalCost = t.CalculateTotalCost()
            })
            .ToListAsync(cancellationToken);
    }
}
