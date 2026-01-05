// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core;

/// <summary>
/// Represents a recommendation for a local business or service.
/// </summary>
public class Recommendation
{
    /// <summary>
    /// Gets or sets the unique identifier for the recommendation.
    /// </summary>
    public Guid RecommendationId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the neighbor ID who made this recommendation.
    /// </summary>
    public Guid NeighborId { get; set; }

    /// <summary>
    /// Gets or sets the title of the recommendation.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description or review.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of recommendation.
    /// </summary>
    public RecommendationType RecommendationType { get; set; }

    /// <summary>
    /// Gets or sets the business or service name.
    /// </summary>
    public string? BusinessName { get; set; }

    /// <summary>
    /// Gets or sets the location or address.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the rating (1-5).
    /// </summary>
    public int? Rating { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the neighbor who made this recommendation.
    /// </summary>
    public Neighbor? Neighbor { get; set; }
}
