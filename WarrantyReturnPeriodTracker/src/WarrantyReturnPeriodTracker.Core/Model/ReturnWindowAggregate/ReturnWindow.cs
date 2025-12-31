// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents the return window period for a purchase.
/// </summary>
public class ReturnWindow
{
    /// <summary>
    /// Gets or sets the unique identifier for the return window.
    /// </summary>
    public Guid ReturnWindowId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the purchase.
    /// </summary>
    public Guid PurchaseId { get; set; }

    /// <summary>
    /// Gets or sets the start date of the return window.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the return window.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the duration of the return window in days.
    /// </summary>
    public int DurationDays { get; set; }

    /// <summary>
    /// Gets or sets the status of the return window.
    /// </summary>
    public ReturnWindowStatus Status { get; set; } = ReturnWindowStatus.Open;

    /// <summary>
    /// Gets or sets the return policy details.
    /// </summary>
    public string? PolicyDetails { get; set; }

    /// <summary>
    /// Gets or sets any conditions or requirements for returns.
    /// </summary>
    public string? Conditions { get; set; }

    /// <summary>
    /// Gets or sets the restocking fee percentage, if applicable.
    /// </summary>
    public decimal? RestockingFeePercent { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the return window.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the purchase.
    /// </summary>
    public Purchase? Purchase { get; set; }

    /// <summary>
    /// Checks if the return window is currently open.
    /// </summary>
    /// <returns>True if the window is open and current date is within the period; otherwise, false.</returns>
    public bool IsOpen()
    {
        var now = DateTime.UtcNow;
        return Status == ReturnWindowStatus.Open && now >= StartDate && now <= EndDate;
    }

    /// <summary>
    /// Checks if the return window is closing soon (within 7 days).
    /// </summary>
    /// <returns>True if window closes within 7 days; otherwise, false.</returns>
    public bool IsClosingSoon()
    {
        if (!IsOpen())
            return false;

        var daysRemaining = (EndDate - DateTime.UtcNow).Days;
        return daysRemaining > 0 && daysRemaining <= 7;
    }

    /// <summary>
    /// Gets the number of days remaining in the return window.
    /// </summary>
    /// <returns>The number of days remaining, or 0 if closed.</returns>
    public int GetDaysRemaining()
    {
        if (!IsOpen())
            return 0;

        var days = (EndDate - DateTime.UtcNow).Days;
        return Math.Max(0, days);
    }

    /// <summary>
    /// Marks the return window as used (product was returned).
    /// </summary>
    public void MarkAsUsed()
    {
        Status = ReturnWindowStatus.Used;
    }

    /// <summary>
    /// Marks the return window as expired.
    /// </summary>
    public void MarkAsExpired()
    {
        Status = ReturnWindowStatus.Expired;
    }

    /// <summary>
    /// Marks the return window as voided.
    /// </summary>
    /// <param name="reason">The reason for voiding.</param>
    public void VoidWindow(string reason)
    {
        Status = ReturnWindowStatus.Voided;
        Notes = string.IsNullOrEmpty(Notes)
            ? $"Voided: {reason}"
            : $"{Notes}\n\nVoided: {reason}";
    }

    /// <summary>
    /// Calculates the restocking fee for a return.
    /// </summary>
    /// <param name="purchasePrice">The original purchase price.</param>
    /// <returns>The restocking fee amount.</returns>
    public decimal CalculateRestockingFee(decimal purchasePrice)
    {
        if (!RestockingFeePercent.HasValue)
            return 0;

        return purchasePrice * (RestockingFeePercent.Value / 100);
    }
}
