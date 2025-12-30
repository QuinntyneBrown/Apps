// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Activity;

/// <summary>
/// Command to create a new activity.
/// </summary>
public record CreateActivityCommand : IRequest<ActivityDto>
{
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
}

/// <summary>
/// Handler for creating a new activity.
/// </summary>
public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, ActivityDto>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public CreateActivityCommandHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<ActivityDto> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = new Core.Activity
        {
            ActivityId = Guid.NewGuid(),
            UserId = request.UserId,
            ChildName = request.ChildName,
            Name = request.Name,
            ActivityType = request.ActivityType,
            Organization = request.Organization,
            CoachName = request.CoachName,
            CoachContact = request.CoachContact,
            Season = request.Season,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Activities.Add(activity);
        await _context.SaveChangesAsync(cancellationToken);

        return activity.ToDto();
    }
}
