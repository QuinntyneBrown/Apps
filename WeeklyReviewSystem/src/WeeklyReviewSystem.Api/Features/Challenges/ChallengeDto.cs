// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Challenges;

/// <summary>
/// Data transfer object for Challenge.
/// </summary>
public class ChallengeDto
{
    public Guid ChallengeId { get; set; }
    public Guid WeeklyReviewId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Resolution { get; set; }
    public bool IsResolved { get; set; }
    public string? LessonsLearned { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Extension methods for Challenge entity.
/// </summary>
public static class ChallengeExtensions
{
    /// <summary>
    /// Converts a Challenge entity to a DTO.
    /// </summary>
    public static ChallengeDto ToDto(this Challenge challenge)
    {
        return new ChallengeDto
        {
            ChallengeId = challenge.ChallengeId,
            WeeklyReviewId = challenge.WeeklyReviewId,
            Title = challenge.Title,
            Description = challenge.Description,
            Resolution = challenge.Resolution,
            IsResolved = challenge.IsResolved,
            LessonsLearned = challenge.LessonsLearned,
            CreatedAt = challenge.CreatedAt
        };
    }
}
