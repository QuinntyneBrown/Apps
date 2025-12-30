// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Features.Meeting;

public record GetMeetingByIdQuery(Guid MeetingId) : IRequest<MeetingDto?>;

public class GetMeetingByIdQueryHandler : IRequestHandler<GetMeetingByIdQuery, MeetingDto?>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public GetMeetingByIdQueryHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<MeetingDto?> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        var meeting = await _context.Meetings
            .FirstOrDefaultAsync(m => m.MeetingId == request.MeetingId, cancellationToken);

        return meeting?.ToDto();
    }
}
