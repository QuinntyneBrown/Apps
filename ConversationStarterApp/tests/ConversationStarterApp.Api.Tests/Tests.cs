// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConversationStarterApp.Api.Tests;

/// <summary>
/// Tests for Prompt-related commands and queries.
/// </summary>
public class PromptTests
{
    private IConversationStarterAppContext _context = null!;
    private Mock<ILogger<CreatePromptCommandHandler>> _createPromptLogger = null!;
    private Mock<ILogger<GetPromptByIdQueryHandler>> _getPromptLogger = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<Infrastructure.Data.ConversationStarterAppContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new Infrastructure.Data.ConversationStarterAppContext(options);
        _createPromptLogger = new Mock<ILogger<CreatePromptCommandHandler>>();
        _getPromptLogger = new Mock<ILogger<GetPromptByIdQueryHandler>>();
    }

    [TearDown]
    public void TearDown()
    {
        if (_context is Infrastructure.Data.ConversationStarterAppContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }

    [Test]
    public async Task CreatePromptCommand_ShouldCreatePrompt()
    {
        // Arrange
        var handler = new CreatePromptCommandHandler(_context, _createPromptLogger.Object);
        var command = new CreatePromptCommand
        {
            UserId = Guid.NewGuid(),
            Text = "Test prompt",
            Category = Category.Icebreaker,
            Depth = Depth.Surface,
            Tags = "test, sample",
            IsSystemPrompt = false,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Text, Is.EqualTo("Test prompt"));
        Assert.That(result.Category, Is.EqualTo(Category.Icebreaker));
        Assert.That(result.Depth, Is.EqualTo(Depth.Surface));
        Assert.That(result.UsageCount, Is.EqualTo(0));
    }

    [Test]
    public async Task GetPromptByIdQuery_ShouldReturnPrompt()
    {
        // Arrange
        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            Text = "Test prompt",
            Category = Category.Deep,
            Depth = Depth.Moderate,
            IsSystemPrompt = true,
            UsageCount = 5,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Prompts.Add(prompt);
        await _context.SaveChangesAsync();

        var handler = new GetPromptByIdQueryHandler(_context, _getPromptLogger.Object);
        var query = new GetPromptByIdQuery { PromptId = prompt.PromptId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.PromptId, Is.EqualTo(prompt.PromptId));
        Assert.That(result.Text, Is.EqualTo("Test prompt"));
    }

    [Test]
    public async Task GetPromptByIdQuery_ShouldReturnNull_WhenPromptNotFound()
    {
        // Arrange
        var handler = new GetPromptByIdQueryHandler(_context, _getPromptLogger.Object);
        var query = new GetPromptByIdQuery { PromptId = Guid.NewGuid() };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }
}

/// <summary>
/// Tests for Session-related commands and queries.
/// </summary>
public class SessionTests
{
    private IConversationStarterAppContext _context = null!;
    private Mock<ILogger<CreateSessionCommandHandler>> _createSessionLogger = null!;
    private Mock<ILogger<GetSessionByIdQueryHandler>> _getSessionLogger = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<Infrastructure.Data.ConversationStarterAppContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new Infrastructure.Data.ConversationStarterAppContext(options);
        _createSessionLogger = new Mock<ILogger<CreateSessionCommandHandler>>();
        _getSessionLogger = new Mock<ILogger<GetSessionByIdQueryHandler>>();
    }

    [TearDown]
    public void TearDown()
    {
        if (_context is Infrastructure.Data.ConversationStarterAppContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }

    [Test]
    public async Task CreateSessionCommand_ShouldCreateSession()
    {
        // Arrange
        var handler = new CreateSessionCommandHandler(_context, _createSessionLogger.Object);
        var command = new CreateSessionCommand
        {
            UserId = Guid.NewGuid(),
            Title = "Test session",
            Participants = "Me and my friend",
            PromptsUsed = "3 prompts",
            Notes = "Great conversation",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo("Test session"));
        Assert.That(result.Participants, Is.EqualTo("Me and my friend"));
        Assert.That(result.WasSuccessful, Is.True);
    }

    [Test]
    public async Task GetSessionByIdQuery_ShouldReturnSession()
    {
        // Arrange
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test session",
            StartTime = DateTime.UtcNow,
            Participants = "Test participants",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        var handler = new GetSessionByIdQueryHandler(_context, _getSessionLogger.Object);
        var query = new GetSessionByIdQuery { SessionId = session.SessionId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.SessionId, Is.EqualTo(session.SessionId));
        Assert.That(result.Title, Is.EqualTo("Test session"));
    }
}

/// <summary>
/// Tests for Favorite-related commands and queries.
/// </summary>
public class FavoriteTests
{
    private IConversationStarterAppContext _context = null!;
    private Mock<ILogger<CreateFavoriteCommandHandler>> _createFavoriteLogger = null!;
    private Mock<ILogger<GetFavoriteByIdQueryHandler>> _getFavoriteLogger = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<Infrastructure.Data.ConversationStarterAppContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new Infrastructure.Data.ConversationStarterAppContext(options);
        _createFavoriteLogger = new Mock<ILogger<CreateFavoriteCommandHandler>>();
        _getFavoriteLogger = new Mock<ILogger<GetFavoriteByIdQueryHandler>>();
    }

    [TearDown]
    public void TearDown()
    {
        if (_context is Infrastructure.Data.ConversationStarterAppContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }

    [Test]
    public async Task CreateFavoriteCommand_ShouldCreateFavorite()
    {
        // Arrange
        var handler = new CreateFavoriteCommandHandler(_context, _createFavoriteLogger.Object);
        var command = new CreateFavoriteCommand
        {
            UserId = Guid.NewGuid(),
            PromptId = Guid.NewGuid(),
            Notes = "Great prompt",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.PromptId, Is.EqualTo(command.PromptId));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.Notes, Is.EqualTo("Great prompt"));
    }

    [Test]
    public async Task GetFavoriteByIdQuery_ShouldReturnFavorite()
    {
        // Arrange
        var favorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PromptId = Guid.NewGuid(),
            Notes = "Test favorite",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();

        var handler = new GetFavoriteByIdQueryHandler(_context, _getFavoriteLogger.Object);
        var query = new GetFavoriteByIdQuery { FavoriteId = favorite.FavoriteId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.FavoriteId, Is.EqualTo(favorite.FavoriteId));
        Assert.That(result.Notes, Is.EqualTo("Test favorite"));
    }
}
