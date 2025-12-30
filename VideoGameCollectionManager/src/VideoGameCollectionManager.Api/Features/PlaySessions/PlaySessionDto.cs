// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.PlaySessions;

public class PlaySessionDto
{
    public Guid PlaySessionId { get; set; }
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int? DurationMinutes { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public static class PlaySessionDtoExtensions
{
    public static PlaySessionDto ToDto(this PlaySession playSession)
    {
        return new PlaySessionDto
        {
            PlaySessionId = playSession.PlaySessionId,
            UserId = playSession.UserId,
            GameId = playSession.GameId,
            StartTime = playSession.StartTime,
            EndTime = playSession.EndTime,
            DurationMinutes = playSession.DurationMinutes,
            Notes = playSession.Notes,
            CreatedAt = playSession.CreatedAt
        };
    }
}
