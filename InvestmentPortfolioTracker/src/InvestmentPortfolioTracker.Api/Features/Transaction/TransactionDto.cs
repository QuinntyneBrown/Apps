// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;

namespace InvestmentPortfolioTracker.Api.Features.Transaction;

/// <summary>
/// Data transfer object for Transaction.
/// </summary>
public record TransactionDto
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
    public decimal TotalCost { get; set; }
}

/// <summary>
/// Extension methods for Transaction entity.
/// </summary>
public static class TransactionExtensions
{
    /// <summary>
    /// Converts a Transaction entity to TransactionDto.
    /// </summary>
    public static TransactionDto ToDto(this Core.Transaction transaction)
    {
        return new TransactionDto
        {
            TransactionId = transaction.TransactionId,
            AccountId = transaction.AccountId,
            HoldingId = transaction.HoldingId,
            TransactionDate = transaction.TransactionDate,
            TransactionType = transaction.TransactionType,
            Symbol = transaction.Symbol,
            Shares = transaction.Shares,
            PricePerShare = transaction.PricePerShare,
            Amount = transaction.Amount,
            Fees = transaction.Fees,
            Notes = transaction.Notes,
            TotalCost = transaction.CalculateTotalCost()
        };
    }
}
