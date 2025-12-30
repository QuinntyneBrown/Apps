namespace DateNightIdeaGenerator.Core.Tests;

public class EventTests
{
    [Test]
    public void DateIdeaCreatedEvent_CanBeCreated()
    {
        // Arrange
        var dateIdeaId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Romantic Dinner";
        var category = Category.Romantic;
        var budgetRange = BudgetRange.Medium;

        // Act
        var evt = new DateIdeaCreatedEvent
        {
            DateIdeaId = dateIdeaId,
            UserId = userId,
            Title = title,
            Category = category,
            BudgetRange = budgetRange
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DateIdeaId, Is.EqualTo(dateIdeaId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.Category, Is.EqualTo(category));
            Assert.That(evt.BudgetRange, Is.EqualTo(budgetRange));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void DateIdeaMarkedAsFavoriteEvent_CanBeCreated()
    {
        // Arrange
        var dateIdeaId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new DateIdeaMarkedAsFavoriteEvent
        {
            DateIdeaId = dateIdeaId,
            UserId = userId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DateIdeaId, Is.EqualTo(dateIdeaId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void DateIdeaMarkedAsTriedEvent_CanBeCreated()
    {
        // Arrange
        var dateIdeaId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new DateIdeaMarkedAsTriedEvent
        {
            DateIdeaId = dateIdeaId,
            UserId = userId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DateIdeaId, Is.EqualTo(dateIdeaId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ExperienceCreatedEvent_CanBeCreated()
    {
        // Arrange
        var experienceId = Guid.NewGuid();
        var dateIdeaId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var experienceDate = DateTime.UtcNow;

        // Act
        var evt = new ExperienceCreatedEvent
        {
            ExperienceId = experienceId,
            DateIdeaId = dateIdeaId,
            UserId = userId,
            ExperienceDate = experienceDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ExperienceId, Is.EqualTo(experienceId));
            Assert.That(evt.DateIdeaId, Is.EqualTo(dateIdeaId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.ExperienceDate, Is.EqualTo(experienceDate));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ExperienceUpdatedEvent_CanBeCreated()
    {
        // Arrange
        var experienceId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new ExperienceUpdatedEvent
        {
            ExperienceId = experienceId,
            UserId = userId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ExperienceId, Is.EqualTo(experienceId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void RatingCreatedEvent_CanBeCreated()
    {
        // Arrange
        var ratingId = Guid.NewGuid();
        var dateIdeaId = Guid.NewGuid();
        var experienceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var score = 5;

        // Act
        var evt = new RatingCreatedEvent
        {
            RatingId = ratingId,
            DateIdeaId = dateIdeaId,
            ExperienceId = experienceId,
            UserId = userId,
            Score = score
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RatingId, Is.EqualTo(ratingId));
            Assert.That(evt.DateIdeaId, Is.EqualTo(dateIdeaId));
            Assert.That(evt.ExperienceId, Is.EqualTo(experienceId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Score, Is.EqualTo(score));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void RatingCreatedEvent_WithNullDateIdeaId_CanBeCreated()
    {
        // Arrange & Act
        var evt = new RatingCreatedEvent
        {
            RatingId = Guid.NewGuid(),
            DateIdeaId = null,
            ExperienceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Score = 4
        };

        // Assert
        Assert.That(evt.DateIdeaId, Is.Null);
    }

    [Test]
    public void RatingCreatedEvent_WithNullExperienceId_CanBeCreated()
    {
        // Arrange & Act
        var evt = new RatingCreatedEvent
        {
            RatingId = Guid.NewGuid(),
            DateIdeaId = Guid.NewGuid(),
            ExperienceId = null,
            UserId = Guid.NewGuid(),
            Score = 4
        };

        // Assert
        Assert.That(evt.ExperienceId, Is.Null);
    }

    [Test]
    public void RatingUpdatedEvent_CanBeCreated()
    {
        // Arrange
        var ratingId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var newScore = 4;

        // Act
        var evt = new RatingUpdatedEvent
        {
            RatingId = ratingId,
            UserId = userId,
            NewScore = newScore
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RatingId, Is.EqualTo(ratingId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.NewScore, Is.EqualTo(newScore));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Events_AreRecords()
    {
        // This ensures events are immutable and have value-based equality
        var dateIdeaId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var event1 = new DateIdeaMarkedAsFavoriteEvent
        {
            DateIdeaId = dateIdeaId,
            UserId = userId,
            Timestamp = new DateTime(2024, 1, 1)
        };

        var event2 = new DateIdeaMarkedAsFavoriteEvent
        {
            DateIdeaId = dateIdeaId,
            UserId = userId,
            Timestamp = new DateTime(2024, 1, 1)
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void DateIdeaCreatedEvent_AllCategories_CanBeUsed()
    {
        // Arrange & Act
        var outdoorEvent = new DateIdeaCreatedEvent { Category = Category.Outdoor };
        var romanticEvent = new DateIdeaCreatedEvent { Category = Category.Romantic };
        var culturalEvent = new DateIdeaCreatedEvent { Category = Category.Cultural };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(outdoorEvent.Category, Is.EqualTo(Category.Outdoor));
            Assert.That(romanticEvent.Category, Is.EqualTo(Category.Romantic));
            Assert.That(culturalEvent.Category, Is.EqualTo(Category.Cultural));
        });
    }

    [Test]
    public void DateIdeaCreatedEvent_AllBudgetRanges_CanBeUsed()
    {
        // Arrange & Act
        var freeEvent = new DateIdeaCreatedEvent { BudgetRange = BudgetRange.Free };
        var lowEvent = new DateIdeaCreatedEvent { BudgetRange = BudgetRange.Low };
        var mediumEvent = new DateIdeaCreatedEvent { BudgetRange = BudgetRange.Medium };
        var highEvent = new DateIdeaCreatedEvent { BudgetRange = BudgetRange.High };
        var premiumEvent = new DateIdeaCreatedEvent { BudgetRange = BudgetRange.Premium };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(freeEvent.BudgetRange, Is.EqualTo(BudgetRange.Free));
            Assert.That(lowEvent.BudgetRange, Is.EqualTo(BudgetRange.Low));
            Assert.That(mediumEvent.BudgetRange, Is.EqualTo(BudgetRange.Medium));
            Assert.That(highEvent.BudgetRange, Is.EqualTo(BudgetRange.High));
            Assert.That(premiumEvent.BudgetRange, Is.EqualTo(BudgetRange.Premium));
        });
    }
}
