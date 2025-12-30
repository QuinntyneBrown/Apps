// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Core;

/// <summary>
/// Event raised when a mortgage is created.
/// </summary>
public record MortgageCreatedEvent
{
    /// <summary>
    /// Gets the mortgage ID.
    /// </summary>
    public Guid MortgageId { get; init; }

    /// <summary>
    /// Gets the property address.
    /// </summary>
    public string PropertyAddress { get; init; } = string.Empty;

    /// <summary>
    /// Gets the original loan amount.
    /// </summary>
    public decimal OriginalLoanAmount { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
