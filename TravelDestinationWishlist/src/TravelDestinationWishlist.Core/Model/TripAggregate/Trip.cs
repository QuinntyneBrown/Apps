// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TravelDestinationWishlist.Core;

public class Trip
{
    public Guid TripId { get; set; }
    public Guid UserId { get; set; }
    public Guid DestinationId { get; set; }
    public Destination? Destination { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? TotalCost { get; set; }
    public string? Accommodation { get; set; }
    public string? Transportation { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Memory> Memories { get; set; } = new List<Memory>();
}
