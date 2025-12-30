// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.Transactions;

public class UpdateTransactionCommand : IRequest<TransactionDto?>
{
    public Guid TransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? Fees { get; set; }
    public string? Notes { get; set; }
}

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionDto?>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public UpdateTransactionCommandHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<TransactionDto?> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.TransactionId == request.TransactionId, cancellationToken);

        if (transaction == null)
            return null;

        transaction.TransactionDate = request.TransactionDate;
        transaction.TransactionType = request.TransactionType;
        transaction.Symbol = request.Symbol;
        transaction.Quantity = request.Quantity;
        transaction.PricePerUnit = request.PricePerUnit;
        transaction.TotalAmount = request.TotalAmount;
        transaction.Fees = request.Fees;
        transaction.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

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
