// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Core;

public record AppointmentBookedEvent
{
    public Guid AppointmentId { get; init; }
    public Guid ScreeningId { get; init; }
    public DateTime AppointmentDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
