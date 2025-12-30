// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoPortfolioManager.Api.Features.Transactions;

public class CreateTransactionCommand : IRequest<TransactionDto>
{
    public Guid WalletId { get; set; }
    public DateTime TransactionDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? Fees { get; set; }
    public string? Notes { get; set; }
}

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    private readonly ICryptoPortfolioManagerContext _context;

    public CreateTransactionCommandHandler(ICryptoPortfolioManagerContext context)
    {
        _context = context;
    }

    public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = new Transaction
        {
            TransactionId = Guid.NewGuid(),
            WalletId = request.WalletId,
            TransactionDate = request.TransactionDate,
            TransactionType = request.TransactionType,
            Symbol = request.Symbol,
            Quantity = request.Quantity,
            PricePerUnit = request.PricePerUnit,
            TotalAmount = request.TotalAmount,
            Fees = request.Fees,
            Notes = request.Notes
        };

        _context.Transactions.Add(transaction);
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
