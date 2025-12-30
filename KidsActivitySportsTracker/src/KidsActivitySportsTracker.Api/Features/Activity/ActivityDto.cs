// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;

namespace KidsActivitySportsTracker.Api.Features.Activity;

/// <summary>
/// Data transfer object for Activity.
/// </summary>
public record ActivityDto
{
    public Guid ActivityId { get; init; }
    public Guid UserId { get; init; }
    public string ChildName { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public ActivityType ActivityType { get; init; }
    public string? Organization { get; init; }
    public string? CoachName { get; init; }
    public string? CoachContact { get; init; }
    public string? Season { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for Activity entity.
/// </summary>
public static class ActivityExtensions
{
    /// <summary>
    /// Converts an Activity entity to ActivityDto.
    /// </summary>
    public static ActivityDto ToDto(this Core.Activity activity)
    {
        return new ActivityDto
        {
            ActivityId = activity.ActivityId,
            UserId = activity.UserId,
            ChildName = activity.ChildName,
            Name = activity.Name,
            ActivityType = activity.ActivityType,
            Organization = activity.Organization,
            CoachName = activity.CoachName,
            CoachContact = activity.CoachContact,
            Season = activity.Season,
            StartDate = activity.StartDate,
            EndDate = activity.EndDate,
            Notes = activity.Notes,
            CreatedAt = activity.CreatedAt
        };
    }
}
