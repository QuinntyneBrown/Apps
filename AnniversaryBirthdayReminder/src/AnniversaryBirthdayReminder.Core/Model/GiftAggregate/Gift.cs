// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Represents a gift idea or purchased gift for an important date.
/// </summary>
public class Gift
{
    /// <summary>
    /// Gets or sets the unique identifier for the gift.
    /// </summary>
    public Guid GiftId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the important date.
    /// </summary>
    public Guid ImportantDateId { get; set; }

    /// <summary>
    /// Gets or sets the gift description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated price of the gift.
    /// </summary>
    public decimal EstimatedPrice { get; set; }

    /// <summary>
    /// Gets or sets the actual price of the gift (after purchase).
    /// </summary>
    public decimal? ActualPrice { get; set; }

    /// <summary>
    /// Gets or sets the URL where the gift can be purchased.
    /// </summary>
    public string? PurchaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the status of the gift.
    /// </summary>
    public GiftStatus Status { get; set; } = GiftStatus.Idea;

    /// <summary>
    /// Gets or sets the timestamp when the gift was purchased.
    /// </summary>
    public DateTime? PurchasedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the gift was delivered.
    /// </summary>
    public DateTime? DeliveredAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the important date.
    /// </summary>
    public ImportantDate? ImportantDate { get; set; }

    /// <summary>
    /// Marks the gift as purchased.
    /// </summary>
    /// <param name="actualPrice">The actual price paid.</param>
    public void MarkAsPurchased(decimal actualPrice)
    {
        Status = GiftStatus.Purchased;
        ActualPrice = actualPrice;
        PurchasedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the gift as delivered.
    /// </summary>
    public void MarkAsDelivered()
    {
        Status = GiftStatus.Delivered;
        DeliveredAt = DateTime.UtcNow;
    }
}
