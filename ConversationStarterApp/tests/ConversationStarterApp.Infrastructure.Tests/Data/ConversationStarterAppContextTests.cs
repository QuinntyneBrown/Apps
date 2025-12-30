// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Infrastructure.Tests.Data;

/// <summary>
/// Contains unit tests for the <see cref="ConversationStarterAppContext"/> class.
/// </summary>
[TestFixture]
public class ConversationStarterAppContextTests
{
    private DbContextOptions<ConversationStarterAppContext> _options = null!;
    private ConversationStarterAppContext _context = null!;
    private Guid _testUserId;

    /// <summary>
    /// Sets up the test context before each test.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _testUserId = Guid.NewGuid();
        _options = new DbContextOptionsBuilder<ConversationStarterAppContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ConversationStarterAppContext(_options);
    }

    /// <summary>
    /// Cleans up resources after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    #region Prompt Tests

    /// <summary>
    /// Tests that a prompt can be created successfully.
    /// </summary>
    [Test]
    public async Task CreatePrompt_ShouldAddPromptToDatabase()
    {
        // Arrange
        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            UserId = _testUserId,
            Text = "What's your favorite memory?",
            Category = Category.PastExperiences,
            Depth = Depth.Moderate,
            Tags = "memory, nostalgia",
            IsSystemPrompt = false,
            UsageCount = 0,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Prompts.Add(prompt);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Prompts.FindAsync(prompt.PromptId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Text, Is.EqualTo("What's your favorite memory?"));
        Assert.That(retrieved.Category, Is.EqualTo(Category.PastExperiences));
    }

    /// <summary>
    /// Tests that a prompt can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdatePrompt_ShouldModifyExistingPrompt()
    {
        // Arrange
        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            UserId = _testUserId,
            Text = "Original text",
            Category = Category.Icebreaker,
            Depth = Depth.Surface,
            UsageCount = 0,
            CreatedAt = DateTime.UtcNow
        };
        _context.Prompts.Add(prompt);
        await _context.SaveChangesAsync();

        // Act
        prompt.Text = "Updated text";
        prompt.UsageCount = 5;
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Prompts.FindAsync(prompt.PromptId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.Text, Is.EqualTo("Updated text"));
        Assert.That(updated.UsageCount, Is.EqualTo(5));
    }

    /// <summary>
    /// Tests that a prompt can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeletePrompt_ShouldRemovePromptFromDatabase()
    {
        // Arrange
        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            UserId = _testUserId,
            Text = "To Delete",
            Category = Category.Fun,
            Depth = Depth.Surface,
            CreatedAt = DateTime.UtcNow
        };
        _context.Prompts.Add(prompt);
        await _context.SaveChangesAsync();

        // Act
        _context.Prompts.Remove(prompt);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Prompts.FindAsync(prompt.PromptId);
        Assert.That(deleted, Is.Null);
    }

    /// <summary>
    /// Tests that multiple prompts can be queried successfully.
    /// </summary>
    [Test]
    public async Task QueryPrompts_ShouldReturnAllPrompts()
    {
        // Arrange
        var prompts = new List<Prompt>
        {
            new()
            {
                PromptId = Guid.NewGuid(),
                Text = "Prompt 1",
                Category = Category.Icebreaker,
                Depth = Depth.Surface,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                PromptId = Guid.NewGuid(),
                Text = "Prompt 2",
                Category = Category.Deep,
                Depth = Depth.Deep,
                CreatedAt = DateTime.UtcNow
            }
        };
        _context.Prompts.AddRange(prompts);
        await _context.SaveChangesAsync();

        // Act
        var retrieved = await _context.Prompts.ToListAsync();

        // Assert
        Assert.That(retrieved, Has.Count.EqualTo(2));
    }

    #endregion

    #region Favorite Tests

    /// <summary>
    /// Tests that a favorite can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateFavorite_ShouldAddFavoriteToDatabase()
    {
        // Arrange
        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            Text = "Test Prompt",
            Category = Category.Fun,
            Depth = Depth.Surface,
            CreatedAt = DateTime.UtcNow
        };
        _context.Prompts.Add(prompt);
        await _context.SaveChangesAsync();

        var favorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            PromptId = prompt.PromptId,
            UserId = _testUserId,
            Notes = "Great conversation starter",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Favorites.FindAsync(favorite.FavoriteId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.PromptId, Is.EqualTo(prompt.PromptId));
        Assert.That(retrieved.Notes, Is.EqualTo("Great conversation starter"));
    }

    /// <summary>
    /// Tests that a favorite can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateFavorite_ShouldModifyExistingFavorite()
    {
        // Arrange
        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            Text = "Test Prompt",
            Category = Category.Fun,
            Depth = Depth.Surface,
            CreatedAt = DateTime.UtcNow
        };
        _context.Prompts.Add(prompt);
        await _context.SaveChangesAsync();

        var favorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            PromptId = prompt.PromptId,
            UserId = _testUserId,
            Notes = "Original notes",
            CreatedAt = DateTime.UtcNow
        };
        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();

        // Act
        favorite.Notes = "Updated notes";
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Favorites.FindAsync(favorite.FavoriteId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.Notes, Is.EqualTo("Updated notes"));
    }

    /// <summary>
    /// Tests that a favorite can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteFavorite_ShouldRemoveFavoriteFromDatabase()
    {
        // Arrange
        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            Text = "Test Prompt",
            Category = Category.Fun,
            Depth = Depth.Surface,
            CreatedAt = DateTime.UtcNow
        };
        _context.Prompts.Add(prompt);
        await _context.SaveChangesAsync();

        var favorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            PromptId = prompt.PromptId,
            UserId = _testUserId,
            CreatedAt = DateTime.UtcNow
        };
        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();

        // Act
        _context.Favorites.Remove(favorite);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Favorites.FindAsync(favorite.FavoriteId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Session Tests

    /// <summary>
    /// Tests that a session can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateSession_ShouldAddSessionToDatabase()
    {
        // Arrange
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = _testUserId,
            Title = "Evening Conversation",
            StartTime = DateTime.UtcNow,
            Participants = "Me and friend",
            WasSuccessful = true,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Sessions.FindAsync(session.SessionId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Title, Is.EqualTo("Evening Conversation"));
        Assert.That(retrieved.WasSuccessful, Is.True);
    }

    /// <summary>
    /// Tests that a session can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateSession_ShouldModifyExistingSession()
    {
        // Arrange
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = _testUserId,
            Title = "Original Title",
            StartTime = DateTime.UtcNow,
            EndTime = null,
            CreatedAt = DateTime.UtcNow
        };
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        // Act
        session.EndTime = DateTime.UtcNow.AddHours(1);
        session.Notes = "Great conversation";
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Sessions.FindAsync(session.SessionId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.EndTime, Is.Not.Null);
        Assert.That(updated.Notes, Is.EqualTo("Great conversation"));
    }

    /// <summary>
    /// Tests that a session can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteSession_ShouldRemoveSessionFromDatabase()
    {
        // Arrange
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = _testUserId,
            Title = "To Delete",
            StartTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        // Act
        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Sessions.FindAsync(session.SessionId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Relationship Tests

    /// <summary>
    /// Tests that relationships between prompts and favorites work correctly.
    /// </summary>
    [Test]
    public async Task PromptFavoriteRelationship_ShouldLoadCorrectly()
    {
        // Arrange
        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            Text = "Test Prompt",
            Category = Category.Fun,
            Depth = Depth.Surface,
            CreatedAt = DateTime.UtcNow
        };
        _context.Prompts.Add(prompt);
        await _context.SaveChangesAsync();

        var favorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            PromptId = prompt.PromptId,
            UserId = _testUserId,
            CreatedAt = DateTime.UtcNow
        };
        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();

        // Act
        var loadedPrompt = await _context.Prompts
            .Include(p => p.Favorites)
            .FirstOrDefaultAsync(p => p.PromptId == prompt.PromptId);

        // Assert
        Assert.That(loadedPrompt, Is.Not.Null);
        Assert.That(loadedPrompt.Favorites, Has.Count.EqualTo(1));
        Assert.That(loadedPrompt.Favorites.First().UserId, Is.EqualTo(_testUserId));
    }

    #endregion
}
