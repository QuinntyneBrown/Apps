// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;

namespace MeetingNotesActionItemTracker.Api.Features.ActionItem;

public record CreateActionItemCommand(
    Guid UserId,
    Guid MeetingId,
    string Description,
    string? ResponsiblePerson,
    DateTime? DueDate,
    Priority Priority,
    ActionItemStatus Status,
    string? Notes
) : IRequest<ActionItemDto>;

public class CreateActionItemCommandHandler : IRequestHandler<CreateActionItemCommand, ActionItemDto>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public CreateActionItemCommandHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<ActionItemDto> Handle(CreateActionItemCommand request, CancellationToken cancellationToken)
    {
        var actionItem = new Core.ActionItem
        {
            ActionItemId = Guid.NewGuid(),
            UserId = request.UserId,
            MeetingId = request.MeetingId,
            Description = request.Description,
            ResponsiblePerson = request.ResponsiblePerson,
            DueDate = request.DueDate,
            Priority = request.Priority,
            Status = request.Status,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.ActionItems.Add(actionItem);
        await _context.SaveChangesAsync(cancellationToken);

        return actionItem.ToDto();
    }
}
