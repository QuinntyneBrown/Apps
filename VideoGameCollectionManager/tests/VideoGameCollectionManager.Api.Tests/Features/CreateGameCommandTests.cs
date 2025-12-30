// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Api.Features.Games;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Tests.Features;

[TestFixture]
public class CreateGameCommandTests
{
    private IVideoGameCollectionManagerContext _context;
    private CreateGameCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<VideoGameCollectionManager.Infrastructure.Data.VideoGameCollectionManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new VideoGameCollectionManager.Infrastructure.Data.VideoGameCollectionManagerContext(options);
        _handler = new CreateGameCommandHandler(_context);
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
    public async Task Handle_ValidCommand_CreatesGame()
    {
        // Arrange
        var command = new CreateGameCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Test Game",
            Platform = Platform.PC,
            Genre = Genre.Action,
            Status = CompletionStatus.NotStarted,
            Publisher = "Test Publisher",
            Developer = "Test Developer",
            Rating = 5,
            Notes = "Test notes"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(command.Title));
        Assert.That(result.Platform, Is.EqualTo(command.Platform));
        Assert.That(result.Genre, Is.EqualTo(command.Genre));
        Assert.That(result.Status, Is.EqualTo(command.Status));
        Assert.That(result.Publisher, Is.EqualTo(command.Publisher));
        Assert.That(result.Developer, Is.EqualTo(command.Developer));
        Assert.That(result.Rating, Is.EqualTo(command.Rating));
        Assert.That(result.Notes, Is.EqualTo(command.Notes));

        var gameInDb = await _context.Games.FindAsync(result.GameId);
        Assert.That(gameInDb, Is.Not.Null);
        Assert.That(gameInDb!.Title, Is.EqualTo(command.Title));
    }
}
