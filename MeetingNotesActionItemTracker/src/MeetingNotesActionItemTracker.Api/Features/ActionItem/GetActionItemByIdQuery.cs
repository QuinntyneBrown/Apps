// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Features.ActionItem;

public record GetActionItemByIdQuery(Guid ActionItemId) : IRequest<ActionItemDto?>;

public class GetActionItemByIdQueryHandler : IRequestHandler<GetActionItemByIdQuery, ActionItemDto?>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public GetActionItemByIdQueryHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<ActionItemDto?> Handle(GetActionItemByIdQuery request, CancellationToken cancellationToken)
    {
        var actionItem = await _context.ActionItems
            .FirstOrDefaultAsync(a => a.ActionItemId == request.ActionItemId, cancellationToken);

        return actionItem?.ToDto();
    }
}
