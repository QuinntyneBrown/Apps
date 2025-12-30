// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.Mortgage;

/// <summary>
/// Data transfer object for Mortgage entity.
/// </summary>
public record MortgageDto
{
    public Guid MortgageId { get; set; }
    public string PropertyAddress { get; set; } = string.Empty;
    public string Lender { get; set; } = string.Empty;
    public decimal OriginalLoanAmount { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal InterestRate { get; set; }
    public int LoanTermYears { get; set; }
    public decimal MonthlyPayment { get; set; }
    public DateTime StartDate { get; set; }
    public MortgageType MortgageType { get; set; }
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Extension methods for mapping Mortgage to MortgageDto.
/// </summary>
public static class MortgageDtoExtensions
{
    public static MortgageDto ToDto(this Core.Mortgage mortgage)
    {
        return new MortgageDto
        {
            MortgageId = mortgage.MortgageId,
            PropertyAddress = mortgage.PropertyAddress,
            Lender = mortgage.Lender,
            OriginalLoanAmount = mortgage.OriginalLoanAmount,
            CurrentBalance = mortgage.CurrentBalance,
            InterestRate = mortgage.InterestRate,
            LoanTermYears = mortgage.LoanTermYears,
            MonthlyPayment = mortgage.MonthlyPayment,
            StartDate = mortgage.StartDate,
            MortgageType = mortgage.MortgageType,
            IsActive = mortgage.IsActive,
            Notes = mortgage.Notes
        };
    }
}
