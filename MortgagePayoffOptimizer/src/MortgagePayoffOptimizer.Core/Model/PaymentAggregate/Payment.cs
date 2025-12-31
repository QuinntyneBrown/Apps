// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Core;

/// <summary>
/// Represents a mortgage payment.
/// </summary>
public class Payment
{
    /// <summary>
    /// Gets or sets the unique identifier for the payment.
    /// </summary>
    public Guid PaymentId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the mortgage.
    /// </summary>
    public Guid MortgageId { get; set; }

    /// <summary>
    /// Gets or sets the payment date.
    /// </summary>
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// Gets or sets the payment amount.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the principal portion.
    /// </summary>
    public decimal PrincipalAmount { get; set; }

    /// <summary>
    /// Gets or sets the interest portion.
    /// </summary>
    public decimal InterestAmount { get; set; }

    /// <summary>
    /// Gets or sets the extra principal payment.
    /// </summary>
    public decimal? ExtraPrincipal { get; set; }

    /// <summary>
    /// Gets or sets notes about the payment.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the mortgage.
    /// </summary>
    public Mortgage? Mortgage { get; set; }

    /// <summary>
    /// Calculates the total payment including extra principal.
    /// </summary>
    public decimal CalculateTotalPayment()
    {
        return Amount + (ExtraPrincipal ?? 0);
    }
}
