// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;

namespace MeetingNotesActionItemTracker.Api.Features.Meeting;

public record CreateMeetingCommand(
    Guid UserId,
    string Title,
    DateTime MeetingDateTime,
    int? DurationMinutes,
    string? Location,
    List<string>? Attendees,
    string? Agenda,
    string? Summary
) : IRequest<MeetingDto>;

public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, MeetingDto>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public CreateMeetingCommandHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<MeetingDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = new Core.Meeting
        {
            MeetingId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            MeetingDateTime = request.MeetingDateTime,
            DurationMinutes = request.DurationMinutes,
            Location = request.Location,
            Attendees = request.Attendees ?? new List<string>(),
            Agenda = request.Agenda,
            Summary = request.Summary,
            CreatedAt = DateTime.UtcNow
        };

        _context.Meetings.Add(meeting);
        await _context.SaveChangesAsync(cancellationToken);

        return meeting.ToDto();
    }
}
