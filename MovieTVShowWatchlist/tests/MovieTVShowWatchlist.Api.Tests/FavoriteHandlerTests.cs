namespace MovieTVShowWatchlist.Api.Tests;

[TestFixture]
public class FavoriteHandlerTests
{
    private MovieTVShowWatchlistContext _context = null!;
    private Mock<ILogger<CreateFavorite.Handler>> _createLogger = null!;
    private Mock<ILogger<GetFavorites.Handler>> _getLogger = null!;
    private Mock<ILogger<DeleteFavorite.Handler>> _deleteLogger = null!;
    private User _testUser = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<MovieTVShowWatchlistContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new MovieTVShowWatchlistContext(options);

        _createLogger = new Mock<ILogger<CreateFavorite.Handler>>();
        _getLogger = new Mock<ILogger<GetFavorites.Handler>>();
        _deleteLogger = new Mock<ILogger<DeleteFavorite.Handler>>();

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
    public async Task CreateFavorite_ShouldCreateFavorite()
    {
        var handler = new CreateFavorite.Handler(_context, _createLogger.Object);
        var command = new CreateFavorite.Command(
            _testUser.UserId,
            Guid.NewGuid(),
            "Movie",
            "All-Time Best",
            "First movie I watched"
        );

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.FavoriteCategory, Is.EqualTo("All-Time Best"));
    }

    [Test]
    public async Task CreateFavorite_ShouldThrowWhenDuplicate()
    {
        var contentId = Guid.NewGuid();
        var handler = new CreateFavorite.Handler(_context, _createLogger.Object);
        var command = new CreateFavorite.Command(
            _testUser.UserId,
            contentId,
            "Movie",
            null,
            null
        );

        await handler.Handle(command, CancellationToken.None);

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task GetFavorites_ShouldReturnFavorites()
    {
        var favorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            AddedDate = DateTime.UtcNow,
            FavoriteCategory = "Comfort Watch",
            RewatchCount = 5,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();

        var handler = new GetFavorites.Handler(_context, _getLogger.Object);
        var query = new GetFavorites.Query(_testUser.UserId, null, null, null, 1, 20);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].RewatchCount, Is.EqualTo(5));
    }

    [Test]
    public async Task GetFavorites_ShouldFilterByCategory()
    {
        var comfortFavorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            AddedDate = DateTime.UtcNow,
            FavoriteCategory = "Comfort Watch",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var bestFavorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            AddedDate = DateTime.UtcNow,
            FavoriteCategory = "All-Time Best",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Favorites.AddRange(comfortFavorite, bestFavorite);
        await _context.SaveChangesAsync();

        var handler = new GetFavorites.Handler(_context, _getLogger.Object);
        var query = new GetFavorites.Query(_testUser.UserId, null, "Comfort Watch", null, 1, 20);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].FavoriteCategory, Is.EqualTo("Comfort Watch"));
    }

    [Test]
    public async Task DeleteFavorite_ShouldDeleteFavorite()
    {
        var favorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            AddedDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();

        var handler = new DeleteFavorite.Handler(_context, _deleteLogger.Object);
        var command = new DeleteFavorite.Command(_testUser.UserId, favorite.FavoriteId);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.True);

        var deletedFavorite = await _context.Favorites.FindAsync(favorite.FavoriteId);
        Assert.That(deletedFavorite, Is.Null);
    }

    [Test]
    public async Task DeleteFavorite_ShouldReturnFalseForNonExistent()
    {
        var handler = new DeleteFavorite.Handler(_context, _deleteLogger.Object);
        var command = new DeleteFavorite.Command(_testUser.UserId, Guid.NewGuid());

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.False);
    }
}
