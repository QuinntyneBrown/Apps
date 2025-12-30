// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.WeeklyReviews;

/// <summary>
/// Command to update an existing weekly review.
/// </summary>
public class UpdateWeeklyReviewCommand : IRequest<WeeklyReviewDto?>
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
}

/// <summary>
/// Handler for UpdateWeeklyReviewCommand.
/// </summary>
public class UpdateWeeklyReviewCommandHandler : IRequestHandler<UpdateWeeklyReviewCommand, WeeklyReviewDto?>
{
    private readonly IWeeklyReviewSystemContext _context;

    public UpdateWeeklyReviewCommandHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<WeeklyReviewDto?> Handle(UpdateWeeklyReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.WeeklyReviewId == request.WeeklyReviewId, cancellationToken);

        if (review == null)
        {
            return null;
        }

        review.UserId = request.UserId;
        review.WeekStartDate = request.WeekStartDate;
        review.WeekEndDate = request.WeekEndDate;
        review.OverallRating = request.OverallRating;
        review.Reflections = request.Reflections;
        review.LessonsLearned = request.LessonsLearned;
        review.Gratitude = request.Gratitude;
        review.ImprovementAreas = request.ImprovementAreas;
        review.IsCompleted = request.IsCompleted;

        await _context.SaveChangesAsync(cancellationToken);

        return review.ToDto();
    }
}
