// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core;

public class Itinerary
{
    public Guid ItineraryId { get; set; }
    public Guid TripId { get; set; }
    public Trip? Trip { get; set; }
    public DateTime Date { get; set; }
    public string? Activity { get; set; }
    public string? Location { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
