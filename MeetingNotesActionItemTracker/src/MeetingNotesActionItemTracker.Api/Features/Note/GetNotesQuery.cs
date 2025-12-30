// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Features.Note;

public record GetNotesQuery : IRequest<List<NoteDto>>;

public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, List<NoteDto>>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public GetNotesQueryHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<NoteDto>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Notes
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => n.ToDto())
            .ToListAsync(cancellationToken);
    }
}
