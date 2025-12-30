// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core;

public class Booking
{
    public Guid BookingId { get; set; }
    public Guid TripId { get; set; }
    public Trip? Trip { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? ConfirmationNumber { get; set; }
    public decimal? Cost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
