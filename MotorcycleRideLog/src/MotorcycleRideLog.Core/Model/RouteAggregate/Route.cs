// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MotorcycleRideLog.Core;

public class Route
{
    public Guid RouteId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? StartLocation { get; set; }
    public string? EndLocation { get; set; }
    public decimal? DistanceMiles { get; set; }
    public string? Waypoints { get; set; }
    public int? EstimatedDurationMinutes { get; set; }
    public string? Difficulty { get; set; }
    public bool IsFavorite { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
