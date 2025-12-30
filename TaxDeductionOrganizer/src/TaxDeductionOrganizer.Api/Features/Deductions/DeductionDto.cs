// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.Deductions;

public class DeductionDto
{
    public Guid DeductionId { get; set; }
    public Guid TaxYearId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public DeductionCategory Category { get; set; }
    public string? Notes { get; set; }
    public bool HasReceipt { get; set; }

    public static DeductionDto ToDto(Deduction deduction)
    {
        return new DeductionDto
        {
            DeductionId = deduction.DeductionId,
            TaxYearId = deduction.TaxYearId,
            Description = deduction.Description,
            Amount = deduction.Amount,
            Date = deduction.Date,
            Category = deduction.Category,
            Notes = deduction.Notes,
            HasReceipt = deduction.HasReceipt
        };
    }
}

public static class DeductionExtensions
{
    public static DeductionDto ToDto(this Deduction deduction) => DeductionDto.ToDto(deduction);
}
