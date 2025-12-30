namespace DailyJournalingApp.Core.Tests;

public class JournalEntryTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesJournalEntry()
    {
        // Arrange
        var entryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Great Day";
        var content = "Today was amazing!";
        var mood = Mood.VeryHappy;
        var tags = "work,gratitude";

        // Act
        var entry = new JournalEntry
        {
            JournalEntryId = entryId,
            UserId = userId,
            Title = title,
            Content = content,
            Mood = mood,
            Tags = tags
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(entry.JournalEntryId, Is.EqualTo(entryId));
            Assert.That(entry.UserId, Is.EqualTo(userId));
            Assert.That(entry.Title, Is.EqualTo(title));
            Assert.That(entry.Content, Is.EqualTo(content));
            Assert.That(entry.Mood, Is.EqualTo(mood));
            Assert.That(entry.Tags, Is.EqualTo(tags));
            Assert.That(entry.IsFavorite, Is.False);
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var entry = new JournalEntry();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(entry.Title, Is.EqualTo(string.Empty));
            Assert.That(entry.Content, Is.EqualTo(string.Empty));
            Assert.That(entry.IsFavorite, Is.False);
            Assert.That(entry.EntryDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(entry.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(entry.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void MarkAsFavorite_UpdatesPropertiesCorrectly()
    {
        // Arrange
        var entry = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            IsFavorite = false
        };
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        entry.MarkAsFavorite();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(entry.IsFavorite, Is.True);
            Assert.That(entry.UpdatedAt, Is.Not.Null);
            Assert.That(entry.UpdatedAt!.Value, Is.GreaterThan(beforeUpdate));
        });
    }

    [Test]
    public void MarkAsFavorite_CalledTwice_RemainsTrue()
    {
        // Arrange
        var entry = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid()
        };

        // Act
        entry.MarkAsFavorite();
        entry.MarkAsFavorite();

        // Assert
        Assert.That(entry.IsFavorite, Is.True);
    }

    [Test]
    public void UpdateMood_ChangesToNewMood()
    {
        // Arrange
        var entry = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            Mood = Mood.Neutral
        };
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        entry.UpdateMood(Mood.Happy);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(entry.Mood, Is.EqualTo(Mood.Happy));
            Assert.That(entry.UpdatedAt, Is.Not.Null);
            Assert.That(entry.UpdatedAt!.Value, Is.GreaterThan(beforeUpdate));
        });
    }

    [Test]
    public void UpdateMood_ToSameMood_UpdatesTimestamp()
    {
        // Arrange
        var entry = new JournalEntry
        {
            JournalEntryId = Guid.NewGuid(),
            Mood = Mood.Happy
        };

        // Act
        entry.UpdateMood(Mood.Happy);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(entry.Mood, Is.EqualTo(Mood.Happy));
            Assert.That(entry.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void UpdateMood_ToVeryHappy_Works()
    {
        // Arrange
        var entry = new JournalEntry { Mood = Mood.Sad };

        // Act
        entry.UpdateMood(Mood.VeryHappy);

        // Assert
        Assert.That(entry.Mood, Is.EqualTo(Mood.VeryHappy));
    }

    [Test]
    public void UpdateMood_ToVerySad_Works()
    {
        // Arrange
        var entry = new JournalEntry { Mood = Mood.Happy };

        // Act
        entry.UpdateMood(Mood.VerySad);

        // Assert
        Assert.That(entry.Mood, Is.EqualTo(Mood.VerySad));
    }

    [Test]
    public void UpdateMood_ToAnxious_Works()
    {
        // Arrange
        var entry = new JournalEntry { Mood = Mood.Calm };

        // Act
        entry.UpdateMood(Mood.Anxious);

        // Assert
        Assert.That(entry.Mood, Is.EqualTo(Mood.Anxious));
    }

    [Test]
    public void UpdateMood_ToCal_Works()
    {
        // Arrange
        var entry = new JournalEntry { Mood = Mood.Anxious };

        // Act
        entry.UpdateMood(Mood.Calm);

        // Assert
        Assert.That(entry.Mood, Is.EqualTo(Mood.Calm));
    }

    [Test]
    public void UpdateMood_ToEnergetic_Works()
    {
        // Arrange
        var entry = new JournalEntry { Mood = Mood.Tired };

        // Act
        entry.UpdateMood(Mood.Energetic);

        // Assert
        Assert.That(entry.Mood, Is.EqualTo(Mood.Energetic));
    }

    [Test]
    public void UpdateMood_ToTired_Works()
    {
        // Arrange
        var entry = new JournalEntry { Mood = Mood.Energetic };

        // Act
        entry.UpdateMood(Mood.Tired);

        // Assert
        Assert.That(entry.Mood, Is.EqualTo(Mood.Tired));
    }

    [Test]
    public void PromptId_CanBeSet()
    {
        // Arrange
        var promptId = Guid.NewGuid();
        var entry = new JournalEntry();

        // Act
        entry.PromptId = promptId;

        // Assert
        Assert.That(entry.PromptId, Is.EqualTo(promptId));
    }

    [Test]
    public void PromptId_CanBeNull()
    {
        // Arrange & Act
        var entry = new JournalEntry
        {
            PromptId = null
        };

        // Assert
        Assert.That(entry.PromptId, Is.Null);
    }

    [Test]
    public void Tags_CanBeSet()
    {
        // Arrange
        var tags = "work,family,health";
        var entry = new JournalEntry();

        // Act
        entry.Tags = tags;

        // Assert
        Assert.That(entry.Tags, Is.EqualTo(tags));
    }

    [Test]
    public void Prompt_NavigationProperty_CanBeSet()
    {
        // Arrange
        var prompt = new Prompt { PromptId = Guid.NewGuid() };
        var entry = new JournalEntry
        {
            PromptId = prompt.PromptId
        };

        // Act
        entry.Prompt = prompt;

        // Assert
        Assert.That(entry.Prompt, Is.EqualTo(prompt));
    }

    [Test]
    public void EntryDate_CanBeSetToCustomDate()
    {
        // Arrange
        var customDate = new DateTime(2024, 6, 15, 10, 30, 0, DateTimeKind.Utc);

        // Act
        var entry = new JournalEntry
        {
            EntryDate = customDate
        };

        // Assert
        Assert.That(entry.EntryDate, Is.EqualTo(customDate));
    }
}
