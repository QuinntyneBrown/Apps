// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.WeeklyReviews;

/// <summary>
/// Data transfer object for WeeklyReview.
/// </summary>
public class WeeklyReviewDto
{
    public Guid WeeklyReviewId { get; set; }
    public Guid UserId { get; set; }
    public DateTime WeekStartDate { get; set; }
    public DateTime WeekEndDate { get; set; }
    public int? OverallRating { get; set; }
    public string? Reflections { get; set; }
    public string? LessonsLearned { get; set; }
    public string? Gratitude { get; set; }
    public string? ImprovementAreas { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Extension methods for WeeklyReview entity.
/// </summary>
public static class WeeklyReviewExtensions
{
    /// <summary>
    /// Converts a WeeklyReview entity to a DTO.
    /// </summary>
    public static WeeklyReviewDto ToDto(this WeeklyReview review)
    {
        return new WeeklyReviewDto
        {
            WeeklyReviewId = review.WeeklyReviewId,
            UserId = review.UserId,
            WeekStartDate = review.WeekStartDate,
            WeekEndDate = review.WeekEndDate,
            OverallRating = review.OverallRating,
            Reflections = review.Reflections,
            LessonsLearned = review.LessonsLearned,
            Gratitude = review.Gratitude,
            ImprovementAreas = review.ImprovementAreas,
            IsCompleted = review.IsCompleted,
            CreatedAt = review.CreatedAt
        };
    }
}
