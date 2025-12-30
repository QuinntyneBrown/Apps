// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Api.Features.PlaySessions;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Tests.Features;

[TestFixture]
public class PlaySessionCommandTests
{
    private IVideoGameCollectionManagerContext _context;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<VideoGameCollectionManager.Infrastructure.Data.VideoGameCollectionManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new VideoGameCollectionManager.Infrastructure.Data.VideoGameCollectionManagerContext(options);
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
    public async Task CreatePlaySession_ValidCommand_CreatesPlaySession()
    {
        // Arrange
        var handler = new CreatePlaySessionCommandHandler(_context);
        var command = new CreatePlaySessionCommand
        {
            UserId = Guid.NewGuid(),
            GameId = Guid.NewGuid(),
            StartTime = DateTime.UtcNow,
            DurationMinutes = 60,
            Notes = "Test session"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.GameId, Is.EqualTo(command.GameId));
        Assert.That(result.DurationMinutes, Is.EqualTo(command.DurationMinutes));
        Assert.That(result.Notes, Is.EqualTo(command.Notes));

        var sessionInDb = await _context.PlaySessions.FindAsync(result.PlaySessionId);
        Assert.That(sessionInDb, Is.Not.Null);
    }

    [Test]
    public async Task UpdatePlaySession_ExistingSession_UpdatesSuccessfully()
    {
        // Arrange
        var playSession = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameId = Guid.NewGuid(),
            StartTime = DateTime.UtcNow,
            DurationMinutes = 30
        };

        _context.PlaySessions.Add(playSession);
        await _context.SaveChangesAsync();

        var handler = new UpdatePlaySessionCommandHandler(_context);
        var command = new UpdatePlaySessionCommand
        {
            PlaySessionId = playSession.PlaySessionId,
            UserId = playSession.UserId,
            GameId = playSession.GameId,
            StartTime = playSession.StartTime,
            DurationMinutes = 120,
            Notes = "Updated notes"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.DurationMinutes, Is.EqualTo(120));
        Assert.That(result.Notes, Is.EqualTo("Updated notes"));
    }

    [Test]
    public async Task DeletePlaySession_ExistingSession_DeletesSuccessfully()
    {
        // Arrange
        var playSession = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameId = Guid.NewGuid(),
            StartTime = DateTime.UtcNow
        };

        _context.PlaySessions.Add(playSession);
        await _context.SaveChangesAsync();

        var handler = new DeletePlaySessionCommandHandler(_context);
        var command = new DeletePlaySessionCommand { PlaySessionId = playSession.PlaySessionId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);

        var sessionInDb = await _context.PlaySessions.FindAsync(playSession.PlaySessionId);
        Assert.That(sessionInDb, Is.Null);
    }
}
