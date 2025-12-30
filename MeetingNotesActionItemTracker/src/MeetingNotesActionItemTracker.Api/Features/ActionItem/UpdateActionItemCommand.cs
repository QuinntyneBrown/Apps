// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Features.ActionItem;

public record UpdateActionItemCommand(
    Guid ActionItemId,
    string Description,
    string? ResponsiblePerson,
    DateTime? DueDate,
    Priority Priority,
    ActionItemStatus Status,
    string? Notes
) : IRequest<ActionItemDto>;

public class UpdateActionItemCommandHandler : IRequestHandler<UpdateActionItemCommand, ActionItemDto>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public UpdateActionItemCommandHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<ActionItemDto> Handle(UpdateActionItemCommand request, CancellationToken cancellationToken)
    {
        var actionItem = await _context.ActionItems
            .FirstOrDefaultAsync(a => a.ActionItemId == request.ActionItemId, cancellationToken);

        if (actionItem == null)
        {
            throw new InvalidOperationException($"ActionItem with ID {request.ActionItemId} not found");
        }

        actionItem.Description = request.Description;
        actionItem.ResponsiblePerson = request.ResponsiblePerson;
        actionItem.DueDate = request.DueDate;
        actionItem.Priority = request.Priority;
        actionItem.UpdateStatus(request.Status);
        actionItem.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return actionItem.ToDto();
    }
}
