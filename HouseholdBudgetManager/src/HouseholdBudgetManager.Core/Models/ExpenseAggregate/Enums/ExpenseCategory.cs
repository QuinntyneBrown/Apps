// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core;

/// <summary>
/// Represents the category of an expense.
/// </summary>
public enum ExpenseCategory
{
    /// <summary>
    /// Housing expenses (rent, mortgage, utilities).
    /// </summary>
    Housing = 0,

    /// <summary>
    /// Transportation expenses.
    /// </summary>
    Transportation = 1,

    /// <summary>
    /// Food and groceries.
    /// </summary>
    Food = 2,

    /// <summary>
    /// Healthcare expenses.
    /// </summary>
    Healthcare = 3,

    /// <summary>
    /// Entertainment and recreation.
    /// </summary>
    Entertainment = 4,

    /// <summary>
    /// Personal care.
    /// </summary>
    PersonalCare = 5,

    /// <summary>
    /// Education expenses.
    /// </summary>
    Education = 6,

    /// <summary>
    /// Debt payments.
    /// </summary>
    DebtPayment = 7,

    /// <summary>
    /// Savings and investments.
    /// </summary>
    Savings = 8,

    /// <summary>
    /// Other expenses.
    /// </summary>
    Other = 9,
}
