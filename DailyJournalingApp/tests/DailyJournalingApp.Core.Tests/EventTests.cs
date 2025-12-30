namespace DailyJournalingApp.Core.Tests;

public class EventTests
{
    [Test]
    public void JournalEntryCreatedEvent_CanBeCreated()
    {
        // Arrange
        var entryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "New Entry";
        var mood = Mood.Happy;

        // Act
        var evt = new JournalEntryCreatedEvent
        {
            JournalEntryId = entryId,
            UserId = userId,
            Title = title,
            Mood = mood
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.JournalEntryId, Is.EqualTo(entryId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.Mood, Is.EqualTo(mood));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void JournalEntryMarkedAsFavoriteEvent_CanBeCreated()
    {
        // Arrange
        var entryId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new JournalEntryMarkedAsFavoriteEvent
        {
            JournalEntryId = entryId,
            UserId = userId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.JournalEntryId, Is.EqualTo(entryId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void TagCreatedEvent_CanBeCreated()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Gratitude";

        // Act
        var evt = new TagCreatedEvent
        {
            TagId = tagId,
            UserId = userId,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TagId, Is.EqualTo(tagId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void PromptCreatedEvent_CanBeCreated()
    {
        // Arrange
        var promptId = Guid.NewGuid();
        var text = "What made you smile today?";
        var category = "Gratitude";

        // Act
        var evt = new PromptCreatedEvent
        {
            PromptId = promptId,
            Text = text,
            Category = category
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PromptId, Is.EqualTo(promptId));
            Assert.That(evt.Text, Is.EqualTo(text));
            Assert.That(evt.Category, Is.EqualTo(category));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void PromptCreatedEvent_WithNullCategory_CanBeCreated()
    {
        // Arrange
        var promptId = Guid.NewGuid();
        var text = "Describe your day";

        // Act
        var evt = new PromptCreatedEvent
        {
            PromptId = promptId,
            Text = text,
            Category = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PromptId, Is.EqualTo(promptId));
            Assert.That(evt.Text, Is.EqualTo(text));
            Assert.That(evt.Category, Is.Null);
        });
    }

    [Test]
    public void JournalEntryCreatedEvent_AllMoods_CanBeUsed()
    {
        // Arrange & Act
        var veryHappyEvent = new JournalEntryCreatedEvent { Mood = Mood.VeryHappy };
        var happyEvent = new JournalEntryCreatedEvent { Mood = Mood.Happy };
        var neutralEvent = new JournalEntryCreatedEvent { Mood = Mood.Neutral };
        var sadEvent = new JournalEntryCreatedEvent { Mood = Mood.Sad };
        var verySadEvent = new JournalEntryCreatedEvent { Mood = Mood.VerySad };
        var anxiousEvent = new JournalEntryCreatedEvent { Mood = Mood.Anxious };
        var calmEvent = new JournalEntryCreatedEvent { Mood = Mood.Calm };
        var energeticEvent = new JournalEntryCreatedEvent { Mood = Mood.Energetic };
        var tiredEvent = new JournalEntryCreatedEvent { Mood = Mood.Tired };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(veryHappyEvent.Mood, Is.EqualTo(Mood.VeryHappy));
            Assert.That(happyEvent.Mood, Is.EqualTo(Mood.Happy));
            Assert.That(neutralEvent.Mood, Is.EqualTo(Mood.Neutral));
            Assert.That(sadEvent.Mood, Is.EqualTo(Mood.Sad));
            Assert.That(verySadEvent.Mood, Is.EqualTo(Mood.VerySad));
            Assert.That(anxiousEvent.Mood, Is.EqualTo(Mood.Anxious));
            Assert.That(calmEvent.Mood, Is.EqualTo(Mood.Calm));
            Assert.That(energeticEvent.Mood, Is.EqualTo(Mood.Energetic));
            Assert.That(tiredEvent.Mood, Is.EqualTo(Mood.Tired));
        });
    }

    [Test]
    public void Events_AreRecords()
    {
        // This ensures events are immutable and have value-based equality
        var entryId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var event1 = new JournalEntryMarkedAsFavoriteEvent
        {
            JournalEntryId = entryId,
            UserId = userId,
            Timestamp = new DateTime(2024, 1, 1)
        };

        var event2 = new JournalEntryMarkedAsFavoriteEvent
        {
            JournalEntryId = entryId,
            UserId = userId,
            Timestamp = new DateTime(2024, 1, 1)
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }
}
