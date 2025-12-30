// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core.Tests;

public class EntryTypeTests
{
    [Test]
    public void EntryType_General_HasCorrectValue()
    {
        // Arrange & Act
        var entryType = EntryType.General;

        // Assert
        Assert.That((int)entryType, Is.EqualTo(0));
    }

    [Test]
    public void EntryType_Gratitude_HasCorrectValue()
    {
        // Arrange & Act
        var entryType = EntryType.Gratitude;

        // Assert
        Assert.That((int)entryType, Is.EqualTo(1));
    }

    [Test]
    public void EntryType_Reflection_HasCorrectValue()
    {
        // Arrange & Act
        var entryType = EntryType.Reflection;

        // Assert
        Assert.That((int)entryType, Is.EqualTo(2));
    }

    [Test]
    public void EntryType_Prayer_HasCorrectValue()
    {
        // Arrange & Act
        var entryType = EntryType.Prayer;

        // Assert
        Assert.That((int)entryType, Is.EqualTo(3));
    }

    [Test]
    public void EntryType_Goal_HasCorrectValue()
    {
        // Arrange & Act
        var entryType = EntryType.Goal;

        // Assert
        Assert.That((int)entryType, Is.EqualTo(4));
    }

    [Test]
    public void EntryType_Challenge_HasCorrectValue()
    {
        // Arrange & Act
        var entryType = EntryType.Challenge;

        // Assert
        Assert.That((int)entryType, Is.EqualTo(5));
    }

    [Test]
    public void EntryType_Celebration_HasCorrectValue()
    {
        // Arrange & Act
        var entryType = EntryType.Celebration;

        // Assert
        Assert.That((int)entryType, Is.EqualTo(6));
    }

    [Test]
    public void EntryType_CanBeAssignedToJournalEntry()
    {
        // Arrange
        var entry = new JournalEntry();

        // Act
        entry.EntryType = EntryType.Gratitude;

        // Assert
        Assert.That(entry.EntryType, Is.EqualTo(EntryType.Gratitude));
    }

    [Test]
    public void EntryType_AllValuesCanBeAssigned()
    {
        // Arrange
        var entry = new JournalEntry();
        var allTypes = new[]
        {
            EntryType.General,
            EntryType.Gratitude,
            EntryType.Reflection,
            EntryType.Prayer,
            EntryType.Goal,
            EntryType.Challenge,
            EntryType.Celebration
        };

        // Act & Assert
        foreach (var type in allTypes)
        {
            entry.EntryType = type;
            Assert.That(entry.EntryType, Is.EqualTo(type));
        }
    }
}
