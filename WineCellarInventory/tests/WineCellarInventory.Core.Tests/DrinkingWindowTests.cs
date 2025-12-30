namespace WineCellarInventory.Core.Tests;

public class DrinkingWindowTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesDrinkingWindow()
    {
        // Arrange
        var drinkingWindowId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var wineId = Guid.NewGuid();
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2029, 12, 31);
        var notes = "Peak drinking window, excellent aging potential";

        // Act
        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = drinkingWindowId,
            UserId = userId,
            WineId = wineId,
            StartDate = startDate,
            EndDate = endDate,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(drinkingWindow.DrinkingWindowId, Is.EqualTo(drinkingWindowId));
            Assert.That(drinkingWindow.UserId, Is.EqualTo(userId));
            Assert.That(drinkingWindow.WineId, Is.EqualTo(wineId));
            Assert.That(drinkingWindow.StartDate, Is.EqualTo(startDate));
            Assert.That(drinkingWindow.EndDate, Is.EqualTo(endDate));
            Assert.That(drinkingWindow.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Notes_NullValue_IsAllowed()
    {
        // Arrange & Act
        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(5),
            Notes = null
        };

        // Assert
        Assert.That(drinkingWindow.Notes, Is.Null);
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSetToUtcNow()
    {
        // Arrange & Act
        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(5)
        };

        // Assert
        Assert.That(drinkingWindow.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void StartDate_BeforeEndDate_IsValid()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2029, 12, 31);

        // Act
        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            StartDate = startDate,
            EndDate = endDate
        };

        // Assert
        Assert.That(drinkingWindow.StartDate, Is.LessThan(drinkingWindow.EndDate));
    }

    [Test]
    public void DrinkingWindow_ShortTerm_CanBeSet()
    {
        // Arrange
        var startDate = DateTime.UtcNow;
        var endDate = DateTime.UtcNow.AddMonths(6);

        // Act
        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            StartDate = startDate,
            EndDate = endDate,
            Notes = "Drink young and fresh"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(drinkingWindow.StartDate, Is.EqualTo(startDate));
            Assert.That(drinkingWindow.EndDate, Is.EqualTo(endDate));
        });
    }

    [Test]
    public void DrinkingWindow_LongTerm_CanBeSet()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddYears(5);
        var endDate = DateTime.UtcNow.AddYears(20);

        // Act
        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            StartDate = startDate,
            EndDate = endDate,
            Notes = "Long aging potential"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(drinkingWindow.StartDate, Is.EqualTo(startDate));
            Assert.That(drinkingWindow.EndDate, Is.EqualTo(endDate));
        });
    }

    [Test]
    public void DrinkingWindow_WithWineNavigation_CanBeSet()
    {
        // Arrange
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Wine"
        };

        // Act
        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = wine.UserId,
            WineId = wine.WineId,
            Wine = wine,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(5)
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(drinkingWindow.Wine, Is.Not.Null);
            Assert.That(drinkingWindow.Wine.WineId, Is.EqualTo(wine.WineId));
        });
    }

    [Test]
    public void DrinkingWindow_CurrentPeriod_IsValid()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddMonths(-6);
        var endDate = DateTime.UtcNow.AddMonths(6);

        // Act
        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            StartDate = startDate,
            EndDate = endDate,
            Notes = "Currently in optimal drinking window"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(DateTime.UtcNow, Is.GreaterThanOrEqualTo(drinkingWindow.StartDate));
            Assert.That(DateTime.UtcNow, Is.LessThanOrEqualTo(drinkingWindow.EndDate));
        });
    }

    [Test]
    public void DrinkingWindow_FuturePeriod_IsValid()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddYears(2);
        var endDate = DateTime.UtcNow.AddYears(7);

        // Act
        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            StartDate = startDate,
            EndDate = endDate,
            Notes = "Not yet ready, needs aging"
        };

        // Assert
        Assert.That(DateTime.UtcNow, Is.LessThan(drinkingWindow.StartDate));
    }

    [Test]
    public void DrinkingWindow_WithDetailedNotes_CanBeSet()
    {
        // Arrange
        var notes = "2024-2029: Optimal drinking window. " +
                   "Wine will show its best characteristics during this period. " +
                   "Expect complex aromas and balanced tannins.";

        // Act
        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2029, 12, 31),
            Notes = notes
        };

        // Assert
        Assert.That(drinkingWindow.Notes, Is.EqualTo(notes));
    }
}
