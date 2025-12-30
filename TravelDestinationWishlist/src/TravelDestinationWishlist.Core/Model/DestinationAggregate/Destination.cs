// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TravelDestinationWishlist.Core;

public class Destination
{
    public Guid DestinationId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public DestinationType DestinationType { get; set; }
    public string? Description { get; set; }
    public int Priority { get; set; } = 3;
    public bool IsVisited { get; set; }
    public DateTime? VisitedDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
