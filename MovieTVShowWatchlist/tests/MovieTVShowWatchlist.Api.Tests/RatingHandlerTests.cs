namespace MovieTVShowWatchlist.Api.Tests;

[TestFixture]
public class RatingHandlerTests
{
    private MovieTVShowWatchlistContext _context = null!;
    private Mock<ILogger<CreateRating.Handler>> _createLogger = null!;
    private Mock<ILogger<GetRatings.Handler>> _getLogger = null!;
    private User _testUser = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<MovieTVShowWatchlistContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new MovieTVShowWatchlistContext(options);

        _createLogger = new Mock<ILogger<CreateRating.Handler>>();
        _getLogger = new Mock<ILogger<GetRatings.Handler>>();

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
    public async Task CreateRating_ShouldCreateRating()
    {
        var handler = new CreateRating.Handler(_context, _createLogger.Object);
        var command = new CreateRating.Command(
            _testUser.UserId,
            Guid.NewGuid(),
            "Movie",
            8.5m,
            "TenPoint",
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(-1),
            false,
            "Happy"
        );

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.RatingValue, Is.EqualTo(8.5m));
        Assert.That(result.RatingScale, Is.EqualTo("TenPoint"));
    }

    [Test]
    public void CreateRating_ShouldThrowForInvalidContentType()
    {
        var handler = new CreateRating.Handler(_context, _createLogger.Object);
        var command = new CreateRating.Command(
            _testUser.UserId,
            Guid.NewGuid(),
            "InvalidType",
            8.5m,
            "TenPoint",
            DateTime.UtcNow,
            null,
            false,
            null
        );

        Assert.ThrowsAsync<ArgumentException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void CreateRating_ShouldThrowForInvalidScale()
    {
        var handler = new CreateRating.Handler(_context, _createLogger.Object);
        var command = new CreateRating.Command(
            _testUser.UserId,
            Guid.NewGuid(),
            "Movie",
            8.5m,
            "InvalidScale",
            DateTime.UtcNow,
            null,
            false,
            null
        );

        Assert.ThrowsAsync<ArgumentException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task GetRatings_ShouldReturnRatings()
    {
        var rating = new Rating
        {
            RatingId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            RatingValue = 8.5m,
            RatingScale = RatingScale.TenPoint,
            RatingDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();

        var handler = new GetRatings.Handler(_context, _getLogger.Object);
        var query = new GetRatings.Query(_testUser.UserId, null, null, null, null, 1, 20);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result.Items, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task GetRatings_ShouldFilterByContentType()
    {
        var movieRating = new Rating
        {
            RatingId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            RatingValue = 8.5m,
            RatingScale = RatingScale.TenPoint,
            RatingDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var showRating = new Rating
        {
            RatingId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.TVShow,
            RatingValue = 9.0m,
            RatingScale = RatingScale.TenPoint,
            RatingDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Ratings.AddRange(movieRating, showRating);
        await _context.SaveChangesAsync();

        var handler = new GetRatings.Handler(_context, _getLogger.Object);
        var query = new GetRatings.Query(_testUser.UserId, "Movie", null, null, null, 1, 20);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].ContentType, Is.EqualTo("Movie"));
    }

    [Test]
    public async Task GetRatings_ShouldFilterByMinRating()
    {
        var lowRating = new Rating
        {
            RatingId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            RatingValue = 5.0m,
            RatingScale = RatingScale.TenPoint,
            RatingDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var highRating = new Rating
        {
            RatingId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            RatingValue = 9.0m,
            RatingScale = RatingScale.TenPoint,
            RatingDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Ratings.AddRange(lowRating, highRating);
        await _context.SaveChangesAsync();

        var handler = new GetRatings.Handler(_context, _getLogger.Object);
        var query = new GetRatings.Query(_testUser.UserId, null, 7.0m, null, null, 1, 20);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].RatingValue, Is.EqualTo(9.0m));
    }
}
