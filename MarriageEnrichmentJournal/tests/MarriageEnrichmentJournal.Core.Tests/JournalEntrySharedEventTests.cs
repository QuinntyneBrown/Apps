// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core.Tests;

public class JournalEntrySharedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var journalEntryId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new JournalEntrySharedEvent
        {
            JournalEntryId = journalEntryId,
            UserId = userId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.JournalEntryId, Is.EqualTo(journalEntryId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Timestamp_IsSetAutomatically()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new JournalEntrySharedEvent
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
        var evt = new JournalEntrySharedEvent { JournalEntryId = expectedId };

        // Assert
        Assert.That(evt.JournalEntryId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();

        // Act
        var evt = new JournalEntrySharedEvent { UserId = expectedUserId };

        // Assert
        Assert.That(evt.UserId, Is.EqualTo(expectedUserId));
    }
}
