// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Core.Tests;

public class TimeBlockTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTimeBlock()
    {
        // Arrange
        var timeBlockId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var category = ActivityCategory.Work;
        var description = "Working on project";
        var startTime = DateTime.UtcNow;
        var notes = "Productive session";
        var tags = "development,coding";
        var isProductive = true;

        // Act
        var timeBlock = new TimeBlock
        {
            TimeBlockId = timeBlockId,
            UserId = userId,
            Category = category,
            Description = description,
            StartTime = startTime,
            Notes = notes,
            Tags = tags,
            IsProductive = isProductive
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(timeBlock.TimeBlockId, Is.EqualTo(timeBlockId));
            Assert.That(timeBlock.UserId, Is.EqualTo(userId));
            Assert.That(timeBlock.Category, Is.EqualTo(category));
            Assert.That(timeBlock.Description, Is.EqualTo(description));
            Assert.That(timeBlock.StartTime, Is.EqualTo(startTime));
            Assert.That(timeBlock.EndTime, Is.Null);
            Assert.That(timeBlock.Notes, Is.EqualTo(notes));
            Assert.That(timeBlock.Tags, Is.EqualTo(tags));
            Assert.That(timeBlock.IsProductive, Is.True);
        });
    }

    [Test]
    public void GetDurationInMinutes_WhenNotEnded_ReturnsNull()
    {
        // Arrange
        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = DateTime.UtcNow
        };

        // Act
        var duration = timeBlock.GetDurationInMinutes();

        // Assert
        Assert.That(duration, Is.Null);
    }

    [Test]
    public void GetDurationInMinutes_WhenEnded_ReturnsCorrectDuration()
    {
        // Arrange
        var startTime = new DateTime(2024, 3, 15, 10, 0, 0);
        var endTime = new DateTime(2024, 3, 15, 11, 30, 0);

        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = startTime,
            EndTime = endTime
        };

        // Act
        var duration = timeBlock.GetDurationInMinutes();

        // Assert
        Assert.That(duration, Is.EqualTo(90.0));
    }

    [Test]
    public void EndActivity_WithValidEndTime_SetsEndTime()
    {
        // Arrange
        var startTime = DateTime.UtcNow;
        var endTime = startTime.AddHours(2);

        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = startTime
        };

        // Act
        timeBlock.EndActivity(endTime);

        // Assert
        Assert.That(timeBlock.EndTime, Is.EqualTo(endTime));
    }

    [Test]
    public void EndActivity_WithEndTimeBeforeStartTime_ThrowsException()
    {
        // Arrange
        var startTime = DateTime.UtcNow;
        var endTime = startTime.AddHours(-1);

        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = startTime
        };

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => timeBlock.EndActivity(endTime));
        Assert.That(ex.Message, Does.Contain("End time must be after start time"));
    }

    [Test]
    public void EndActivity_WithEndTimeEqualToStartTime_ThrowsException()
    {
        // Arrange
        var startTime = DateTime.UtcNow;
        var endTime = startTime;

        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = startTime
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => timeBlock.EndActivity(endTime));
    }

    [Test]
    public void IsActive_WhenNotEnded_ReturnsTrue()
    {
        // Arrange
        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = DateTime.UtcNow
        };

        // Act
        var isActive = timeBlock.IsActive();

        // Assert
        Assert.That(isActive, Is.True);
    }

    [Test]
    public void IsActive_WhenEnded_ReturnsFalse()
    {
        // Arrange
        var startTime = DateTime.UtcNow;
        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = startTime
        };

        timeBlock.EndActivity(startTime.AddHours(1));

        // Act
        var isActive = timeBlock.IsActive();

        // Assert
        Assert.That(isActive, Is.False);
    }

    [Test]
    public void ActivityCategory_AllValuesCanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new TimeBlock { Category = ActivityCategory.Work }, Throws.Nothing);
            Assert.That(() => new TimeBlock { Category = ActivityCategory.PersonalDevelopment }, Throws.Nothing);
            Assert.That(() => new TimeBlock { Category = ActivityCategory.Exercise }, Throws.Nothing);
            Assert.That(() => new TimeBlock { Category = ActivityCategory.Social }, Throws.Nothing);
            Assert.That(() => new TimeBlock { Category = ActivityCategory.Entertainment }, Throws.Nothing);
            Assert.That(() => new TimeBlock { Category = ActivityCategory.Household }, Throws.Nothing);
            Assert.That(() => new TimeBlock { Category = ActivityCategory.Sleep }, Throws.Nothing);
            Assert.That(() => new TimeBlock { Category = ActivityCategory.Meals }, Throws.Nothing);
            Assert.That(() => new TimeBlock { Category = ActivityCategory.Commute }, Throws.Nothing);
            Assert.That(() => new TimeBlock { Category = ActivityCategory.Other }, Throws.Nothing);
        });
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = DateTime.UtcNow
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(timeBlock.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(timeBlock.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void TimeBlock_WithOptionalFieldsNull_IsValid()
    {
        // Arrange & Act
        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = DateTime.UtcNow
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(timeBlock.Notes, Is.Null);
            Assert.That(timeBlock.Tags, Is.Null);
            Assert.That(timeBlock.EndTime, Is.Null);
        });
    }

    [Test]
    public void GetDurationInMinutes_ShortDuration_ReturnsCorrectValue()
    {
        // Arrange
        var startTime = new DateTime(2024, 3, 15, 10, 0, 0);
        var endTime = new DateTime(2024, 3, 15, 10, 15, 0);

        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = startTime,
            EndTime = endTime
        };

        // Act
        var duration = timeBlock.GetDurationInMinutes();

        // Assert
        Assert.That(duration, Is.EqualTo(15.0));
    }

    [Test]
    public void GetDurationInMinutes_LongDuration_ReturnsCorrectValue()
    {
        // Arrange
        var startTime = new DateTime(2024, 3, 15, 8, 0, 0);
        var endTime = new DateTime(2024, 3, 15, 17, 30, 0);

        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = startTime,
            EndTime = endTime
        };

        // Act
        var duration = timeBlock.GetDurationInMinutes();

        // Assert
        Assert.That(duration, Is.EqualTo(570.0)); // 9.5 hours = 570 minutes
    }

    [Test]
    public void IsProductive_CanBeSetToFalse()
    {
        // Arrange & Act
        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Entertainment,
            Description = "Watching TV",
            StartTime = DateTime.UtcNow,
            IsProductive = false
        };

        // Assert
        Assert.That(timeBlock.IsProductive, Is.False);
    }
}
