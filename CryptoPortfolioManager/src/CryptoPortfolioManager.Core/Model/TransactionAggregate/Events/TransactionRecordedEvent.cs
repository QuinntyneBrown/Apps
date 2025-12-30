// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CryptoPortfolioManager.Core;

public record TransactionRecordedEvent
{
    public Guid TransactionId { get; init; }
    public Guid WalletId { get; init; }
    public TransactionType TransactionType { get; init; }
    public decimal TotalAmount { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
