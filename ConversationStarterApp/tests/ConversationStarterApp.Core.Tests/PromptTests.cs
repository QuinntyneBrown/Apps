// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core.Tests;

public class PromptTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var prompt = new Prompt();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(prompt.Text, Is.EqualTo(string.Empty));
            Assert.That(prompt.UserId, Is.Null);
            Assert.That(prompt.Category, Is.EqualTo(Category.Icebreaker));
            Assert.That(prompt.Depth, Is.EqualTo(Depth.Surface));
            Assert.That(prompt.Tags, Is.Null);
            Assert.That(prompt.IsSystemPrompt, Is.False);
            Assert.That(prompt.UsageCount, Is.EqualTo(0));
            Assert.That(prompt.UpdatedAt, Is.Null);
            Assert.That(prompt.Favorites, Is.Not.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var promptId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var prompt = new Prompt
        {
            PromptId = promptId,
            UserId = userId,
            Text = "What's your favorite childhood memory?",
            Category = Category.Deep,
            Depth = Depth.Moderate,
            Tags = "childhood,memories,personal",
            IsSystemPrompt = false,
            UsageCount = 5
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(prompt.PromptId, Is.EqualTo(promptId));
            Assert.That(prompt.UserId, Is.EqualTo(userId));
            Assert.That(prompt.Text, Is.EqualTo("What's your favorite childhood memory?"));
            Assert.That(prompt.Category, Is.EqualTo(Category.Deep));
            Assert.That(prompt.Depth, Is.EqualTo(Depth.Moderate));
            Assert.That(prompt.Tags, Is.EqualTo("childhood,memories,personal"));
            Assert.That(prompt.IsSystemPrompt, Is.False);
            Assert.That(prompt.UsageCount, Is.EqualTo(5));
        });
    }

    [Test]
    public void IncrementUsageCount_IncrementsCountAndUpdatesTimestamp()
    {
        // Arrange
        var prompt = new Prompt
        {
            UsageCount = 5,
            UpdatedAt = null
        };

        // Act
        prompt.IncrementUsageCount();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(prompt.UsageCount, Is.EqualTo(6));
            Assert.That(prompt.UpdatedAt, Is.Not.Null);
            Assert.That(prompt.UpdatedAt.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IncrementUsageCount_FromZero_IncrementsToOne()
    {
        // Arrange
        var prompt = new Prompt
        {
            UsageCount = 0
        };

        // Act
        prompt.IncrementUsageCount();

        // Assert
        Assert.That(prompt.UsageCount, Is.EqualTo(1));
    }

    [Test]
    public void IncrementUsageCount_MultipleCalls_IncrementsCorrectly()
    {
        // Arrange
        var prompt = new Prompt
        {
            UsageCount = 0
        };

        // Act
        prompt.IncrementUsageCount();
        prompt.IncrementUsageCount();
        prompt.IncrementUsageCount();

        // Assert
        Assert.That(prompt.UsageCount, Is.EqualTo(3));
    }

    [Test]
    public void IsFavoritedByUser_UserHasFavorited_ReturnsTrue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var prompt = new Prompt
        {
            Favorites = new List<Favorite>
            {
                new Favorite { UserId = userId, PromptId = Guid.NewGuid() },
                new Favorite { UserId = Guid.NewGuid(), PromptId = Guid.NewGuid() }
            }
        };

        // Act
        var result = prompt.IsFavoritedByUser(userId);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsFavoritedByUser_UserHasNotFavorited_ReturnsFalse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var prompt = new Prompt
        {
            Favorites = new List<Favorite>
            {
                new Favorite { UserId = Guid.NewGuid(), PromptId = Guid.NewGuid() },
                new Favorite { UserId = Guid.NewGuid(), PromptId = Guid.NewGuid() }
            }
        };

        // Act
        var result = prompt.IsFavoritedByUser(userId);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsFavoritedByUser_NoFavorites_ReturnsFalse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var prompt = new Prompt();

        // Act
        var result = prompt.IsFavoritedByUser(userId);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsSystemPrompt_CanBeSetToTrue()
    {
        // Arrange & Act
        var prompt = new Prompt
        {
            IsSystemPrompt = true,
            UserId = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(prompt.IsSystemPrompt, Is.True);
            Assert.That(prompt.UserId, Is.Null);
        });
    }
}
