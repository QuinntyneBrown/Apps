// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Core;

public class Team
{
    public Guid TeamId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Sport Sport { get; set; }
    public string? League { get; set; }
    public string? City { get; set; }
    public bool IsFavorite { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Game> Games { get; set; } = new List<Game>();
}
