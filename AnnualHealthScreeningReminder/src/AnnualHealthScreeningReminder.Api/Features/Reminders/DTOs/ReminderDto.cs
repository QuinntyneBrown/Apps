// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Api.Features.Reminders.DTOs;

public class ReminderDto
{
    public Guid ReminderId { get; set; }
    public Guid UserId { get; set; }
    public Guid ScreeningId { get; set; }
    public DateTime ReminderDate { get; set; }
    public string? Message { get; set; }
    public bool IsSent { get; set; }
    public DateTime CreatedAt { get; set; }
}
