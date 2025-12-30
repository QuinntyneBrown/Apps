// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.WeeklyReviews;

/// <summary>
/// Command to create a new weekly review.
/// </summary>
public class CreateWeeklyReviewCommand : IRequest<WeeklyReviewDto>
{
    public Guid UserId { get; set; }
    public DateTime WeekStartDate { get; set; }
    public DateTime WeekEndDate { get; set; }
    public int? OverallRating { get; set; }
    public string? Reflections { get; set; }
    public string? LessonsLearned { get; set; }
    public string? Gratitude { get; set; }
    public string? ImprovementAreas { get; set; }
}

/// <summary>
/// Handler for CreateWeeklyReviewCommand.
/// </summary>
public class CreateWeeklyReviewCommandHandler : IRequestHandler<CreateWeeklyReviewCommand, WeeklyReviewDto>
{
    private readonly IWeeklyReviewSystemContext _context;

    public CreateWeeklyReviewCommandHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<WeeklyReviewDto> Handle(CreateWeeklyReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = request.UserId,
            WeekStartDate = request.WeekStartDate,
            WeekEndDate = request.WeekEndDate,
            OverallRating = request.OverallRating,
            Reflections = request.Reflections,
            LessonsLearned = request.LessonsLearned,
            Gratitude = request.Gratitude,
            ImprovementAreas = request.ImprovementAreas,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        return review.ToDto();
    }
}
