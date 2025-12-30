using HomeInventoryManager.Api.Features.Items;
using HomeInventoryManager.Api.Features.ValueEstimates;

namespace HomeInventoryManager.Api.Tests;

[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void ItemDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var item = new Core.Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Item",
            Description = "Test Description",
            Category = Core.Category.Electronics,
            Room = Core.Room.LivingRoom,
            Brand = "Test Brand",
            ModelNumber = "MODEL-123",
            SerialNumber = "SERIAL-456",
            PurchaseDate = DateTime.UtcNow.AddMonths(-6),
            PurchasePrice = 500.00m,
            CurrentValue = 400.00m,
            Quantity = 2,
            PhotoUrl = "https://example.com/photo.jpg",
            ReceiptUrl = "https://example.com/receipt.pdf",
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = item.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ItemId, Is.EqualTo(item.ItemId));
            Assert.That(dto.UserId, Is.EqualTo(item.UserId));
            Assert.That(dto.Name, Is.EqualTo(item.Name));
            Assert.That(dto.Description, Is.EqualTo(item.Description));
            Assert.That(dto.Category, Is.EqualTo(item.Category));
            Assert.That(dto.Room, Is.EqualTo(item.Room));
            Assert.That(dto.Brand, Is.EqualTo(item.Brand));
            Assert.That(dto.ModelNumber, Is.EqualTo(item.ModelNumber));
            Assert.That(dto.SerialNumber, Is.EqualTo(item.SerialNumber));
            Assert.That(dto.PurchaseDate, Is.EqualTo(item.PurchaseDate));
            Assert.That(dto.PurchasePrice, Is.EqualTo(item.PurchasePrice));
            Assert.That(dto.CurrentValue, Is.EqualTo(item.CurrentValue));
            Assert.That(dto.Quantity, Is.EqualTo(item.Quantity));
            Assert.That(dto.PhotoUrl, Is.EqualTo(item.PhotoUrl));
            Assert.That(dto.ReceiptUrl, Is.EqualTo(item.ReceiptUrl));
            Assert.That(dto.Notes, Is.EqualTo(item.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(item.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(item.UpdatedAt));
        });
    }

    [Test]
    public void ValueEstimateDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var valueEstimate = new Core.ValueEstimate
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = 450.00m,
            EstimationDate = DateTime.UtcNow,
            Source = "Professional Appraisal",
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = valueEstimate.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ValueEstimateId, Is.EqualTo(valueEstimate.ValueEstimateId));
            Assert.That(dto.ItemId, Is.EqualTo(valueEstimate.ItemId));
            Assert.That(dto.EstimatedValue, Is.EqualTo(valueEstimate.EstimatedValue));
            Assert.That(dto.EstimationDate, Is.EqualTo(valueEstimate.EstimationDate));
            Assert.That(dto.Source, Is.EqualTo(valueEstimate.Source));
            Assert.That(dto.Notes, Is.EqualTo(valueEstimate.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(valueEstimate.CreatedAt));
        });
    }
}
