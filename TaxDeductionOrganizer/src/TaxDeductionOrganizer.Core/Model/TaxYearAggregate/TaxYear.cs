// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaxDeductionOrganizer.Core;

public class TaxYear
{
    public Guid TaxYearId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public int Year { get; set; }
    public bool IsFiled { get; set; }
    public DateTime? FilingDate { get; set; }
    public decimal TotalDeductions { get; set; }
    public string? Notes { get; set; }
    public List<Deduction> Deductions { get; set; } = new List<Deduction>();
    
    public void CalculateTotalDeductions()
    {
        TotalDeductions = Deductions.Sum(d => d.Amount);
    }
    
    public void MarkAsFiled()
    {
        IsFiled = true;
        FilingDate = DateTime.UtcNow;
    }
}
