// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaxDeductionOrganizer.Core;

/// <summary>
/// Represents a tax deduction.
/// </summary>
public class Deduction
{
    public Guid DeductionId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid TaxYearId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public DeductionCategory Category { get; set; }
    public string? Notes { get; set; }
    public bool HasReceipt { get; set; }
    public TaxYear? TaxYear { get; set; }
    public void AttachReceipt() => HasReceipt = true;
}
