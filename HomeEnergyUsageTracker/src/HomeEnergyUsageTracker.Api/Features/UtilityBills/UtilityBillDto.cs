// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;

namespace HomeEnergyUsageTracker.Api.Features.UtilityBills;

public class UtilityBillDto
{
    public Guid UtilityBillId { get; set; }
    public Guid UserId { get; set; }
    public UtilityType UtilityType { get; set; }
    public DateTime BillingDate { get; set; }
    public decimal Amount { get; set; }
    public decimal? UsageAmount { get; set; }
    public string? Unit { get; set; }
    public DateTime CreatedAt { get; set; }

    public static UtilityBillDto FromUtilityBill(UtilityBill utilityBill)
    {
        return new UtilityBillDto
        {
            UtilityBillId = utilityBill.UtilityBillId,
            UserId = utilityBill.UserId,
            UtilityType = utilityBill.UtilityType,
            BillingDate = utilityBill.BillingDate,
            Amount = utilityBill.Amount,
            UsageAmount = utilityBill.UsageAmount,
            Unit = utilityBill.Unit,
            CreatedAt = utilityBill.CreatedAt
        };
    }
}
