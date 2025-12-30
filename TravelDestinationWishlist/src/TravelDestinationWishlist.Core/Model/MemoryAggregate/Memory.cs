// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TravelDestinationWishlist.Core;

public class Memory
{
    public Guid MemoryId { get; set; }
    public Guid UserId { get; set; }
    public Guid TripId { get; set; }
    public Trip? Trip { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime MemoryDate { get; set; } = DateTime.UtcNow;
    public string? PhotoUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
