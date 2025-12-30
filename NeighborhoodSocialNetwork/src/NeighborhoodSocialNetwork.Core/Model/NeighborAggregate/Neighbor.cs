// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core;

/// <summary>
/// Represents a neighbor in the social network.
/// </summary>
public class Neighbor
{
    /// <summary>
    /// Gets or sets the unique identifier for the neighbor.
    /// </summary>
    public Guid NeighborId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the neighbor.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address or location.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the contact information.
    /// </summary>
    public string? ContactInfo { get; set; }

    /// <summary>
    /// Gets or sets bio or description.
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// Gets or sets interests or hobbies.
    /// </summary>
    public string? Interests { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this neighbor profile is verified.
    /// </summary>
    public bool IsVerified { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of recommendations by this neighbor.
    /// </summary>
    public ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

    /// <summary>
    /// Gets or sets the collection of messages sent by this neighbor.
    /// </summary>
    public ICollection<Message> SentMessages { get; set; } = new List<Message>();

    /// <summary>
    /// Verifies the neighbor profile.
    /// </summary>
    public void Verify()
    {
        IsVerified = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
