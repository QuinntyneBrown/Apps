using GiftIdeaTracker.Api.Features.GiftIdeas;
using GiftIdeaTracker.Api.Features.Recipients;
using GiftIdeaTracker.Api.Features.Purchases;

namespace GiftIdeaTracker.Api.Tests;

[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void GiftIdeaDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var giftIdea = new Core.GiftIdea
        {
            GiftIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            RecipientId = Guid.NewGuid(),
            Name = "Test Gift Idea",
            Occasion = Core.Occasion.Birthday,
            EstimatedPrice = 99.99m,
            IsPurchased = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = giftIdea.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.GiftIdeaId, Is.EqualTo(giftIdea.GiftIdeaId));
            Assert.That(dto.UserId, Is.EqualTo(giftIdea.UserId));
            Assert.That(dto.RecipientId, Is.EqualTo(giftIdea.RecipientId));
            Assert.That(dto.Name, Is.EqualTo(giftIdea.Name));
            Assert.That(dto.Occasion, Is.EqualTo(giftIdea.Occasion));
            Assert.That(dto.EstimatedPrice, Is.EqualTo(giftIdea.EstimatedPrice));
            Assert.That(dto.IsPurchased, Is.EqualTo(giftIdea.IsPurchased));
            Assert.That(dto.CreatedAt, Is.EqualTo(giftIdea.CreatedAt));
        });
    }

    [Test]
    public void RecipientDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var recipient = new Core.Recipient
        {
            RecipientId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "John Doe",
            Relationship = "Friend",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = recipient.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.RecipientId, Is.EqualTo(recipient.RecipientId));
            Assert.That(dto.UserId, Is.EqualTo(recipient.UserId));
            Assert.That(dto.Name, Is.EqualTo(recipient.Name));
            Assert.That(dto.Relationship, Is.EqualTo(recipient.Relationship));
            Assert.That(dto.CreatedAt, Is.EqualTo(recipient.CreatedAt));
        });
    }

    [Test]
    public void PurchaseDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var purchase = new Core.Purchase
        {
            PurchaseId = Guid.NewGuid(),
            GiftIdeaId = Guid.NewGuid(),
            PurchaseDate = DateTime.UtcNow,
            ActualPrice = 89.99m,
            Store = "Amazon",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = purchase.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.PurchaseId, Is.EqualTo(purchase.PurchaseId));
            Assert.That(dto.GiftIdeaId, Is.EqualTo(purchase.GiftIdeaId));
            Assert.That(dto.PurchaseDate, Is.EqualTo(purchase.PurchaseDate));
            Assert.That(dto.ActualPrice, Is.EqualTo(purchase.ActualPrice));
            Assert.That(dto.Store, Is.EqualTo(purchase.Store));
            Assert.That(dto.CreatedAt, Is.EqualTo(purchase.CreatedAt));
        });
    }
}
