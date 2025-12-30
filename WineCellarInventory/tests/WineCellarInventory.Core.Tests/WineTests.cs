namespace WineCellarInventory.Core.Tests;

public class WineTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesWine()
    {
        // Arrange
        var wineId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Château Margaux";
        var wineType = WineType.Red;
        var region = Region.Bordeaux;
        var vintage = 2015;
        var producer = "Château Margaux";
        var purchasePrice = 450.00m;
        var bottleCount = 6;
        var storageLocation = "Cellar A, Rack 3";
        var notes = "Outstanding vintage, needs aging";

        // Act
        var wine = new Wine
        {
            WineId = wineId,
            UserId = userId,
            Name = name,
            WineType = wineType,
            Region = region,
            Vintage = vintage,
            Producer = producer,
            PurchasePrice = purchasePrice,
            BottleCount = bottleCount,
            StorageLocation = storageLocation,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(wine.WineId, Is.EqualTo(wineId));
            Assert.That(wine.UserId, Is.EqualTo(userId));
            Assert.That(wine.Name, Is.EqualTo(name));
            Assert.That(wine.WineType, Is.EqualTo(wineType));
            Assert.That(wine.Region, Is.EqualTo(region));
            Assert.That(wine.Vintage, Is.EqualTo(vintage));
            Assert.That(wine.Producer, Is.EqualTo(producer));
            Assert.That(wine.PurchasePrice, Is.EqualTo(purchasePrice));
            Assert.That(wine.BottleCount, Is.EqualTo(bottleCount));
            Assert.That(wine.StorageLocation, Is.EqualTo(storageLocation));
            Assert.That(wine.Notes, Is.EqualTo(notes));
            Assert.That(wine.TastingNotes, Is.Not.Null);
            Assert.That(wine.DrinkingWindows, Is.Not.Null);
        });
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var wine = new Wine();

        // Assert
        Assert.That(wine.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void BottleCount_DefaultValue_IsOne()
    {
        // Arrange & Act
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Wine"
        };

        // Assert
        Assert.That(wine.BottleCount, Is.EqualTo(1));
    }

    [Test]
    public void Vintage_NullValue_IsAllowed()
    {
        // Arrange & Act
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Non-Vintage Champagne",
            Vintage = null
        };

        // Assert
        Assert.That(wine.Vintage, Is.Null);
    }

    [Test]
    public void Producer_NullValue_IsAllowed()
    {
        // Arrange & Act
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "House Wine",
            Producer = null
        };

        // Assert
        Assert.That(wine.Producer, Is.Null);
    }

    [Test]
    public void PurchasePrice_NullValue_IsAllowed()
    {
        // Arrange & Act
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Gift Wine",
            PurchasePrice = null
        };

        // Assert
        Assert.That(wine.PurchasePrice, Is.Null);
    }

    [Test]
    public void StorageLocation_NullValue_IsAllowed()
    {
        // Arrange & Act
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Wine",
            StorageLocation = null
        };

        // Assert
        Assert.That(wine.StorageLocation, Is.Null);
    }

    [Test]
    public void Notes_NullValue_IsAllowed()
    {
        // Arrange & Act
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Wine",
            Notes = null
        };

        // Assert
        Assert.That(wine.Notes, Is.Null);
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSetToUtcNow()
    {
        // Arrange & Act
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Wine"
        };

        // Assert
        Assert.That(wine.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void WineType_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var red = new Wine { WineType = WineType.Red };
            Assert.That(red.WineType, Is.EqualTo(WineType.Red));

            var white = new Wine { WineType = WineType.White };
            Assert.That(white.WineType, Is.EqualTo(WineType.White));

            var rose = new Wine { WineType = WineType.Rose };
            Assert.That(rose.WineType, Is.EqualTo(WineType.Rose));

            var sparkling = new Wine { WineType = WineType.Sparkling };
            Assert.That(sparkling.WineType, Is.EqualTo(WineType.Sparkling));

            var dessert = new Wine { WineType = WineType.Dessert };
            Assert.That(dessert.WineType, Is.EqualTo(WineType.Dessert));

            var fortified = new Wine { WineType = WineType.Fortified };
            Assert.That(fortified.WineType, Is.EqualTo(WineType.Fortified));
        });
    }

    [Test]
    public void Region_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var bordeaux = new Wine { Region = Region.Bordeaux };
            Assert.That(bordeaux.Region, Is.EqualTo(Region.Bordeaux));

            var burgundy = new Wine { Region = Region.Burgundy };
            Assert.That(burgundy.Region, Is.EqualTo(Region.Burgundy));

            var champagne = new Wine { Region = Region.Champagne };
            Assert.That(champagne.Region, Is.EqualTo(Region.Champagne));

            var tuscany = new Wine { Region = Region.Tuscany };
            Assert.That(tuscany.Region, Is.EqualTo(Region.Tuscany));

            var napa = new Wine { Region = Region.Napa };
            Assert.That(napa.Region, Is.EqualTo(Region.Napa));

            var sonoma = new Wine { Region = Region.Sonoma };
            Assert.That(sonoma.Region, Is.EqualTo(Region.Sonoma));

            var rioja = new Wine { Region = Region.Rioja };
            Assert.That(rioja.Region, Is.EqualTo(Region.Rioja));

            var barolo = new Wine { Region = Region.Barolo };
            Assert.That(barolo.Region, Is.EqualTo(Region.Barolo));

            var rhoneValley = new Wine { Region = Region.RhoneValley };
            Assert.That(rhoneValley.Region, Is.EqualTo(Region.RhoneValley));

            var mosel = new Wine { Region = Region.Mosel };
            Assert.That(mosel.Region, Is.EqualTo(Region.Mosel));

            var other = new Wine { Region = Region.Other };
            Assert.That(other.Region, Is.EqualTo(Region.Other));
        });
    }

    [Test]
    public void TastingNotes_Collection_CanBePopulated()
    {
        // Arrange
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Wine"
        };

        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            WineId = wine.WineId,
            Rating = 90
        };

        // Act
        wine.TastingNotes.Add(tastingNote);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(wine.TastingNotes, Has.Count.EqualTo(1));
            Assert.That(wine.TastingNotes.First().Rating, Is.EqualTo(90));
        });
    }

    [Test]
    public void DrinkingWindows_Collection_CanBePopulated()
    {
        // Arrange
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Wine"
        };

        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            WineId = wine.WineId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(5)
        };

        // Act
        wine.DrinkingWindows.Add(drinkingWindow);

        // Assert
        Assert.That(wine.DrinkingWindows, Has.Count.EqualTo(1));
    }

    [Test]
    public void BottleCount_MultipleBottles_CanBeSet()
    {
        // Arrange & Act
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Case Wine",
            BottleCount = 12
        };

        // Assert
        Assert.That(wine.BottleCount, Is.EqualTo(12));
    }

    [Test]
    public void Vintage_OldVintage_CanBeSet()
    {
        // Arrange & Act
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Vintage Port",
            Vintage = 1963
        };

        // Assert
        Assert.That(wine.Vintage, Is.EqualTo(1963));
    }
}
