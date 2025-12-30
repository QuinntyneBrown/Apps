// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Core;

namespace AnnualHealthScreeningReminder.Api.Features.Screenings.DTOs;

public class ScreeningDto
{
    public Guid ScreeningId { get; set; }
    public Guid UserId { get; set; }
    public ScreeningType ScreeningType { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RecommendedFrequencyMonths { get; set; }
    public DateTime? LastScreeningDate { get; set; }
    public DateTime? NextDueDate { get; set; }
    public string? Provider { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDueSoon { get; set; }
}
