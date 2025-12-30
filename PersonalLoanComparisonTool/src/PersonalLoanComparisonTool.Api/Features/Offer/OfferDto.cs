// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.Offer;

public record OfferDto(
    Guid OfferId,
    Guid LoanId,
    string LenderName,
    decimal LoanAmount,
    decimal InterestRate,
    int TermMonths,
    decimal MonthlyPayment,
    decimal TotalCost,
    decimal Fees,
    string? Notes
);

public static class OfferExtensions
{
    public static OfferDto ToDto(this Core.Offer offer)
    {
        return new OfferDto(
            offer.OfferId,
            offer.LoanId,
            offer.LenderName,
            offer.LoanAmount,
            offer.InterestRate,
            offer.TermMonths,
            offer.MonthlyPayment,
            offer.TotalCost,
            offer.Fees,
            offer.Notes
        );
    }
}
