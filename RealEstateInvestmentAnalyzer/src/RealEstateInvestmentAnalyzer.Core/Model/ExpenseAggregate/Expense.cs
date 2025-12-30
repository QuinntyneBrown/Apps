// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core;

public class Expense
{
    public Guid ExpenseId { get; set; }
    public Guid PropertyId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool IsRecurring { get; set; }
    public string? Notes { get; set; }
    public Property? Property { get; set; }
}
