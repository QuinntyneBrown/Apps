// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WineCellarInventory.Core;

public record TastingNoteCreatedEvent
{
    public Guid TastingNoteId { get; init; }
    public Guid UserId { get; init; }
    public Guid WineId { get; init; }
    public int Rating { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
