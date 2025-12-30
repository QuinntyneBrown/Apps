// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.WeeklyReviews;

/// <summary>
/// Command to delete a weekly review.
/// </summary>
public class DeleteWeeklyReviewCommand : IRequest<bool>
{
    public Guid WeeklyReviewId { get; set; }
}

/// <summary>
/// Handler for DeleteWeeklyReviewCommand.
/// </summary>
public class DeleteWeeklyReviewCommandHandler : IRequestHandler<DeleteWeeklyReviewCommand, bool>
{
    private readonly IWeeklyReviewSystemContext _context;

    public DeleteWeeklyReviewCommandHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteWeeklyReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.WeeklyReviewId == request.WeeklyReviewId, cancellationToken);

        if (review == null)
        {
            return false;
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
