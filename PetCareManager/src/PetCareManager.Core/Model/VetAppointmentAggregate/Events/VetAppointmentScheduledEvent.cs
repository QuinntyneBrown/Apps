// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core;

public record VetAppointmentScheduledEvent
{
    public Guid VetAppointmentId { get; init; }
    public Guid PetId { get; init; }
    public DateTime AppointmentDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
