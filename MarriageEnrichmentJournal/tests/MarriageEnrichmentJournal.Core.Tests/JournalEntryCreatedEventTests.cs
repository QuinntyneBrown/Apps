// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core.Tests;

public class JournalEntryCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var journalEntryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Weekly Reflection";
        var entryType = EntryType.Reflection;

        // Act
        var evt = new JournalEntryCreatedEvent
        {
            JournalEntryId = journalEntryId,
            UserId = userId,
            Title = title,
            EntryType = entryType
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.JournalEntryId, Is.EqualTo(journalEntryId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.EntryType, Is.EqualTo(entryType));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Timestamp_IsSetAutomatically()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new JournalEntryCreatedEvent
        {
            JournalEntryId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void JournalEntryId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var evt = new JournalEntryCreatedEvent { JournalEntryId = expectedId };

        // Assert
        Assert.That(evt.JournalEntryId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();

        // Act
        var evt = new JournalEntryCreatedEvent { UserId = expectedUserId };

        // Assert
        Assert.That(evt.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Title_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedTitle = "Marriage Goals";

        // Act
        var evt = new JournalEntryCreatedEvent { Title = expectedTitle };

        // Assert
        Assert.That(evt.Title, Is.EqualTo(expectedTitle));
    }

    [Test]
    public void EntryType_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedEntryType = EntryType.Goal;

        // Act
        var evt = new JournalEntryCreatedEvent { EntryType = expectedEntryType };

        // Assert
        Assert.That(evt.EntryType, Is.EqualTo(expectedEntryType));
    }
}
