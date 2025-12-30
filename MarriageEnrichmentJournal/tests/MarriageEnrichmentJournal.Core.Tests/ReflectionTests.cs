// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core.Tests;

public class ReflectionTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesReflection()
    {
        // Arrange
        var reflectionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var journalEntryId = Guid.NewGuid();
        var text = "Reflecting on...";
        var topic = "Communication";

        // Act
        var reflection = new Reflection
        {
            ReflectionId = reflectionId,
            UserId = userId,
            JournalEntryId = journalEntryId,
            Text = text,
            Topic = topic
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reflection.ReflectionId, Is.EqualTo(reflectionId));
            Assert.That(reflection.UserId, Is.EqualTo(userId));
            Assert.That(reflection.JournalEntryId, Is.EqualTo(journalEntryId));
            Assert.That(reflection.Text, Is.EqualTo(text));
            Assert.That(reflection.Topic, Is.EqualTo(topic));
            Assert.That(reflection.ReflectionDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(reflection.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ReflectionId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var reflection = new Reflection();
        var expectedId = Guid.NewGuid();

        // Act
        reflection.ReflectionId = expectedId;

        // Assert
        Assert.That(reflection.ReflectionId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var reflection = new Reflection();
        var expectedUserId = Guid.NewGuid();

        // Act
        reflection.UserId = expectedUserId;

        // Assert
        Assert.That(reflection.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void JournalEntryId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var reflection = new Reflection();
        var expectedJournalEntryId = Guid.NewGuid();

        // Act
        reflection.JournalEntryId = expectedJournalEntryId;

        // Assert
        Assert.That(reflection.JournalEntryId, Is.EqualTo(expectedJournalEntryId));
    }

    [Test]
    public void JournalEntryId_CanBeNull()
    {
        // Arrange
        var reflection = new Reflection();

        // Act
        reflection.JournalEntryId = null;

        // Assert
        Assert.That(reflection.JournalEntryId, Is.Null);
    }

    [Test]
    public void Text_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var reflection = new Reflection();
        var expectedText = "Our relationship has grown...";

        // Act
        reflection.Text = expectedText;

        // Assert
        Assert.That(reflection.Text, Is.EqualTo(expectedText));
    }

    [Test]
    public void Topic_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var reflection = new Reflection();
        var expectedTopic = "Trust";

        // Act
        reflection.Topic = expectedTopic;

        // Assert
        Assert.That(reflection.Topic, Is.EqualTo(expectedTopic));
    }

    [Test]
    public void Topic_CanBeNull()
    {
        // Arrange
        var reflection = new Reflection();

        // Act
        reflection.Topic = null;

        // Assert
        Assert.That(reflection.Topic, Is.Null);
    }

    [Test]
    public void ReflectionDate_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var reflection = new Reflection();
        var expectedDate = DateTime.UtcNow.AddDays(-2);

        // Act
        reflection.ReflectionDate = expectedDate;

        // Assert
        Assert.That(reflection.ReflectionDate, Is.EqualTo(expectedDate));
    }

    [Test]
    public void JournalEntry_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var reflection = new Reflection();
        var journalEntry = new JournalEntry { JournalEntryId = Guid.NewGuid() };

        // Act
        reflection.JournalEntry = journalEntry;

        // Assert
        Assert.That(reflection.JournalEntry, Is.EqualTo(journalEntry));
    }

    [Test]
    public void JournalEntry_CanBeNull()
    {
        // Arrange
        var reflection = new Reflection();

        // Act
        reflection.JournalEntry = null;

        // Assert
        Assert.That(reflection.JournalEntry, Is.Null);
    }

    [Test]
    public void UpdatedAt_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var reflection = new Reflection();
        var expectedUpdatedAt = DateTime.UtcNow;

        // Act
        reflection.UpdatedAt = expectedUpdatedAt;

        // Assert
        Assert.That(reflection.UpdatedAt, Is.EqualTo(expectedUpdatedAt));
    }

    [Test]
    public void UpdatedAt_CanBeNull()
    {
        // Arrange & Act
        var reflection = new Reflection();

        // Assert
        Assert.That(reflection.UpdatedAt, Is.Null);
    }
}
