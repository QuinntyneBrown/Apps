// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Activity;

/// <summary>
/// Command to update an existing activity.
/// </summary>
public record UpdateActivityCommand : IRequest<ActivityDto>
{
    public Guid ActivityId { get; init; }
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
/// Handler for updating an activity.
/// </summary>
public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, ActivityDto>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public UpdateActivityCommandHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<ActivityDto> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await _context.Activities
            .FirstOrDefaultAsync(a => a.ActivityId == request.ActivityId, cancellationToken);

        if (activity == null)
        {
            throw new InvalidOperationException($"Activity with ID {request.ActivityId} not found.");
        }

        activity.ChildName = request.ChildName;
        activity.Name = request.Name;
        activity.ActivityType = request.ActivityType;
        activity.Organization = request.Organization;
        activity.CoachName = request.CoachName;
        activity.CoachContact = request.CoachContact;
        activity.Season = request.Season;
        activity.StartDate = request.StartDate;
        activity.EndDate = request.EndDate;
        activity.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return activity.ToDto();
    }
}
