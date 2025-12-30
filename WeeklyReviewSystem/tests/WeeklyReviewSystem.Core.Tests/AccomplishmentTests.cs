namespace WeeklyReviewSystem.Core.Tests;

public class AccomplishmentTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesAccomplishment()
    {
        // Arrange
        var accomplishmentId = Guid.NewGuid();
        var reviewId = Guid.NewGuid();
        var title = "Completed major project";
        var description = "Successfully delivered the project on time";
        var category = "Work";
        var impactLevel = 9;

        // Act
        var accomplishment = new Accomplishment
        {
            AccomplishmentId = accomplishmentId,
            WeeklyReviewId = reviewId,
            Title = title,
            Description = description,
            Category = category,
            ImpactLevel = impactLevel
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(accomplishment.AccomplishmentId, Is.EqualTo(accomplishmentId));
            Assert.That(accomplishment.WeeklyReviewId, Is.EqualTo(reviewId));
            Assert.That(accomplishment.Title, Is.EqualTo(title));
            Assert.That(accomplishment.Description, Is.EqualTo(description));
            Assert.That(accomplishment.Category, Is.EqualTo(category));
            Assert.That(accomplishment.ImpactLevel, Is.EqualTo(impactLevel));
        });
    }

    [Test]
    public void Title_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var accomplishment = new Accomplishment();

        // Assert
        Assert.That(accomplishment.Title, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Description_NullValue_IsAllowed()
    {
        // Arrange & Act
        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test",
            Description = null
        };

        // Assert
        Assert.That(accomplishment.Description, Is.Null);
    }

    [Test]
    public void Category_NullValue_IsAllowed()
    {
        // Arrange & Act
        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test",
            Category = null
        };

        // Assert
        Assert.That(accomplishment.Category, Is.Null);
    }

    [Test]
    public void ImpactLevel_ValidRange_CanBeSet()
    {
        // Arrange & Act
        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test",
            ImpactLevel = 8
        };

        // Assert
        Assert.That(accomplishment.ImpactLevel, Is.EqualTo(8));
    }

    [Test]
    public void ImpactLevel_NullValue_IsAllowed()
    {
        // Arrange & Act
        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test",
            ImpactLevel = null
        };

        // Assert
        Assert.That(accomplishment.ImpactLevel, Is.Null);
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSetToUtcNow()
    {
        // Arrange & Act
        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Test"
        };

        // Assert
        Assert.That(accomplishment.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Accomplishment_DifferentCategories_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var work = new Accomplishment { Category = "Work" };
            Assert.That(work.Category, Is.EqualTo("Work"));

            var personal = new Accomplishment { Category = "Personal" };
            Assert.That(personal.Category, Is.EqualTo("Personal"));

            var health = new Accomplishment { Category = "Health" };
            Assert.That(health.Category, Is.EqualTo("Health"));

            var learning = new Accomplishment { Category = "Learning" };
            Assert.That(learning.Category, Is.EqualTo("Learning"));
        });
    }

    [Test]
    public void Accomplishment_HighImpactLevel_CanBeSet()
    {
        // Arrange & Act
        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Major breakthrough",
            ImpactLevel = 10
        };

        // Assert
        Assert.That(accomplishment.ImpactLevel, Is.EqualTo(10));
    }

    [Test]
    public void Accomplishment_LowImpactLevel_CanBeSet()
    {
        // Arrange & Act
        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Small task",
            ImpactLevel = 1
        };

        // Assert
        Assert.That(accomplishment.ImpactLevel, Is.EqualTo(1));
    }
}
