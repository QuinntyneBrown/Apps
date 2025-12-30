// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core;

/// <summary>
/// Represents the type of investment account.
/// </summary>
public enum AccountType
{
    /// <summary>
    /// Standard taxable brokerage account.
    /// </summary>
    Taxable = 0,

    /// <summary>
    /// Traditional IRA.
    /// </summary>
    TraditionalIRA = 1,

    /// <summary>
    /// Roth IRA.
    /// </summary>
    RothIRA = 2,

    /// <summary>
    /// 401(k) retirement account.
    /// </summary>
    FourZeroOneK = 3,

    /// <summary>
    /// 403(b) retirement account.
    /// </summary>
    FourZeroThreeB = 4,

    /// <summary>
    /// Health Savings Account (HSA).
    /// </summary>
    HSA = 5,

    /// <summary>
    /// 529 college savings plan.
    /// </summary>
    FiveTwoNine = 6,
}
