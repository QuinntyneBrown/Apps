// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Features.ActionItem;

public record GetActionItemsQuery : IRequest<List<ActionItemDto>>;

public class GetActionItemsQueryHandler : IRequestHandler<GetActionItemsQuery, List<ActionItemDto>>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public GetActionItemsQueryHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<ActionItemDto>> Handle(GetActionItemsQuery request, CancellationToken cancellationToken)
    {
        return await _context.ActionItems
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => a.ToDto())
            .ToListAsync(cancellationToken);
    }
}
