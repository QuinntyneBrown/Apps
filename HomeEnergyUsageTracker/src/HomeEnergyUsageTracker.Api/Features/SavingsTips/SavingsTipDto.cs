// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;

namespace HomeEnergyUsageTracker.Api.Features.SavingsTips;

public class SavingsTipDto
{
    public Guid SavingsTipId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public static SavingsTipDto FromSavingsTip(SavingsTip savingsTip)
    {
        return new SavingsTipDto
        {
            SavingsTipId = savingsTip.SavingsTipId,
            Title = savingsTip.Title,
            Description = savingsTip.Description,
            CreatedAt = savingsTip.CreatedAt
        };
    }
}
