// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core;

public class Trip
{
    public Guid TripId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Destination { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<VacationBudget> Budgets { get; set; } = new List<VacationBudget>();
    public ICollection<PackingList> PackingLists { get; set; } = new List<PackingList>();
}
