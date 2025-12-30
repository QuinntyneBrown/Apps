// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Api.Features.Games;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Tests.Features;

[TestFixture]
public class GetAllGamesQueryTests
{
    private IVideoGameCollectionManagerContext _context;
    private GetAllGamesQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<VideoGameCollectionManager.Infrastructure.Data.VideoGameCollectionManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new VideoGameCollectionManager.Infrastructure.Data.VideoGameCollectionManagerContext(options);
        _handler = new GetAllGamesQueryHandler(_context);
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
    public async Task Handle_ReturnsAllGames()
    {
        // Arrange
        var game1 = new Game { GameId = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Game 1", Platform = Platform.PC, Genre = Genre.Action, Status = CompletionStatus.InProgress };
        var game2 = new Game { GameId = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Game 2", Platform = Platform.PlayStation5, Genre = Genre.RPG, Status = CompletionStatus.Completed };

        _context.Games.Add(game1);
        _context.Games.Add(game2);
        await _context.SaveChangesAsync();

        var query = new GetAllGamesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result.Any(g => g.GameId == game1.GameId), Is.True);
        Assert.That(result.Any(g => g.GameId == game2.GameId), Is.True);
    }

    [Test]
    public async Task Handle_EmptyDatabase_ReturnsEmptyList()
    {
        // Arrange
        var query = new GetAllGamesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }
}
