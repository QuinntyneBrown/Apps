// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;

namespace HomeEnergyUsageTracker.Api.Features.Usages;

public class UsageDto
{
    public Guid UsageId { get; set; }
    public Guid UtilityBillId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }

    public static UsageDto FromUsage(Usage usage)
    {
        return new UsageDto
        {
            UsageId = usage.UsageId,
            UtilityBillId = usage.UtilityBillId,
            Date = usage.Date,
            Amount = usage.Amount,
            CreatedAt = usage.CreatedAt
        };
    }
}
