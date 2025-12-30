// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Challenges;

/// <summary>
/// Command to update an existing challenge.
/// </summary>
public class UpdateChallengeCommand : IRequest<ChallengeDto?>
{
    public Guid ChallengeId { get; set; }
    public Guid WeeklyReviewId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Resolution { get; set; }
    public bool IsResolved { get; set; }
    public string? LessonsLearned { get; set; }
}

/// <summary>
/// Handler for UpdateChallengeCommand.
/// </summary>
public class UpdateChallengeCommandHandler : IRequestHandler<UpdateChallengeCommand, ChallengeDto?>
{
    private readonly IWeeklyReviewSystemContext _context;

    public UpdateChallengeCommandHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<ChallengeDto?> Handle(UpdateChallengeCommand request, CancellationToken cancellationToken)
    {
        var challenge = await _context.Challenges
            .FirstOrDefaultAsync(c => c.ChallengeId == request.ChallengeId, cancellationToken);

        if (challenge == null)
        {
            return null;
        }

        challenge.WeeklyReviewId = request.WeeklyReviewId;
        challenge.Title = request.Title;
        challenge.Description = request.Description;
        challenge.Resolution = request.Resolution;
        challenge.IsResolved = request.IsResolved;
        challenge.LessonsLearned = request.LessonsLearned;

        await _context.SaveChangesAsync(cancellationToken);

        return challenge.ToDto();
    }
}
