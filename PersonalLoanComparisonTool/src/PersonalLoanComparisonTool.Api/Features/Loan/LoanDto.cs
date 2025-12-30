// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.Loan;

public record LoanDto(
    Guid LoanId,
    string Name,
    LoanType LoanType,
    decimal RequestedAmount,
    string Purpose,
    int CreditScore,
    string? Notes
);

public static class LoanExtensions
{
    public static LoanDto ToDto(this Core.Loan loan)
    {
        return new LoanDto(
            loan.LoanId,
            loan.Name,
            loan.LoanType,
            loan.RequestedAmount,
            loan.Purpose,
            loan.CreditScore,
            loan.Notes
        );
    }
}
