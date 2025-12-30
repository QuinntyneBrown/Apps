// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Core;

public class Reminder
{
    public Guid ReminderId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid ScreeningId { get; set; }
    public DateTime ReminderDate { get; set; }
    public string? Message { get; set; }
    public bool IsSent { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public void MarkAsSent()
    {
        IsSent = true;
    }
}
