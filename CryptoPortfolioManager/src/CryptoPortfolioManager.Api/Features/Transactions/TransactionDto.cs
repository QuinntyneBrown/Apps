// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;

namespace CryptoPortfolioManager.Api.Features.Transactions;

public class TransactionDto
{
    public Guid TransactionId { get; set; }
    public Guid WalletId { get; set; }
    public DateTime TransactionDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? Fees { get; set; }
    public string? Notes { get; set; }
    public decimal TotalCost { get; set; }
}
