using WineCellarInventory.Api.Features.Wines;
using WineCellarInventory.Api.Features.TastingNotes;
using WineCellarInventory.Api.Features.DrinkingWindows;

namespace WineCellarInventory.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void WineDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var wine = new Core.Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Chateau Margaux 2015",
            WineType = Core.WineType.Red,
            Region = Core.Region.Bordeaux,
            Vintage = 2015,
            Producer = "Chateau Margaux",
            PurchasePrice = 450.00m,
            BottleCount = 6,
            StorageLocation = "Cellar - Rack A1",
            Notes = "Full-bodied with notes of blackcurrant and oak",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = wine.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.WineId, Is.EqualTo(wine.WineId));
            Assert.That(dto.UserId, Is.EqualTo(wine.UserId));
            Assert.That(dto.Name, Is.EqualTo(wine.Name));
            Assert.That(dto.WineType, Is.EqualTo(wine.WineType));
            Assert.That(dto.Region, Is.EqualTo(wine.Region));
            Assert.That(dto.Vintage, Is.EqualTo(wine.Vintage));
            Assert.That(dto.Producer, Is.EqualTo(wine.Producer));
            Assert.That(dto.PurchasePrice, Is.EqualTo(wine.PurchasePrice));
            Assert.That(dto.BottleCount, Is.EqualTo(wine.BottleCount));
            Assert.That(dto.StorageLocation, Is.EqualTo(wine.StorageLocation));
            Assert.That(dto.Notes, Is.EqualTo(wine.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(wine.CreatedAt));
        });
    }

    [Test]
    public void TastingNoteDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var tastingNote = new Core.TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            TastingDate = DateTime.UtcNow,
            Rating = 95,
            Appearance = "Deep ruby color",
            Aroma = "Blackcurrant, tobacco, and vanilla",
            Taste = "Rich and full-bodied with integrated tannins",
            Finish = "Long and persistent",
            OverallImpression = "Excellent wine with great aging potential",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = tastingNote.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.TastingNoteId, Is.EqualTo(tastingNote.TastingNoteId));
            Assert.That(dto.UserId, Is.EqualTo(tastingNote.UserId));
            Assert.That(dto.WineId, Is.EqualTo(tastingNote.WineId));
            Assert.That(dto.TastingDate, Is.EqualTo(tastingNote.TastingDate));
            Assert.That(dto.Rating, Is.EqualTo(tastingNote.Rating));
            Assert.That(dto.Appearance, Is.EqualTo(tastingNote.Appearance));
            Assert.That(dto.Aroma, Is.EqualTo(tastingNote.Aroma));
            Assert.That(dto.Taste, Is.EqualTo(tastingNote.Taste));
            Assert.That(dto.Finish, Is.EqualTo(tastingNote.Finish));
            Assert.That(dto.OverallImpression, Is.EqualTo(tastingNote.OverallImpression));
            Assert.That(dto.CreatedAt, Is.EqualTo(tastingNote.CreatedAt));
        });
    }

    [Test]
    public void DrinkingWindowDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var drinkingWindow = new Core.DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow.AddYears(5),
            EndDate = DateTime.UtcNow.AddYears(15),
            Notes = "Peak drinking window for this vintage",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = drinkingWindow.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.DrinkingWindowId, Is.EqualTo(drinkingWindow.DrinkingWindowId));
            Assert.That(dto.UserId, Is.EqualTo(drinkingWindow.UserId));
            Assert.That(dto.WineId, Is.EqualTo(drinkingWindow.WineId));
            Assert.That(dto.StartDate, Is.EqualTo(drinkingWindow.StartDate));
            Assert.That(dto.EndDate, Is.EqualTo(drinkingWindow.EndDate));
            Assert.That(dto.Notes, Is.EqualTo(drinkingWindow.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(drinkingWindow.CreatedAt));
        });
    }

    [Test]
    public void WineDto_WithNullableFields_MapsCorrectly()
    {
        // Arrange
        var wine = new Core.Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "House Red",
            WineType = Core.WineType.Red,
            Region = Core.Region.Other,
            Vintage = null,
            Producer = null,
            PurchasePrice = null,
            BottleCount = 1,
            StorageLocation = null,
            Notes = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = wine.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.Vintage, Is.Null);
            Assert.That(dto.Producer, Is.Null);
            Assert.That(dto.PurchasePrice, Is.Null);
            Assert.That(dto.StorageLocation, Is.Null);
            Assert.That(dto.Notes, Is.Null);
        });
    }

    [Test]
    public void TastingNoteDto_WithNullableFields_MapsCorrectly()
    {
        // Arrange
        var tastingNote = new Core.TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            TastingDate = DateTime.UtcNow,
            Rating = 80,
            Appearance = null,
            Aroma = null,
            Taste = null,
            Finish = null,
            OverallImpression = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = tastingNote.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.Appearance, Is.Null);
            Assert.That(dto.Aroma, Is.Null);
            Assert.That(dto.Taste, Is.Null);
            Assert.That(dto.Finish, Is.Null);
            Assert.That(dto.OverallImpression, Is.Null);
        });
    }

    [Test]
    public void DrinkingWindowDto_WithNullNotes_MapsCorrectly()
    {
        // Arrange
        var drinkingWindow = new Core.DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(10),
            Notes = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = drinkingWindow.ToDto();

        // Assert
        Assert.That(dto.Notes, Is.Null);
    }
}
