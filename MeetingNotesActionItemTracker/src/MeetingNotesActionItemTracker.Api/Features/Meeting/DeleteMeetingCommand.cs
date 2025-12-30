// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Features.Meeting;

public record DeleteMeetingCommand(Guid MeetingId) : IRequest<Unit>;

public class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand, Unit>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public DeleteMeetingCommandHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _context.Meetings
            .FirstOrDefaultAsync(m => m.MeetingId == request.MeetingId, cancellationToken);

        if (meeting == null)
        {
            throw new InvalidOperationException($"Meeting with ID {request.MeetingId} not found");
        }

        _context.Meetings.Remove(meeting);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
