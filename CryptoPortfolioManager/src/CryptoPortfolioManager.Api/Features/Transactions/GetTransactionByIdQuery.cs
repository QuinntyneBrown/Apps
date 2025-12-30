// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.Transactions;

public class GetTransactionByIdQuery : IRequest<TransactionDto?>
{
    public Guid TransactionId { get; set; }
}

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto?>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public GetTransactionByIdQueryHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<TransactionDto?> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.TransactionId == request.TransactionId, cancellationToken);

        if (transaction == null)
            return null;

        return new TransactionDto
        {
            TransactionId = transaction.TransactionId,
            WalletId = transaction.WalletId,
            TransactionDate = transaction.TransactionDate,
            TransactionType = transaction.TransactionType,
            Symbol = transaction.Symbol,
            Quantity = transaction.Quantity,
            PricePerUnit = transaction.PricePerUnit,
            TotalAmount = transaction.TotalAmount,
            Fees = transaction.Fees,
            Notes = transaction.Notes,
            TotalCost = transaction.CalculateTotalCost()
        };
    }
}
