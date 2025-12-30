// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WineCellarInventory.Core;

public record DrinkingWindowSetEvent
{
    public Guid DrinkingWindowId { get; init; }
    public Guid UserId { get; init; }
    public Guid WineId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
