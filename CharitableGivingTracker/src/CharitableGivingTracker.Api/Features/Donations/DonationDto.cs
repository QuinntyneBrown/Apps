// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;

namespace CharitableGivingTracker.Api.Features.Donations;

public class DonationDto
{
    public Guid DonationId { get; set; }
    public Guid OrganizationId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DonationDate { get; set; }
    public DonationType DonationType { get; set; }
    public string? ReceiptNumber { get; set; }
    public bool IsTaxDeductible { get; set; }
    public string? Notes { get; set; }
    public string? OrganizationName { get; set; }
}
