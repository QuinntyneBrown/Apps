// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core.Tests;

public class FavoriteTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var favorite = new Favorite();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(favorite.Notes, Is.Null);
            Assert.That(favorite.Prompt, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var favoriteId = Guid.NewGuid();
        var promptId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var favorite = new Favorite
        {
            FavoriteId = favoriteId,
            PromptId = promptId,
            UserId = userId,
            Notes = "Love this question for date nights"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(favorite.FavoriteId, Is.EqualTo(favoriteId));
            Assert.That(favorite.PromptId, Is.EqualTo(promptId));
            Assert.That(favorite.UserId, Is.EqualTo(userId));
            Assert.That(favorite.Notes, Is.EqualTo("Love this question for date nights"));
        });
    }

    [Test]
    public void Prompt_CanBeAssigned()
    {
        // Arrange
        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            Text = "What's your biggest dream?"
        };
        var favorite = new Favorite();

        // Act
        favorite.Prompt = prompt;

        // Assert
        Assert.That(favorite.Prompt, Is.EqualTo(prompt));
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var favorite = new Favorite
        {
            Notes = null
        };

        // Assert
        Assert.That(favorite.Notes, Is.Null);
    }
}
