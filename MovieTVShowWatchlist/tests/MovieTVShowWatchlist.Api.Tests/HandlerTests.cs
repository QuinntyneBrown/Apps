namespace MovieTVShowWatchlist.Api.Tests;

[TestFixture]
public class HandlerTests
{
    private MovieTVShowWatchlistContext _context = null!;
    private Mock<ILogger<AddMovieToWatchlist.Handler>> _addMovieLogger = null!;
    private Mock<ILogger<AddTVShowToWatchlist.Handler>> _addTVShowLogger = null!;
    private Mock<ILogger<GetWatchlist.Handler>> _getWatchlistLogger = null!;
    private Mock<ILogger<RemoveFromWatchlist.Handler>> _removeLogger = null!;
    private User _testUser = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<MovieTVShowWatchlistContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new MovieTVShowWatchlistContext(options);

        _addMovieLogger = new Mock<ILogger<AddMovieToWatchlist.Handler>>();
        _addTVShowLogger = new Mock<ILogger<AddTVShowToWatchlist.Handler>>();
        _getWatchlistLogger = new Mock<ILogger<GetWatchlist.Handler>>();
        _removeLogger = new Mock<ILogger<RemoveFromWatchlist.Handler>>();

        _testUser = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.Users.Add(_testUser);
        _context.SaveChanges();
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task AddMovieToWatchlist_ShouldAddMovie()
    {
        var handler = new AddMovieToWatchlist.Handler(_context, _addMovieLogger.Object);
        var command = new AddMovieToWatchlist.Command(
            _testUser.UserId,
            Guid.NewGuid(),
            "Test Movie",
            2024,
            new List<string> { "Action" },
            "Test Director",
            120,
            "High",
            "Friend",
            "Netflix"
        );

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo("Test Movie"));
        Assert.That(result.ContentType, Is.EqualTo("Movie"));
    }

    [Test]
    public async Task AddMovieToWatchlist_ShouldThrowWhenDuplicate()
    {
        var handler = new AddMovieToWatchlist.Handler(_context, _addMovieLogger.Object);
        var movieId = Guid.NewGuid();
        var command = new AddMovieToWatchlist.Command(
            _testUser.UserId,
            movieId,
            "Test Movie",
            2024,
            null,
            null,
            null,
            null,
            null,
            null
        );

        await handler.Handle(command, CancellationToken.None);

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task AddTVShowToWatchlist_ShouldAddShow()
    {
        var handler = new AddTVShowToWatchlist.Handler(_context, _addTVShowLogger.Object);
        var command = new AddTVShowToWatchlist.Command(
            _testUser.UserId,
            Guid.NewGuid(),
            "Test Show",
            2020,
            new List<string> { "Drama" },
            5,
            "Ongoing",
            "MustWatch",
            "HBO Max"
        );

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo("Test Show"));
        Assert.That(result.ContentType, Is.EqualTo("TVShow"));
    }

    [Test]
    public async Task GetWatchlist_ShouldReturnItems()
    {
        var item = new WatchlistItem
        {
            WatchlistItemId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            Title = "Test Movie",
            AddedDate = DateTime.UtcNow,
            PriorityRank = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.WatchlistItems.Add(item);
        await _context.SaveChangesAsync();

        var handler = new GetWatchlist.Handler(_context, _getWatchlistLogger.Object);
        var query = new GetWatchlist.Query(_testUser.UserId, null, null, null, null, 1, 20);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.TotalCount, Is.EqualTo(1));
    }

    [Test]
    public async Task GetWatchlist_ShouldFilterByPriority()
    {
        var highPriorityItem = new WatchlistItem
        {
            WatchlistItemId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            Title = "High Priority Movie",
            AddedDate = DateTime.UtcNow,
            PriorityLevel = PriorityLevel.High,
            PriorityRank = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var lowPriorityItem = new WatchlistItem
        {
            WatchlistItemId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            Title = "Low Priority Movie",
            AddedDate = DateTime.UtcNow,
            PriorityLevel = PriorityLevel.Low,
            PriorityRank = 2,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.WatchlistItems.AddRange(highPriorityItem, lowPriorityItem);
        await _context.SaveChangesAsync();

        var handler = new GetWatchlist.Handler(_context, _getWatchlistLogger.Object);
        var query = new GetWatchlist.Query(_testUser.UserId, null, null, "High", null, 1, 20);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].Title, Is.EqualTo("High Priority Movie"));
    }

    [Test]
    public async Task RemoveFromWatchlist_ShouldSoftDeleteItem()
    {
        var item = new WatchlistItem
        {
            WatchlistItemId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            Title = "Test Movie",
            AddedDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.WatchlistItems.Add(item);
        await _context.SaveChangesAsync();

        var handler = new RemoveFromWatchlist.Handler(_context, _removeLogger.Object);
        var command = new RemoveFromWatchlist.Command(_testUser.UserId, item.WatchlistItemId, "Watched", null);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.True);

        var deletedItem = await _context.WatchlistItems
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(w => w.WatchlistItemId == item.WatchlistItemId);

        Assert.That(deletedItem, Is.Not.Null);
        Assert.That(deletedItem!.IsDeleted, Is.True);
        Assert.That(deletedItem.RemovalReason, Is.EqualTo(RemovalReason.Watched));
    }

    [Test]
    public async Task RemoveFromWatchlist_ShouldReturnFalseForNonExistent()
    {
        var handler = new RemoveFromWatchlist.Handler(_context, _removeLogger.Object);
        var command = new RemoveFromWatchlist.Command(_testUser.UserId, Guid.NewGuid(), null, null);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.False);
    }
}
