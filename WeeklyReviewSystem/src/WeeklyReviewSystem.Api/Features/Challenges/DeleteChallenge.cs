// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Challenges;

/// <summary>
/// Command to delete a challenge.
/// </summary>
public class DeleteChallengeCommand : IRequest<bool>
{
    public Guid ChallengeId { get; set; }
}

/// <summary>
/// Handler for DeleteChallengeCommand.
/// </summary>
public class DeleteChallengeCommandHandler : IRequestHandler<DeleteChallengeCommand, bool>
{
    private readonly IWeeklyReviewSystemContext _context;

    public DeleteChallengeCommandHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteChallengeCommand request, CancellationToken cancellationToken)
    {
        var challenge = await _context.Challenges
            .FirstOrDefaultAsync(c => c.ChallengeId == request.ChallengeId, cancellationToken);

        if (challenge == null)
        {
            return false;
        }

        _context.Challenges.Remove(challenge);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
