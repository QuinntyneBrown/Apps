// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VideoGameCollectionManager.Core;

public class Game
{
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public Platform Platform { get; set; }
    public Genre Genre { get; set; }
    public CompletionStatus Status { get; set; }
    public string? Publisher { get; set; }
    public string? Developer { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public int? Rating { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<PlaySession> PlaySessions { get; set; } = new List<PlaySession>();
}
