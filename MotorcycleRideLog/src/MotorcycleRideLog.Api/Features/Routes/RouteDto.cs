// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;

namespace MotorcycleRideLog.Api.Features.Routes;

public class RouteDto
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

    public static RouteDto FromEntity(Core.Route route)
    {
        return new RouteDto
        {
            RouteId = route.RouteId,
            UserId = route.UserId,
            Name = route.Name,
            Description = route.Description,
            StartLocation = route.StartLocation,
            EndLocation = route.EndLocation,
            DistanceMiles = route.DistanceMiles,
            Waypoints = route.Waypoints,
            EstimatedDurationMinutes = route.EstimatedDurationMinutes,
            Difficulty = route.Difficulty,
            IsFavorite = route.IsFavorite,
            Notes = route.Notes,
            CreatedAt = route.CreatedAt
        };
    }
}
