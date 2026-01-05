// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Core;

public class Campsite
{
    public Guid CampsiteId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public CampsiteType CampsiteType { get; set; }
    public string? Description { get; set; }
    public bool HasElectricity { get; set; }
    public bool HasWater { get; set; }
    public decimal? CostPerNight { get; set; }
    public bool IsFavorite { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
