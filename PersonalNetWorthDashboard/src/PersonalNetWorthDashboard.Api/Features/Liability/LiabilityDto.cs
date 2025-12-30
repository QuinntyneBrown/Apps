// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;

namespace PersonalNetWorthDashboard.Api.Features.Liability;

public record LiabilityDto(
    Guid LiabilityId,
    string Name,
    LiabilityType LiabilityType,
    decimal CurrentBalance,
    decimal? OriginalAmount,
    decimal? InterestRate,
    decimal? MonthlyPayment,
    string? Creditor,
    string? AccountNumber,
    DateTime? DueDate,
    string? Notes,
    DateTime LastUpdated,
    bool IsActive);

public static class LiabilityExtensions
{
    public static LiabilityDto ToDto(this Core.Liability liability)
    {
        return new LiabilityDto(
            liability.LiabilityId,
            liability.Name,
            liability.LiabilityType,
            liability.CurrentBalance,
            liability.OriginalAmount,
            liability.InterestRate,
            liability.MonthlyPayment,
            liability.Creditor,
            liability.AccountNumber,
            liability.DueDate,
            liability.Notes,
            liability.LastUpdated,
            liability.IsActive);
    }
}
