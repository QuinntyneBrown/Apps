// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core.Tests;

public class GratitudeTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesGratitude()
    {
        // Arrange
        var gratitudeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var journalEntryId = Guid.NewGuid();
        var text = "I am grateful for...";

        // Act
        var gratitude = new Gratitude
        {
            GratitudeId = gratitudeId,
            UserId = userId,
            JournalEntryId = journalEntryId,
            Text = text
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(gratitude.GratitudeId, Is.EqualTo(gratitudeId));
            Assert.That(gratitude.UserId, Is.EqualTo(userId));
            Assert.That(gratitude.JournalEntryId, Is.EqualTo(journalEntryId));
            Assert.That(gratitude.Text, Is.EqualTo(text));
            Assert.That(gratitude.GratitudeDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(gratitude.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GratitudeId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var gratitude = new Gratitude();
        var expectedId = Guid.NewGuid();

        // Act
        gratitude.GratitudeId = expectedId;

        // Assert
        Assert.That(gratitude.GratitudeId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var gratitude = new Gratitude();
        var expectedUserId = Guid.NewGuid();

        // Act
        gratitude.UserId = expectedUserId;

        // Assert
        Assert.That(gratitude.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void JournalEntryId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var gratitude = new Gratitude();
        var expectedJournalEntryId = Guid.NewGuid();

        // Act
        gratitude.JournalEntryId = expectedJournalEntryId;

        // Assert
        Assert.That(gratitude.JournalEntryId, Is.EqualTo(expectedJournalEntryId));
    }

    [Test]
    public void JournalEntryId_CanBeNull()
    {
        // Arrange
        var gratitude = new Gratitude();

        // Act
        gratitude.JournalEntryId = null;

        // Assert
        Assert.That(gratitude.JournalEntryId, Is.Null);
    }

    [Test]
    public void Text_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var gratitude = new Gratitude();
        var expectedText = "My partner's kindness";

        // Act
        gratitude.Text = expectedText;

        // Assert
        Assert.That(gratitude.Text, Is.EqualTo(expectedText));
    }

    [Test]
    public void GratitudeDate_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var gratitude = new Gratitude();
        var expectedDate = DateTime.UtcNow.AddDays(-1);

        // Act
        gratitude.GratitudeDate = expectedDate;

        // Assert
        Assert.That(gratitude.GratitudeDate, Is.EqualTo(expectedDate));
    }

    [Test]
    public void JournalEntry_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var gratitude = new Gratitude();
        var journalEntry = new JournalEntry { JournalEntryId = Guid.NewGuid() };

        // Act
        gratitude.JournalEntry = journalEntry;

        // Assert
        Assert.That(gratitude.JournalEntry, Is.EqualTo(journalEntry));
    }

    [Test]
    public void JournalEntry_CanBeNull()
    {
        // Arrange
        var gratitude = new Gratitude();

        // Act
        gratitude.JournalEntry = null;

        // Assert
        Assert.That(gratitude.JournalEntry, Is.Null);
    }
}
