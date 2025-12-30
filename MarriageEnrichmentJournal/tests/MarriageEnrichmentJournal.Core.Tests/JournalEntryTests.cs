// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core.Tests;

public class JournalEntryTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesJournalEntry()
    {
        // Arrange
        var journalEntryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Weekly Reflection";
        var content = "This week has been...";
        var entryType = EntryType.Reflection;

        // Act
        var entry = new JournalEntry
        {
            JournalEntryId = journalEntryId,
            UserId = userId,
            Title = title,
            Content = content,
            EntryType = entryType
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(entry.JournalEntryId, Is.EqualTo(journalEntryId));
            Assert.That(entry.UserId, Is.EqualTo(userId));
            Assert.That(entry.Title, Is.EqualTo(title));
            Assert.That(entry.Content, Is.EqualTo(content));
            Assert.That(entry.EntryType, Is.EqualTo(entryType));
            Assert.That(entry.IsSharedWithPartner, Is.False);
            Assert.That(entry.IsPrivate, Is.False);
            Assert.That(entry.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(entry.EntryDate, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ShareWithPartner_WhenCalled_SetsIsSharedWithPartnerTrueAndUpdatesTimestamp()
    {
        // Arrange
        var entry = new JournalEntry { IsSharedWithPartner = false };
        var beforeSharing = DateTime.UtcNow;

        // Act
        entry.ShareWithPartner();

        var afterSharing = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(entry.IsSharedWithPartner, Is.True);
            Assert.That(entry.UpdatedAt, Is.Not.Null);
            Assert.That(entry.UpdatedAt, Is.GreaterThanOrEqualTo(beforeSharing));
            Assert.That(entry.UpdatedAt, Is.LessThanOrEqualTo(afterSharing));
        });
    }

    [Test]
    public void MarkAsPrivate_WhenCalled_SetsIsPrivateTrueAndIsSharedWithPartnerFalse()
    {
        // Arrange
        var entry = new JournalEntry
        {
            IsPrivate = false,
            IsSharedWithPartner = true
        };
        var beforeMarking = DateTime.UtcNow;

        // Act
        entry.MarkAsPrivate();

        var afterMarking = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(entry.IsPrivate, Is.True);
            Assert.That(entry.IsSharedWithPartner, Is.False);
            Assert.That(entry.UpdatedAt, Is.Not.Null);
            Assert.That(entry.UpdatedAt, Is.GreaterThanOrEqualTo(beforeMarking));
            Assert.That(entry.UpdatedAt, Is.LessThanOrEqualTo(afterMarking));
        });
    }

    [Test]
    public void IsSharedWithPartner_DefaultsToFalse()
    {
        // Arrange & Act
        var entry = new JournalEntry();

        // Assert
        Assert.That(entry.IsSharedWithPartner, Is.False);
    }

    [Test]
    public void IsPrivate_DefaultsToFalse()
    {
        // Arrange & Act
        var entry = new JournalEntry();

        // Assert
        Assert.That(entry.IsPrivate, Is.False);
    }

    [Test]
    public void Gratitudes_DefaultsToEmptyCollection()
    {
        // Arrange & Act
        var entry = new JournalEntry();

        // Assert
        Assert.That(entry.Gratitudes, Is.Not.Null);
        Assert.That(entry.Gratitudes, Is.Empty);
    }

    [Test]
    public void Reflections_DefaultsToEmptyCollection()
    {
        // Arrange & Act
        var entry = new JournalEntry();

        // Assert
        Assert.That(entry.Reflections, Is.Not.Null);
        Assert.That(entry.Reflections, Is.Empty);
    }

    [Test]
    public void Gratitudes_CanAddGratitudes_ReturnsCorrectCount()
    {
        // Arrange
        var entry = new JournalEntry();
        var gratitude = new Gratitude { GratitudeId = Guid.NewGuid() };

        // Act
        entry.Gratitudes.Add(gratitude);

        // Assert
        Assert.That(entry.Gratitudes.Count, Is.EqualTo(1));
        Assert.That(entry.Gratitudes.First(), Is.EqualTo(gratitude));
    }

    [Test]
    public void Reflections_CanAddReflections_ReturnsCorrectCount()
    {
        // Arrange
        var entry = new JournalEntry();
        var reflection = new Reflection { ReflectionId = Guid.NewGuid() };

        // Act
        entry.Reflections.Add(reflection);

        // Assert
        Assert.That(entry.Reflections.Count, Is.EqualTo(1));
        Assert.That(entry.Reflections.First(), Is.EqualTo(reflection));
    }

    [Test]
    public void Tags_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var entry = new JournalEntry();
        var expectedTags = "gratitude,reflection,marriage";

        // Act
        entry.Tags = expectedTags;

        // Assert
        Assert.That(entry.Tags, Is.EqualTo(expectedTags));
    }

    [Test]
    public void Tags_CanBeNull()
    {
        // Arrange
        var entry = new JournalEntry();

        // Act
        entry.Tags = null;

        // Assert
        Assert.That(entry.Tags, Is.Null);
    }

    [Test]
    public void UpdatedAt_DefaultsToNull()
    {
        // Arrange & Act
        var entry = new JournalEntry();

        // Assert
        Assert.That(entry.UpdatedAt, Is.Null);
    }
}
