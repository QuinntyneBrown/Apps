// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Accomplishments;

/// <summary>
/// Data transfer object for Accomplishment.
/// </summary>
public class AccomplishmentDto
{
    public Guid AccomplishmentId { get; set; }
    public Guid WeeklyReviewId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public int? ImpactLevel { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Extension methods for Accomplishment entity.
/// </summary>
public static class AccomplishmentExtensions
{
    /// <summary>
    /// Converts an Accomplishment entity to a DTO.
    /// </summary>
    public static AccomplishmentDto ToDto(this Accomplishment accomplishment)
    {
        return new AccomplishmentDto
        {
            AccomplishmentId = accomplishment.AccomplishmentId,
            WeeklyReviewId = accomplishment.WeeklyReviewId,
            Title = accomplishment.Title,
            Description = accomplishment.Description,
            Category = accomplishment.Category,
            ImpactLevel = accomplishment.ImpactLevel,
            CreatedAt = accomplishment.CreatedAt
        };
    }
}
