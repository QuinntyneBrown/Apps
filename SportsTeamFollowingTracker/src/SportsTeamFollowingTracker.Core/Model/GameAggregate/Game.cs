// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Core;

public class Game
{
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
    public DateTime GameDate { get; set; }
    public string Opponent { get; set; } = string.Empty;
    public int? TeamScore { get; set; }
    public int? OpponentScore { get; set; }
    public bool? IsWin { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
