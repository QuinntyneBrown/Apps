// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Api.Features.Appointments.DTOs;

public class AppointmentDto
{
    public Guid AppointmentId { get; set; }
    public Guid UserId { get; set; }
    public Guid ScreeningId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public string? Provider { get; set; }
    public bool IsCompleted { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsUpcoming { get; set; }
}
