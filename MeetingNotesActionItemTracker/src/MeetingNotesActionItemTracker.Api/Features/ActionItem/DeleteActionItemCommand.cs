// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Features.ActionItem;

public record DeleteActionItemCommand(Guid ActionItemId) : IRequest<Unit>;

public class DeleteActionItemCommandHandler : IRequestHandler<DeleteActionItemCommand, Unit>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public DeleteActionItemCommandHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteActionItemCommand request, CancellationToken cancellationToken)
    {
        var actionItem = await _context.ActionItems
            .FirstOrDefaultAsync(a => a.ActionItemId == request.ActionItemId, cancellationToken);

        if (actionItem == null)
        {
            throw new InvalidOperationException($"ActionItem with ID {request.ActionItemId} not found");
        }

        _context.ActionItems.Remove(actionItem);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
