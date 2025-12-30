// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Api.Features.Sessions;

public class SessionDto
{
    public Guid SessionId { get; set; }
    public Guid UserId { get; set; }
    public int GameType { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public decimal BuyIn { get; set; }
    public decimal? CashOut { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
