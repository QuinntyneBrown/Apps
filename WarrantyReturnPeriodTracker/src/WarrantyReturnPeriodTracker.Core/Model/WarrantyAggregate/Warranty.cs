// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents a warranty for a purchased product.
/// </summary>
public class Warranty
{
    /// <summary>
    /// Gets or sets the unique identifier for the warranty.
    /// </summary>
    public Guid WarrantyId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the purchase.
    /// </summary>
    public Guid PurchaseId { get; set; }

    /// <summary>
    /// Gets or sets the warranty type.
    /// </summary>
    public WarrantyType WarrantyType { get; set; }

    /// <summary>
    /// Gets or sets the warranty provider name.
    /// </summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the warranty start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the warranty end date.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the warranty duration in months.
    /// </summary>
    public int DurationMonths { get; set; }

    /// <summary>
    /// Gets or sets the warranty status.
    /// </summary>
    public WarrantyStatus Status { get; set; } = WarrantyStatus.Active;

    /// <summary>
    /// Gets or sets the warranty coverage details.
    /// </summary>
    public string? CoverageDetails { get; set; }

    /// <summary>
    /// Gets or sets the warranty terms and conditions.
    /// </summary>
    public string? Terms { get; set; }

    /// <summary>
    /// Gets or sets the warranty registration number or certificate number.
    /// </summary>
    public string? RegistrationNumber { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the warranty.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the date when a claim was filed.
    /// </summary>
    public DateTime? ClaimFiledDate { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the purchase.
    /// </summary>
    public Purchase? Purchase { get; set; }

    /// <summary>
    /// Checks if the warranty is currently active.
    /// </summary>
    /// <returns>True if the warranty is active and not expired; otherwise, false.</returns>
    public bool IsActive()
    {
        return Status == WarrantyStatus.Active && DateTime.UtcNow <= EndDate;
    }

    /// <summary>
    /// Checks if the warranty is expiring soon (within 30 days).
    /// </summary>
    /// <returns>True if warranty expires within 30 days; otherwise, false.</returns>
    public bool IsExpiringSoon()
    {
        if (!IsActive())
            return false;

        var daysRemaining = (EndDate - DateTime.UtcNow).Days;
        return daysRemaining > 0 && daysRemaining <= 30;
    }

    /// <summary>
    /// Gets the number of days remaining in the warranty.
    /// </summary>
    /// <returns>The number of days remaining, or 0 if expired.</returns>
    public int GetDaysRemaining()
    {
        var days = (EndDate - DateTime.UtcNow).Days;
        return Math.Max(0, days);
    }

    /// <summary>
    /// Files a warranty claim.
    /// </summary>
    public void FileClaim()
    {
        Status = WarrantyStatus.ClaimFiled;
        ClaimFiledDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the warranty claim as approved.
    /// </summary>
    public void ApproveClaim()
    {
        Status = WarrantyStatus.ClaimApproved;
    }

    /// <summary>
    /// Marks the warranty claim as rejected.
    /// </summary>
    /// <param name="reason">The reason for rejection.</param>
    public void RejectClaim(string reason)
    {
        Status = WarrantyStatus.ClaimRejected;
        Notes = string.IsNullOrEmpty(Notes)
            ? $"Claim rejected: {reason}"
            : $"{Notes}\n\nClaim rejected: {reason}";
    }

    /// <summary>
    /// Marks the warranty as expired.
    /// </summary>
    public void MarkAsExpired()
    {
        Status = WarrantyStatus.Expired;
    }

    /// <summary>
    /// Voids or cancels the warranty.
    /// </summary>
    /// <param name="reason">The reason for voiding.</param>
    public void VoidWarranty(string reason)
    {
        Status = WarrantyStatus.Voided;
        Notes = string.IsNullOrEmpty(Notes)
            ? $"Voided: {reason}"
            : $"{Notes}\n\nVoided: {reason}";
    }
}
