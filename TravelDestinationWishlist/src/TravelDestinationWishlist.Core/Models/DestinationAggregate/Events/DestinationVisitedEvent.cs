// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TravelDestinationWishlist.Core;

public record DestinationVisitedEvent
{
    public Guid DestinationId { get; init; }
    public Guid UserId { get; init; }
    public DateTime VisitedDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
