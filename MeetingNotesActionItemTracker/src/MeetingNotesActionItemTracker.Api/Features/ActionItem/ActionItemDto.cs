// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MeetingNotesActionItemTracker.Core;

namespace MeetingNotesActionItemTracker.Api.Features.ActionItem;

public record ActionItemDto(
    Guid ActionItemId,
    Guid UserId,
    Guid MeetingId,
    string Description,
    string? ResponsiblePerson,
    DateTime? DueDate,
    Priority Priority,
    ActionItemStatus Status,
    DateTime? CompletedDate,
    string? Notes,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public static class ActionItemExtensions
{
    public static ActionItemDto ToDto(this Core.ActionItem actionItem)
    {
        return new ActionItemDto(
            actionItem.ActionItemId,
            actionItem.UserId,
            actionItem.MeetingId,
            actionItem.Description,
            actionItem.ResponsiblePerson,
            actionItem.DueDate,
            actionItem.Priority,
            actionItem.Status,
            actionItem.CompletedDate,
            actionItem.Notes,
            actionItem.CreatedAt,
            actionItem.UpdatedAt
        );
    }
}
