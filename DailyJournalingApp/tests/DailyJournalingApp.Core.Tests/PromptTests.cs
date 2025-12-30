namespace DailyJournalingApp.Core.Tests;

public class PromptTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesPrompt()
    {
        // Arrange
        var promptId = Guid.NewGuid();
        var text = "What are you grateful for today?";
        var category = "Gratitude";
        var isSystemPrompt = true;
        var createdByUserId = Guid.NewGuid();

        // Act
        var prompt = new Prompt
        {
            PromptId = promptId,
            Text = text,
            Category = category,
            IsSystemPrompt = isSystemPrompt,
            CreatedByUserId = createdByUserId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(prompt.PromptId, Is.EqualTo(promptId));
            Assert.That(prompt.Text, Is.EqualTo(text));
            Assert.That(prompt.Category, Is.EqualTo(category));
            Assert.That(prompt.IsSystemPrompt, Is.EqualTo(isSystemPrompt));
            Assert.That(prompt.CreatedByUserId, Is.EqualTo(createdByUserId));
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var prompt = new Prompt();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(prompt.Text, Is.EqualTo(string.Empty));
            Assert.That(prompt.IsSystemPrompt, Is.True);
            Assert.That(prompt.Category, Is.Null);
            Assert.That(prompt.CreatedByUserId, Is.Null);
            Assert.That(prompt.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(prompt.JournalEntries, Is.Not.Null.And.Empty);
        });
    }

    [Test]
    public void IsSystemPrompt_CanBeSetToFalse()
    {
        // Arrange & Act
        var prompt = new Prompt
        {
            IsSystemPrompt = false
        };

        // Assert
        Assert.That(prompt.IsSystemPrompt, Is.False);
    }

    [Test]
    public void IsSystemPrompt_DefaultsToTrue()
    {
        // Arrange & Act
        var prompt = new Prompt();

        // Assert
        Assert.That(prompt.IsSystemPrompt, Is.True);
    }

    [Test]
    public void Text_CanBeSet()
    {
        // Arrange
        var text = "Describe your perfect day.";
        var prompt = new Prompt();

        // Act
        prompt.Text = text;

        // Assert
        Assert.That(prompt.Text, Is.EqualTo(text));
    }

    [Test]
    public void Category_CanBeSet()
    {
        // Arrange
        var category = "Self-Reflection";
        var prompt = new Prompt();

        // Act
        prompt.Category = category;

        // Assert
        Assert.That(prompt.Category, Is.EqualTo(category));
    }

    [Test]
    public void Category_CanBeNull()
    {
        // Arrange & Act
        var prompt = new Prompt
        {
            Category = null
        };

        // Assert
        Assert.That(prompt.Category, Is.Null);
    }

    [Test]
    public void CreatedByUserId_CanBeSet()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var prompt = new Prompt();

        // Act
        prompt.CreatedByUserId = userId;

        // Assert
        Assert.That(prompt.CreatedByUserId, Is.EqualTo(userId));
    }

    [Test]
    public void CreatedByUserId_CanBeNull()
    {
        // Arrange & Act
        var prompt = new Prompt
        {
            CreatedByUserId = null
        };

        // Assert
        Assert.That(prompt.CreatedByUserId, Is.Null);
    }

    [Test]
    public void JournalEntries_CanBePopulated()
    {
        // Arrange
        var prompt = new Prompt();
        var entry1 = new JournalEntry { JournalEntryId = Guid.NewGuid(), PromptId = prompt.PromptId };
        var entry2 = new JournalEntry { JournalEntryId = Guid.NewGuid(), PromptId = prompt.PromptId };

        // Act
        prompt.JournalEntries.Add(entry1);
        prompt.JournalEntries.Add(entry2);

        // Assert
        Assert.That(prompt.JournalEntries, Has.Count.EqualTo(2));
    }

    [Test]
    public void CreatedAt_IsSetOnCreation()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var prompt = new Prompt();

        // Assert
        Assert.That(prompt.CreatedAt, Is.GreaterThan(beforeCreation));
    }

    [Test]
    public void UserCreatedPrompt_HasCorrectProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var prompt = new Prompt
        {
            Text = "Custom user prompt",
            IsSystemPrompt = false,
            CreatedByUserId = userId,
            Category = "Personal"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(prompt.IsSystemPrompt, Is.False);
            Assert.That(prompt.CreatedByUserId, Is.EqualTo(userId));
            Assert.That(prompt.Category, Is.EqualTo("Personal"));
        });
    }

    [Test]
    public void SystemPrompt_HasCorrectProperties()
    {
        // Arrange & Act
        var prompt = new Prompt
        {
            Text = "System provided prompt",
            IsSystemPrompt = true,
            CreatedByUserId = null,
            Category = "Daily"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(prompt.IsSystemPrompt, Is.True);
            Assert.That(prompt.CreatedByUserId, Is.Null);
        });
    }
}
