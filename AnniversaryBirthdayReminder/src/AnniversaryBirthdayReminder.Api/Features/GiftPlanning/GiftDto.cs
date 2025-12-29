// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Data transfer object for Gift.
/// </summary>
public record GiftDto
{
    /// <summary>
    /// Gets or sets the gift ID.
    /// </summary>
    public Guid GiftId { get; init; }

    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated price.
    /// </summary>
    public decimal EstimatedPrice { get; init; }

    /// <summary>
    /// Gets or sets the actual price.
    /// </summary>
    public decimal? ActualPrice { get; init; }

    /// <summary>
    /// Gets or sets the purchase URL.
    /// </summary>
    public string? PurchaseUrl { get; init; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public GiftStatus Status { get; init; }

    /// <summary>
    /// Gets or sets the purchased timestamp.
    /// </summary>
    public DateTime? PurchasedAt { get; init; }

    /// <summary>
    /// Gets or sets the delivered timestamp.
    /// </summary>
    public DateTime? DeliveredAt { get; init; }
}

/// <summary>
/// Extension methods for Gift.
/// </summary>
public static class GiftExtensions
{
    /// <summary>
    /// Converts a Gift to a DTO.
    /// </summary>
    /// <param name="gift">The gift.</param>
    /// <returns>The DTO.</returns>
    public static GiftDto ToDto(this Gift gift)
    {
        return new GiftDto
        {
            GiftId = gift.GiftId,
            ImportantDateId = gift.ImportantDateId,
            Description = gift.Description,
            EstimatedPrice = gift.EstimatedPrice,
            ActualPrice = gift.ActualPrice,
            PurchaseUrl = gift.PurchaseUrl,
            Status = gift.Status,
            PurchasedAt = gift.PurchasedAt,
            DeliveredAt = gift.DeliveredAt,
        };
    }
}
