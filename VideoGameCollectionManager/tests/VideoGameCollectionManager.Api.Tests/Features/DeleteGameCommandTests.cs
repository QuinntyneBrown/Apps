// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Api.Features.Games;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Tests.Features;

[TestFixture]
public class DeleteGameCommandTests
{
    private IVideoGameCollectionManagerContext _context;
    private DeleteGameCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<VideoGameCollectionManager.Infrastructure.Data.VideoGameCollectionManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new VideoGameCollectionManager.Infrastructure.Data.VideoGameCollectionManagerContext(options);
        _handler = new DeleteGameCommandHandler(_context);
    }

    [TearDown]
    public void TearDown()
    {
        if (_context is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    [Test]
    public async Task Handle_ExistingGame_DeletesAndReturnsTrue()
    {
        // Arrange
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Game",
            Platform = Platform.PC,
            Genre = Genre.Action,
            Status = CompletionStatus.NotStarted
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        var command = new DeleteGameCommand { GameId = game.GameId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);

        var gameInDb = await _context.Games.FindAsync(game.GameId);
        Assert.That(gameInDb, Is.Null);
    }

    [Test]
    public async Task Handle_NonExistingGame_ReturnsFalse()
    {
        // Arrange
        var command = new DeleteGameCommand { GameId = Guid.NewGuid() };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
    }
}
