// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WineCellarInventory.Core;

public record WineAddedEvent
{
    public Guid WineId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public WineType WineType { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
