// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MorningRoutineBuilder.Api.Features.Streaks;

/// <summary>
/// Data transfer object for Streak.
/// </summary>
public record StreakDto
{
    public Guid StreakId { get; set; }
    public Guid RoutineId { get; set; }
    public Guid UserId { get; set; }
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }
    public DateTime? LastCompletionDate { get; set; }
    public DateTime? StreakStartDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Request to create a new streak.
/// </summary>
public record CreateStreakRequest
{
    public Guid RoutineId { get; set; }
    public Guid UserId { get; set; }
}

/// <summary>
/// Request to update an existing streak.
/// </summary>
public record UpdateStreakRequest
{
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }
    public DateTime? LastCompletionDate { get; set; }
    public DateTime? StreakStartDate { get; set; }
    public bool IsActive { get; set; }
}
