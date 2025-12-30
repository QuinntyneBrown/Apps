// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Core;

public record SessionEndedEvent
{
    public Guid SessionId { get; init; }
    public Guid UserId { get; init; }
    public decimal CashOut { get; init; }
    public decimal Profit { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
