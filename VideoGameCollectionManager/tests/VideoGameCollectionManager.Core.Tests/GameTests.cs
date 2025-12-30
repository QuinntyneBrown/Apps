// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VideoGameCollectionManager.Core.Tests;

public class GameTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesGame()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "The Legend of Zelda: Breath of the Wild";
        var platform = Platform.NintendoSwitch;
        var genre = Genre.Adventure;
        var status = CompletionStatus.Completed;
        var publisher = "Nintendo";
        var developer = "Nintendo EPD";
        var releaseDate = new DateTime(2017, 3, 3);
        var purchaseDate = new DateTime(2024, 1, 15);
        var purchasePrice = 59.99m;
        var rating = 10;
        var notes = "Masterpiece";

        // Act
        var game = new Game
        {
            GameId = gameId,
            UserId = userId,
            Title = title,
            Platform = platform,
            Genre = genre,
            Status = status,
            Publisher = publisher,
            Developer = developer,
            ReleaseDate = releaseDate,
            PurchaseDate = purchaseDate,
            PurchasePrice = purchasePrice,
            Rating = rating,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(game.GameId, Is.EqualTo(gameId));
            Assert.That(game.UserId, Is.EqualTo(userId));
            Assert.That(game.Title, Is.EqualTo(title));
            Assert.That(game.Platform, Is.EqualTo(platform));
            Assert.That(game.Genre, Is.EqualTo(genre));
            Assert.That(game.Status, Is.EqualTo(status));
            Assert.That(game.Publisher, Is.EqualTo(publisher));
            Assert.That(game.Developer, Is.EqualTo(developer));
            Assert.That(game.ReleaseDate, Is.EqualTo(releaseDate));
            Assert.That(game.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(game.PurchasePrice, Is.EqualTo(purchasePrice));
            Assert.That(game.Rating, Is.EqualTo(rating));
            Assert.That(game.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Platform_AllValuesCanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Game { Platform = Platform.PlayStation5 }, Throws.Nothing);
            Assert.That(() => new Game { Platform = Platform.PlayStation4 }, Throws.Nothing);
            Assert.That(() => new Game { Platform = Platform.XboxSeriesX }, Throws.Nothing);
            Assert.That(() => new Game { Platform = Platform.XboxOne }, Throws.Nothing);
            Assert.That(() => new Game { Platform = Platform.NintendoSwitch }, Throws.Nothing);
            Assert.That(() => new Game { Platform = Platform.PC }, Throws.Nothing);
            Assert.That(() => new Game { Platform = Platform.Mobile }, Throws.Nothing);
            Assert.That(() => new Game { Platform = Platform.Other }, Throws.Nothing);
        });
    }

    [Test]
    public void Genre_AllValuesCanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Game { Genre = Genre.Action }, Throws.Nothing);
            Assert.That(() => new Game { Genre = Genre.Adventure }, Throws.Nothing);
            Assert.That(() => new Game { Genre = Genre.RPG }, Throws.Nothing);
            Assert.That(() => new Game { Genre = Genre.Strategy }, Throws.Nothing);
            Assert.That(() => new Game { Genre = Genre.Sports }, Throws.Nothing);
            Assert.That(() => new Game { Genre = Genre.Racing }, Throws.Nothing);
            Assert.That(() => new Game { Genre = Genre.Shooter }, Throws.Nothing);
            Assert.That(() => new Game { Genre = Genre.Puzzle }, Throws.Nothing);
            Assert.That(() => new Game { Genre = Genre.Fighting }, Throws.Nothing);
            Assert.That(() => new Game { Genre = Genre.Simulation }, Throws.Nothing);
            Assert.That(() => new Game { Genre = Genre.Other }, Throws.Nothing);
        });
    }

    [Test]
    public void CompletionStatus_AllValuesCanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Game { Status = CompletionStatus.NotStarted }, Throws.Nothing);
            Assert.That(() => new Game { Status = CompletionStatus.InProgress }, Throws.Nothing);
            Assert.That(() => new Game { Status = CompletionStatus.Completed }, Throws.Nothing);
            Assert.That(() => new Game { Status = CompletionStatus.OnHold }, Throws.Nothing);
            Assert.That(() => new Game { Status = CompletionStatus.Abandoned }, Throws.Nothing);
        });
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Game",
            Platform = Platform.PC,
            Genre = Genre.Action,
            Status = CompletionStatus.NotStarted
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(game.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(game.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Game_OptionalFields_CanBeNull()
    {
        // Arrange & Act
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Indie Game",
            Platform = Platform.PC,
            Genre = Genre.Adventure,
            Status = CompletionStatus.NotStarted
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(game.Publisher, Is.Null);
            Assert.That(game.Developer, Is.Null);
            Assert.That(game.ReleaseDate, Is.Null);
            Assert.That(game.PurchaseDate, Is.Null);
            Assert.That(game.PurchasePrice, Is.Null);
            Assert.That(game.Rating, Is.Null);
            Assert.That(game.Notes, Is.Null);
        });
    }

    [Test]
    public void PlaySessions_InitializesAsEmptyList()
    {
        // Arrange & Act
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Action Game",
            Platform = Platform.PlayStation5,
            Genre = Genre.Action,
            Status = CompletionStatus.InProgress
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(game.PlaySessions, Is.Not.Null);
            Assert.That(game.PlaySessions, Is.Empty);
        });
    }

    [Test]
    public void Rating_CanBeSetToMaxValue()
    {
        // Arrange & Act
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Perfect Game",
            Platform = Platform.PC,
            Genre = Genre.RPG,
            Status = CompletionStatus.Completed,
            Rating = 10
        };

        // Assert
        Assert.That(game.Rating, Is.EqualTo(10));
    }

    [Test]
    public void Rating_CanBeSetToMinValue()
    {
        // Arrange & Act
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Bad Game",
            Platform = Platform.Mobile,
            Genre = Genre.Other,
            Status = CompletionStatus.Abandoned,
            Rating = 1
        };

        // Assert
        Assert.That(game.Rating, Is.EqualTo(1));
    }

    [Test]
    public void Game_WithZeroPurchasePrice_IsValid()
    {
        // Arrange & Act
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Free to Play Game",
            Platform = Platform.PC,
            Genre = Genre.Shooter,
            Status = CompletionStatus.InProgress,
            PurchasePrice = 0m
        };

        // Assert
        Assert.That(game.PurchasePrice, Is.EqualTo(0m));
    }

    [Test]
    public void Game_WithLongTitle_StoresCorrectly()
    {
        // Arrange
        var longTitle = "The Elder Scrolls V: Skyrim - Special Edition - Legendary Edition - Game of the Year";

        // Act
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = longTitle,
            Platform = Platform.PC,
            Genre = Genre.RPG,
            Status = CompletionStatus.NotStarted
        };

        // Assert
        Assert.That(game.Title, Is.EqualTo(longTitle));
    }
}
