// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Challenges;

/// <summary>
/// Command to create a new challenge.
/// </summary>
public class CreateChallengeCommand : IRequest<ChallengeDto>
{
    public Guid WeeklyReviewId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Resolution { get; set; }
    public bool IsResolved { get; set; }
    public string? LessonsLearned { get; set; }
}

/// <summary>
/// Handler for CreateChallengeCommand.
/// </summary>
public class CreateChallengeCommandHandler : IRequestHandler<CreateChallengeCommand, ChallengeDto>
{
    private readonly IWeeklyReviewSystemContext _context;

    public CreateChallengeCommandHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<ChallengeDto> Handle(CreateChallengeCommand request, CancellationToken cancellationToken)
    {
        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = request.WeeklyReviewId,
            Title = request.Title,
            Description = request.Description,
            Resolution = request.Resolution,
            IsResolved = request.IsResolved,
            LessonsLearned = request.LessonsLearned,
            CreatedAt = DateTime.UtcNow
        };

        _context.Challenges.Add(challenge);
        await _context.SaveChangesAsync(cancellationToken);

        return challenge.ToDto();
    }
}
