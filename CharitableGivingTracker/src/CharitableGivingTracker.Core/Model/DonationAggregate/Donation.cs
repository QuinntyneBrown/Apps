// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CharitableGivingTracker.Core;

public class Donation
{
    public Guid DonationId { get; set; }
    public Guid OrganizationId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DonationDate { get; set; }
    public DonationType DonationType { get; set; }
    public string? ReceiptNumber { get; set; }
    public bool IsTaxDeductible { get; set; } = true;
    public string? Notes { get; set; }
    public Organization? Organization { get; set; }
}
