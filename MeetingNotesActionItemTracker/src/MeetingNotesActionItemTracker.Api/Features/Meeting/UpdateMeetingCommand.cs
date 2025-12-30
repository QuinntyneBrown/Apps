// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Features.Meeting;

public record UpdateMeetingCommand(
    Guid MeetingId,
    string Title,
    DateTime MeetingDateTime,
    int? DurationMinutes,
    string? Location,
    List<string>? Attendees,
    string? Agenda,
    string? Summary
) : IRequest<MeetingDto>;

public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand, MeetingDto>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public UpdateMeetingCommandHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<MeetingDto> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _context.Meetings
            .FirstOrDefaultAsync(m => m.MeetingId == request.MeetingId, cancellationToken);

        if (meeting == null)
        {
            throw new InvalidOperationException($"Meeting with ID {request.MeetingId} not found");
        }

        meeting.Title = request.Title;
        meeting.MeetingDateTime = request.MeetingDateTime;
        meeting.DurationMinutes = request.DurationMinutes;
        meeting.Location = request.Location;
        meeting.Attendees = request.Attendees ?? new List<string>();
        meeting.Agenda = request.Agenda;
        meeting.Summary = request.Summary;
        meeting.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return meeting.ToDto();
    }
}
