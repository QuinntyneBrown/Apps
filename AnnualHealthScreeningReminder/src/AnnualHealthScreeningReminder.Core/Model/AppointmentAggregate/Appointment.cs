// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Core;

public class Appointment
{
    public Guid AppointmentId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid ScreeningId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public string? Provider { get; set; }
    public bool IsCompleted { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Screening? Screening { get; set; }
    
    public bool IsUpcoming()
    {
        return !IsCompleted && AppointmentDate > DateTime.UtcNow;
    }
}
