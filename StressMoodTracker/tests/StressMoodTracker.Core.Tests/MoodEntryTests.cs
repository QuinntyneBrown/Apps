// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace StressMoodTracker.Core.Tests;

public class MoodEntryTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMoodEntry()
    {
        // Arrange
        var moodEntryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var moodLevel = MoodLevel.Good;
        var stressLevel = StressLevel.Low;
        var entryTime = new DateTime(2024, 1, 15, 14, 30, 0);
        var notes = "Feeling good after morning exercise";
        var activities = "Exercise,Meditation,Work";

        // Act
        var moodEntry = new MoodEntry
        {
            MoodEntryId = moodEntryId,
            UserId = userId,
            MoodLevel = moodLevel,
            StressLevel = stressLevel,
            EntryTime = entryTime,
            Notes = notes,
            Activities = activities
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(moodEntry.MoodEntryId, Is.EqualTo(moodEntryId));
            Assert.That(moodEntry.UserId, Is.EqualTo(userId));
            Assert.That(moodEntry.MoodLevel, Is.EqualTo(moodLevel));
            Assert.That(moodEntry.StressLevel, Is.EqualTo(stressLevel));
            Assert.That(moodEntry.EntryTime, Is.EqualTo(entryTime));
            Assert.That(moodEntry.Notes, Is.EqualTo(notes));
            Assert.That(moodEntry.Activities, Is.EqualTo(activities));
        });
    }

    [Test]
    public void DefaultValues_NewMoodEntry_HasExpectedDefaults()
    {
        // Act
        var moodEntry = new MoodEntry();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(moodEntry.MoodLevel, Is.EqualTo(MoodLevel.VeryLow));
            Assert.That(moodEntry.StressLevel, Is.EqualTo(StressLevel.None));
            Assert.That(moodEntry.EntryTime, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(moodEntry.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsPositiveMood_GoodMood_ReturnsTrue()
    {
        // Arrange
        var moodEntry = new MoodEntry
        {
            MoodLevel = MoodLevel.Good
        };

        // Act
        var result = moodEntry.IsPositiveMood();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsPositiveMood_ExcellentMood_ReturnsTrue()
    {
        // Arrange
        var moodEntry = new MoodEntry
        {
            MoodLevel = MoodLevel.Excellent
        };

        // Act
        var result = moodEntry.IsPositiveMood();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsPositiveMood_NeutralMood_ReturnsFalse()
    {
        // Arrange
        var moodEntry = new MoodEntry
        {
            MoodLevel = MoodLevel.Neutral
        };

        // Act
        var result = moodEntry.IsPositiveMood();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsPositiveMood_LowMood_ReturnsFalse()
    {
        // Arrange
        var moodEntry = new MoodEntry
        {
            MoodLevel = MoodLevel.Low
        };

        // Act
        var result = moodEntry.IsPositiveMood();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsPositiveMood_VeryLowMood_ReturnsFalse()
    {
        // Arrange
        var moodEntry = new MoodEntry
        {
            MoodLevel = MoodLevel.VeryLow
        };

        // Act
        var result = moodEntry.IsPositiveMood();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void MoodLevel_AllEnumValues_CanBeAssigned()
    {
        // Arrange
        var moodEntry = new MoodEntry();

        // Act & Assert
        moodEntry.MoodLevel = MoodLevel.VeryLow;
        Assert.That(moodEntry.MoodLevel, Is.EqualTo(MoodLevel.VeryLow));

        moodEntry.MoodLevel = MoodLevel.Low;
        Assert.That(moodEntry.MoodLevel, Is.EqualTo(MoodLevel.Low));

        moodEntry.MoodLevel = MoodLevel.Neutral;
        Assert.That(moodEntry.MoodLevel, Is.EqualTo(MoodLevel.Neutral));

        moodEntry.MoodLevel = MoodLevel.Good;
        Assert.That(moodEntry.MoodLevel, Is.EqualTo(MoodLevel.Good));

        moodEntry.MoodLevel = MoodLevel.Excellent;
        Assert.That(moodEntry.MoodLevel, Is.EqualTo(MoodLevel.Excellent));
    }

    [Test]
    public void StressLevel_AllEnumValues_CanBeAssigned()
    {
        // Arrange
        var moodEntry = new MoodEntry();

        // Act & Assert
        moodEntry.StressLevel = StressLevel.None;
        Assert.That(moodEntry.StressLevel, Is.EqualTo(StressLevel.None));

        moodEntry.StressLevel = StressLevel.Low;
        Assert.That(moodEntry.StressLevel, Is.EqualTo(StressLevel.Low));

        moodEntry.StressLevel = StressLevel.Moderate;
        Assert.That(moodEntry.StressLevel, Is.EqualTo(StressLevel.Moderate));

        moodEntry.StressLevel = StressLevel.High;
        Assert.That(moodEntry.StressLevel, Is.EqualTo(StressLevel.High));

        moodEntry.StressLevel = StressLevel.VeryHigh;
        Assert.That(moodEntry.StressLevel, Is.EqualTo(StressLevel.VeryHigh));
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var moodEntry = new MoodEntry
        {
            Notes = null
        };

        // Assert
        Assert.That(moodEntry.Notes, Is.Null);
    }

    [Test]
    public void Activities_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var moodEntry = new MoodEntry
        {
            Activities = null
        };

        // Assert
        Assert.That(moodEntry.Activities, Is.Null);
    }

    [Test]
    public void Activities_CanStoreCommaSeparatedValues()
    {
        // Arrange
        var activities = "Walking,Reading,Listening to music";

        // Act
        var moodEntry = new MoodEntry
        {
            Activities = activities
        };

        // Assert
        Assert.That(moodEntry.Activities, Is.EqualTo(activities));
    }

    [Test]
    public void EntryTime_CanStorePastTime()
    {
        // Arrange
        var pastTime = new DateTime(2024, 1, 1, 12, 0, 0);

        // Act
        var moodEntry = new MoodEntry
        {
            EntryTime = pastTime
        };

        // Assert
        Assert.That(moodEntry.EntryTime, Is.EqualTo(pastTime));
    }

    [Test]
    public void UserId_CanBeAssociated()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var moodEntry = new MoodEntry
        {
            UserId = userId
        };

        // Assert
        Assert.That(moodEntry.UserId, Is.EqualTo(userId));
    }

    [Test]
    public void CreatedAt_AutomaticallySet_OnCreation()
    {
        // Arrange & Act
        var moodEntry = new MoodEntry();

        // Assert
        Assert.That(moodEntry.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void MoodAndStress_DifferentCombinations_CanBeStored()
    {
        // Arrange & Act
        var entry1 = new MoodEntry { MoodLevel = MoodLevel.Good, StressLevel = StressLevel.Low };
        var entry2 = new MoodEntry { MoodLevel = MoodLevel.Low, StressLevel = StressLevel.High };
        var entry3 = new MoodEntry { MoodLevel = MoodLevel.Neutral, StressLevel = StressLevel.Moderate };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(entry1.MoodLevel, Is.EqualTo(MoodLevel.Good));
            Assert.That(entry1.StressLevel, Is.EqualTo(StressLevel.Low));

            Assert.That(entry2.MoodLevel, Is.EqualTo(MoodLevel.Low));
            Assert.That(entry2.StressLevel, Is.EqualTo(StressLevel.High));

            Assert.That(entry3.MoodLevel, Is.EqualTo(MoodLevel.Neutral));
            Assert.That(entry3.StressLevel, Is.EqualTo(StressLevel.Moderate));
        });
    }
}
