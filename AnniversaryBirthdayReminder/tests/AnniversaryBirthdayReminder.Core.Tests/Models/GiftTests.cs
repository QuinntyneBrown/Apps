// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core.Tests;

/// <summary>
/// Unit tests for the Gift entity.
/// </summary>
[TestFixture]
public class GiftTests
{
    /// <summary>
    /// Tests that MarkAsPurchased sets status, price, and timestamp correctly.
    /// </summary>
    [Test]
    public void MarkAsPurchased_SetsStatusPriceAndTimestamp()
    {
        // Arrange
        var gift = new Gift
        {
            GiftId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            Description = "Test Gift",
            EstimatedPrice = 50m,
            Status = GiftStatus.Idea,
        };
        var actualPrice = 45.99m;

        // Act
        gift.MarkAsPurchased(actualPrice);

        // Assert
        Assert.That(gift.Status, Is.EqualTo(GiftStatus.Purchased));
        Assert.That(gift.ActualPrice, Is.EqualTo(actualPrice));
        Assert.That(gift.PurchasedAt, Is.Not.Null);
        Assert.That(gift.PurchasedAt!.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    /// <summary>
    /// Tests that MarkAsDelivered sets status and timestamp correctly.
    /// </summary>
    [Test]
    public void MarkAsDelivered_SetsStatusAndTimestamp()
    {
        // Arrange
        var gift = new Gift
        {
            GiftId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            Description = "Test Gift",
            EstimatedPrice = 50m,
            ActualPrice = 45.99m,
            Status = GiftStatus.Purchased,
            PurchasedAt = DateTime.UtcNow.AddDays(-1),
        };

        // Act
        gift.MarkAsDelivered();

        // Assert
        Assert.That(gift.Status, Is.EqualTo(GiftStatus.Delivered));
        Assert.That(gift.DeliveredAt, Is.Not.Null);
        Assert.That(gift.DeliveredAt!.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    /// <summary>
    /// Tests that gift has default status of Idea.
    /// </summary>
    [Test]
    public void NewGift_HasDefaultStatusOfIdea()
    {
        // Arrange & Act
        var gift = new Gift();

        // Assert
        Assert.That(gift.Status, Is.EqualTo(GiftStatus.Idea));
    }
}
