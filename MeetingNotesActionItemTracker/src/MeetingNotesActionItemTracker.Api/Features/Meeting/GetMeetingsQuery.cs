// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Features.Meeting;

public record GetMeetingsQuery : IRequest<List<MeetingDto>>;

public class GetMeetingsQueryHandler : IRequestHandler<GetMeetingsQuery, List<MeetingDto>>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public GetMeetingsQueryHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<MeetingDto>> Handle(GetMeetingsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Meetings
            .OrderByDescending(m => m.MeetingDateTime)
            .Select(m => m.ToDto())
            .ToListAsync(cancellationToken);
    }
}
