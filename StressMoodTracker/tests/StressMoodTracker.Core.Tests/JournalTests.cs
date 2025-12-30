// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace StressMoodTracker.Core.Tests;

public class JournalTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesJournal()
    {
        // Arrange
        var journalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Today's Reflection";
        var content = "Had a great day today. Feeling grateful for the positive experiences.";
        var entryDate = new DateTime(2024, 1, 15, 20, 30, 0);
        var tags = "gratitude,positive,reflection";

        // Act
        var journal = new Journal
        {
            JournalId = journalId,
            UserId = userId,
            Title = title,
            Content = content,
            EntryDate = entryDate,
            Tags = tags
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(journal.JournalId, Is.EqualTo(journalId));
            Assert.That(journal.UserId, Is.EqualTo(userId));
            Assert.That(journal.Title, Is.EqualTo(title));
            Assert.That(journal.Content, Is.EqualTo(content));
            Assert.That(journal.EntryDate, Is.EqualTo(entryDate));
            Assert.That(journal.Tags, Is.EqualTo(tags));
        });
    }

    [Test]
    public void DefaultValues_NewJournal_HasExpectedDefaults()
    {
        // Act
        var journal = new Journal();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(journal.Title, Is.EqualTo(string.Empty));
            Assert.That(journal.Content, Is.EqualTo(string.Empty));
            Assert.That(journal.EntryDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(journal.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void GetWordCount_EmptyContent_ReturnsZero()
    {
        // Arrange
        var journal = new Journal
        {
            Content = ""
        };

        // Act
        var wordCount = journal.GetWordCount();

        // Assert
        Assert.That(wordCount, Is.EqualTo(0));
    }

    [Test]
    public void GetWordCount_SingleWord_ReturnsOne()
    {
        // Arrange
        var journal = new Journal
        {
            Content = "Hello"
        };

        // Act
        var wordCount = journal.GetWordCount();

        // Assert
        Assert.That(wordCount, Is.EqualTo(1));
    }

    [Test]
    public void GetWordCount_MultipleWords_ReturnsCorrectCount()
    {
        // Arrange
        var journal = new Journal
        {
            Content = "This is a test journal entry"
        };

        // Act
        var wordCount = journal.GetWordCount();

        // Assert
        Assert.That(wordCount, Is.EqualTo(6));
    }

    [Test]
    public void GetWordCount_WithExtraSpaces_ReturnsCorrectCount()
    {
        // Arrange
        var journal = new Journal
        {
            Content = "This  has   extra    spaces"
        };

        // Act
        var wordCount = journal.GetWordCount();

        // Assert
        Assert.That(wordCount, Is.EqualTo(4));
    }

    [Test]
    public void GetWordCount_WithNewlines_ReturnsCorrectCount()
    {
        // Arrange
        var journal = new Journal
        {
            Content = "First line\nSecond line\rThird line"
        };

        // Act
        var wordCount = journal.GetWordCount();

        // Assert
        Assert.That(wordCount, Is.EqualTo(6));
    }

    [Test]
    public void GetWordCount_WithTabsAndNewlines_ReturnsCorrectCount()
    {
        // Arrange
        var journal = new Journal
        {
            Content = "Word1\tWord2\nWord3\rWord4"
        };

        // Act
        var wordCount = journal.GetWordCount();

        // Assert
        Assert.That(wordCount, Is.EqualTo(4));
    }

    [Test]
    public void GetWordCount_LongEntry_ReturnsCorrectCount()
    {
        // Arrange
        var journal = new Journal
        {
            Content = "Today was a challenging day at work. I faced several difficult situations but managed to stay calm and focused. I'm proud of how I handled the stress."
        };

        // Act
        var wordCount = journal.GetWordCount();

        // Assert
        Assert.That(wordCount, Is.EqualTo(29));
    }

    [Test]
    public void Tags_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var journal = new Journal
        {
            Tags = null
        };

        // Assert
        Assert.That(journal.Tags, Is.Null);
    }

    [Test]
    public void Tags_CanStoreCommaSeparatedValues()
    {
        // Arrange
        var tags = "stress,work,anxiety,coping";

        // Act
        var journal = new Journal
        {
            Tags = tags
        };

        // Assert
        Assert.That(journal.Tags, Is.EqualTo(tags));
    }

    [Test]
    public void Title_CanStoreValue()
    {
        // Arrange
        var title = "Weekly Reflection";

        // Act
        var journal = new Journal
        {
            Title = title
        };

        // Assert
        Assert.That(journal.Title, Is.EqualTo(title));
    }

    [Test]
    public void Content_CanStoreLongText()
    {
        // Arrange
        var longContent = string.Join(" ", Enumerable.Repeat("word", 100));

        // Act
        var journal = new Journal
        {
            Content = longContent
        };

        // Assert
        Assert.That(journal.Content, Is.EqualTo(longContent));
    }

    [Test]
    public void EntryDate_CanStorePastDate()
    {
        // Arrange
        var pastDate = new DateTime(2024, 1, 1);

        // Act
        var journal = new Journal
        {
            EntryDate = pastDate
        };

        // Assert
        Assert.That(journal.EntryDate, Is.EqualTo(pastDate));
    }

    [Test]
    public void UserId_CanBeAssociated()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var journal = new Journal
        {
            UserId = userId
        };

        // Assert
        Assert.That(journal.UserId, Is.EqualTo(userId));
    }

    [Test]
    public void CreatedAt_AutomaticallySet_OnCreation()
    {
        // Arrange & Act
        var journal = new Journal();

        // Assert
        Assert.That(journal.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }
}
