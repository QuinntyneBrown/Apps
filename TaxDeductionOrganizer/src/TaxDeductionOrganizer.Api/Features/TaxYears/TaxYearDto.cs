// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.TaxYears;

public class TaxYearDto
{
    public Guid TaxYearId { get; set; }
    public int Year { get; set; }
    public bool IsFiled { get; set; }
    public DateTime? FilingDate { get; set; }
    public decimal TotalDeductions { get; set; }
    public string? Notes { get; set; }

    public static TaxYearDto ToDto(TaxYear taxYear)
    {
        return new TaxYearDto
        {
            TaxYearId = taxYear.TaxYearId,
            Year = taxYear.Year,
            IsFiled = taxYear.IsFiled,
            FilingDate = taxYear.FilingDate,
            TotalDeductions = taxYear.TotalDeductions,
            Notes = taxYear.Notes
        };
    }
}

public static class TaxYearExtensions
{
    public static TaxYearDto ToDto(this TaxYear taxYear) => TaxYearDto.ToDto(taxYear);
}
