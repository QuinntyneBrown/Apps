// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalNetWorthDashboard.Core;

/// <summary>
/// Represents the type of a liability.
/// </summary>
public enum LiabilityType
{
    /// <summary>
    /// Mortgage loan.
    /// </summary>
    Mortgage = 0,

    /// <summary>
    /// Auto loan.
    /// </summary>
    AutoLoan = 1,

    /// <summary>
    /// Student loan.
    /// </summary>
    StudentLoan = 2,

    /// <summary>
    /// Credit card debt.
    /// </summary>
    CreditCard = 3,

    /// <summary>
    /// Personal loan.
    /// </summary>
    PersonalLoan = 4,

    /// <summary>
    /// Medical debt.
    /// </summary>
    MedicalDebt = 5,

    /// <summary>
    /// Business loan.
    /// </summary>
    BusinessLoan = 6,

    /// <summary>
    /// Other type of liability.
    /// </summary>
    Other = 7,
}
