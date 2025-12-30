// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Core;

public class Screening
{
    public Guid ScreeningId { get; set; }
    public Guid UserId { get; set; }
    public ScreeningType ScreeningType { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RecommendedFrequencyMonths { get; set; } = 12;
    public DateTime? LastScreeningDate { get; set; }
    public DateTime? NextDueDate { get; set; }
    public string? Provider { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    
    public bool IsDueSoon()
    {
        return NextDueDate.HasValue && NextDueDate.Value <= DateTime.UtcNow.AddMonths(1);
    }
}
