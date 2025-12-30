// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents a receipt or proof of purchase for a product.
/// </summary>
public class Receipt
{
    /// <summary>
    /// Gets or sets the unique identifier for the receipt.
    /// </summary>
    public Guid ReceiptId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the purchase.
    /// </summary>
    public Guid PurchaseId { get; set; }

    /// <summary>
    /// Gets or sets the receipt number or order number.
    /// </summary>
    public string ReceiptNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the receipt type.
    /// </summary>
    public ReceiptType ReceiptType { get; set; }

    /// <summary>
    /// Gets or sets the receipt format.
    /// </summary>
    public ReceiptFormat Format { get; set; }

    /// <summary>
    /// Gets or sets the storage location (file path, cloud URL, etc.).
    /// </summary>
    public string? StorageLocation { get; set; }

    /// <summary>
    /// Gets or sets the receipt date.
    /// </summary>
    public DateTime ReceiptDate { get; set; }

    /// <summary>
    /// Gets or sets the store or merchant name.
    /// </summary>
    public string StoreName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total amount on the receipt.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the payment method used.
    /// </summary>
    public PaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Gets or sets the status of the receipt.
    /// </summary>
    public ReceiptStatus Status { get; set; } = ReceiptStatus.Active;

    /// <summary>
    /// Gets or sets a value indicating whether the receipt is verified.
    /// </summary>
    public bool IsVerified { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the receipt.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the receipt was uploaded.
    /// </summary>
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the purchase.
    /// </summary>
    public Purchase? Purchase { get; set; }

    /// <summary>
    /// Marks the receipt as verified.
    /// </summary>
    public void Verify()
    {
        IsVerified = true;
    }

    /// <summary>
    /// Marks the receipt as archived.
    /// </summary>
    public void Archive()
    {
        Status = ReceiptStatus.Archived;
    }

    /// <summary>
    /// Marks the receipt as lost or missing.
    /// </summary>
    public void MarkAsLost()
    {
        Status = ReceiptStatus.Lost;
    }

    /// <summary>
    /// Marks the receipt as invalid or rejected.
    /// </summary>
    /// <param name="reason">The reason for marking as invalid.</param>
    public void MarkAsInvalid(string reason)
    {
        Status = ReceiptStatus.Invalid;
        Notes = string.IsNullOrEmpty(Notes)
            ? $"Invalid: {reason}"
            : $"{Notes}\n\nInvalid: {reason}";
    }

    /// <summary>
    /// Updates the storage location of the receipt.
    /// </summary>
    /// <param name="newLocation">The new storage location.</param>
    public void UpdateStorageLocation(string newLocation)
    {
        StorageLocation = newLocation;
    }

    /// <summary>
    /// Checks if the receipt is available and accessible.
    /// </summary>
    /// <returns>True if receipt is active and has a storage location; otherwise, false.</returns>
    public bool IsAccessible()
    {
        return Status == ReceiptStatus.Active && !string.IsNullOrEmpty(StorageLocation);
    }
}
