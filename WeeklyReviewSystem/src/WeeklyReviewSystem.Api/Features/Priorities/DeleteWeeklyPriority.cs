// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Priorities;

/// <summary>
/// Command to delete a weekly priority.
/// </summary>
public class DeleteWeeklyPriorityCommand : IRequest<bool>
{
    public Guid WeeklyPriorityId { get; set; }
}

/// <summary>
/// Handler for DeleteWeeklyPriorityCommand.
/// </summary>
public class DeleteWeeklyPriorityCommandHandler : IRequestHandler<DeleteWeeklyPriorityCommand, bool>
{
    private readonly IWeeklyReviewSystemContext _context;

    public DeleteWeeklyPriorityCommandHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteWeeklyPriorityCommand request, CancellationToken cancellationToken)
    {
        var priority = await _context.Priorities
            .FirstOrDefaultAsync(p => p.WeeklyPriorityId == request.WeeklyPriorityId, cancellationToken);

        if (priority == null)
        {
            return false;
        }

        _context.Priorities.Remove(priority);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
