// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Activity;

/// <summary>
/// Command to delete an activity.
/// </summary>
public record DeleteActivityCommand : IRequest<Unit>
{
    public Guid ActivityId { get; init; }
}

/// <summary>
/// Handler for deleting an activity.
/// </summary>
public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Unit>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public DeleteActivityCommandHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await _context.Activities
            .FirstOrDefaultAsync(a => a.ActivityId == request.ActivityId, cancellationToken);

        if (activity == null)
        {
            throw new InvalidOperationException($"Activity with ID {request.ActivityId} not found.");
        }

        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
