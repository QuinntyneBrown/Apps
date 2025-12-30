// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MeetingNotesActionItemTracker.Core;

namespace MeetingNotesActionItemTracker.Api.Features.Meeting;

public record MeetingDto(
    Guid MeetingId,
    Guid UserId,
    string Title,
    DateTime MeetingDateTime,
    int? DurationMinutes,
    string? Location,
    List<string> Attendees,
    string? Agenda,
    string? Summary,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public static class MeetingExtensions
{
    public static MeetingDto ToDto(this Core.Meeting meeting)
    {
        return new MeetingDto(
            meeting.MeetingId,
            meeting.UserId,
            meeting.Title,
            meeting.MeetingDateTime,
            meeting.DurationMinutes,
            meeting.Location,
            meeting.Attendees,
            meeting.Agenda,
            meeting.Summary,
            meeting.CreatedAt,
            meeting.UpdatedAt
        );
    }
}
