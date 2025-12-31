// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLoanComparisonTool.Core;

public class PaymentSchedule
{
    public Guid PaymentScheduleId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid OfferId { get; set; }
    public int PaymentNumber { get; set; }
    public DateTime DueDate { get; set; }
    public decimal PaymentAmount { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal RemainingBalance { get; set; }
    public Offer? Offer { get; set; }
}
