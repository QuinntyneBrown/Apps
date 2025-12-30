namespace WineCellarInventory.Core.Tests;

public class TastingNoteTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTastingNote()
    {
        // Arrange
        var tastingNoteId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var wineId = Guid.NewGuid();
        var tastingDate = new DateTime(2024, 1, 15);
        var rating = 92;
        var appearance = "Deep ruby red with slight purple hues";
        var aroma = "Black cherry, vanilla, and hints of tobacco";
        var taste = "Rich and full-bodied with balanced tannins";
        var finish = "Long and elegant finish";
        var overallImpression = "Excellent wine, worth cellaring";

        // Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = tastingNoteId,
            UserId = userId,
            WineId = wineId,
            TastingDate = tastingDate,
            Rating = rating,
            Appearance = appearance,
            Aroma = aroma,
            Taste = taste,
            Finish = finish,
            OverallImpression = overallImpression
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tastingNote.TastingNoteId, Is.EqualTo(tastingNoteId));
            Assert.That(tastingNote.UserId, Is.EqualTo(userId));
            Assert.That(tastingNote.WineId, Is.EqualTo(wineId));
            Assert.That(tastingNote.TastingDate, Is.EqualTo(tastingDate));
            Assert.That(tastingNote.Rating, Is.EqualTo(rating));
            Assert.That(tastingNote.Appearance, Is.EqualTo(appearance));
            Assert.That(tastingNote.Aroma, Is.EqualTo(aroma));
            Assert.That(tastingNote.Taste, Is.EqualTo(taste));
            Assert.That(tastingNote.Finish, Is.EqualTo(finish));
            Assert.That(tastingNote.OverallImpression, Is.EqualTo(overallImpression));
        });
    }

    [Test]
    public void TastingDate_DefaultValue_IsSetToUtcNow()
    {
        // Arrange & Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            Rating = 85
        };

        // Assert
        Assert.That(tastingNote.TastingDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSetToUtcNow()
    {
        // Arrange & Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            Rating = 85
        };

        // Assert
        Assert.That(tastingNote.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Appearance_NullValue_IsAllowed()
    {
        // Arrange & Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            Rating = 85,
            Appearance = null
        };

        // Assert
        Assert.That(tastingNote.Appearance, Is.Null);
    }

    [Test]
    public void Aroma_NullValue_IsAllowed()
    {
        // Arrange & Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            Rating = 85,
            Aroma = null
        };

        // Assert
        Assert.That(tastingNote.Aroma, Is.Null);
    }

    [Test]
    public void Taste_NullValue_IsAllowed()
    {
        // Arrange & Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            Rating = 85,
            Taste = null
        };

        // Assert
        Assert.That(tastingNote.Taste, Is.Null);
    }

    [Test]
    public void Finish_NullValue_IsAllowed()
    {
        // Arrange & Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            Rating = 85,
            Finish = null
        };

        // Assert
        Assert.That(tastingNote.Finish, Is.Null);
    }

    [Test]
    public void OverallImpression_NullValue_IsAllowed()
    {
        // Arrange & Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            Rating = 85,
            OverallImpression = null
        };

        // Assert
        Assert.That(tastingNote.OverallImpression, Is.Null);
    }

    [Test]
    public void Rating_HighScore_CanBeSet()
    {
        // Arrange & Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            Rating = 100
        };

        // Assert
        Assert.That(tastingNote.Rating, Is.EqualTo(100));
    }

    [Test]
    public void Rating_LowScore_CanBeSet()
    {
        // Arrange & Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            Rating = 50
        };

        // Assert
        Assert.That(tastingNote.Rating, Is.EqualTo(50));
    }

    [Test]
    public void TastingNote_WithWineNavigation_CanBeSet()
    {
        // Arrange
        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Wine"
        };

        // Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = wine.UserId,
            WineId = wine.WineId,
            Wine = wine,
            Rating = 88
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tastingNote.Wine, Is.Not.Null);
            Assert.That(tastingNote.Wine.WineId, Is.EqualTo(wine.WineId));
        });
    }

    [Test]
    public void TastingNote_DetailedAppearance_CanBeSet()
    {
        // Arrange
        var appearance = "Bright garnet with medium intensity. " +
                        "Clear and brilliant with no sediment. " +
                        "Medium viscosity on the glass.";

        // Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            Rating = 90,
            Appearance = appearance
        };

        // Assert
        Assert.That(tastingNote.Appearance, Is.EqualTo(appearance));
    }

    [Test]
    public void TastingNote_ComplexAroma_CanBeSet()
    {
        // Arrange
        var aroma = "Primary: Red berries, black cherry, plum. " +
                   "Secondary: Vanilla, toast, smoke from oak. " +
                   "Tertiary: Leather, tobacco, earth.";

        // Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            Rating = 93,
            Aroma = aroma
        };

        // Assert
        Assert.That(tastingNote.Aroma, Is.EqualTo(aroma));
    }

    [Test]
    public void TastingNote_MinimalInformation_IsValid()
    {
        // Arrange & Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            Rating = 85
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tastingNote.Rating, Is.EqualTo(85));
            Assert.That(tastingNote.Appearance, Is.Null);
            Assert.That(tastingNote.Aroma, Is.Null);
            Assert.That(tastingNote.Taste, Is.Null);
            Assert.That(tastingNote.Finish, Is.Null);
            Assert.That(tastingNote.OverallImpression, Is.Null);
        });
    }

    [Test]
    public void TastingNote_PastDate_CanBeSet()
    {
        // Arrange
        var pastDate = new DateTime(2020, 6, 15);

        // Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WineId = Guid.NewGuid(),
            TastingDate = pastDate,
            Rating = 87
        };

        // Assert
        Assert.That(tastingNote.TastingDate, Is.EqualTo(pastDate));
    }
}
