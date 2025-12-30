// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Api.Features.Hands;

public class HandDto
{
    public Guid HandId { get; set; }
    public Guid UserId { get; set; }
    public Guid SessionId { get; set; }
    public string? StartingCards { get; set; }
    public decimal? PotSize { get; set; }
    public bool WasWon { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
