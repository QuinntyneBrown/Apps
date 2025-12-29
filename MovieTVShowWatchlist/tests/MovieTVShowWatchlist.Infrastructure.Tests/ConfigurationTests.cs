namespace MovieTVShowWatchlist.Infrastructure.Tests;

[TestFixture]
public class ConfigurationTests
{
    private MovieTVShowWatchlistContext _context = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<MovieTVShowWatchlistContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new MovieTVShowWatchlistContext(options);
        _context.Database.EnsureCreated();
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public void UserConfiguration_ShouldSetPrimaryKey()
    {
        var entityType = _context.Model.FindEntityType(typeof(User));
        var primaryKey = entityType?.FindPrimaryKey();

        Assert.That(primaryKey, Is.Not.Null);
        Assert.That(primaryKey!.Properties.Single().Name, Is.EqualTo("UserId"));
    }

    [Test]
    public void MovieConfiguration_ShouldSetPrimaryKey()
    {
        var entityType = _context.Model.FindEntityType(typeof(Movie));
        var primaryKey = entityType?.FindPrimaryKey();

        Assert.That(primaryKey, Is.Not.Null);
        Assert.That(primaryKey!.Properties.Single().Name, Is.EqualTo("MovieId"));
    }

    [Test]
    public void TVShowConfiguration_ShouldSetPrimaryKey()
    {
        var entityType = _context.Model.FindEntityType(typeof(TVShow));
        var primaryKey = entityType?.FindPrimaryKey();

        Assert.That(primaryKey, Is.Not.Null);
        Assert.That(primaryKey!.Properties.Single().Name, Is.EqualTo("TVShowId"));
    }

    [Test]
    public void WatchlistItemConfiguration_ShouldSetPrimaryKey()
    {
        var entityType = _context.Model.FindEntityType(typeof(WatchlistItem));
        var primaryKey = entityType?.FindPrimaryKey();

        Assert.That(primaryKey, Is.Not.Null);
        Assert.That(primaryKey!.Properties.Single().Name, Is.EqualTo("WatchlistItemId"));
    }

    [Test]
    public void RatingConfiguration_ShouldSetPrimaryKey()
    {
        var entityType = _context.Model.FindEntityType(typeof(Rating));
        var primaryKey = entityType?.FindPrimaryKey();

        Assert.That(primaryKey, Is.Not.Null);
        Assert.That(primaryKey!.Properties.Single().Name, Is.EqualTo("RatingId"));
    }

    [Test]
    public void ReviewConfiguration_ShouldSetPrimaryKey()
    {
        var entityType = _context.Model.FindEntityType(typeof(Review));
        var primaryKey = entityType?.FindPrimaryKey();

        Assert.That(primaryKey, Is.Not.Null);
        Assert.That(primaryKey!.Properties.Single().Name, Is.EqualTo("ReviewId"));
    }

    [Test]
    public void FavoriteConfiguration_ShouldSetPrimaryKey()
    {
        var entityType = _context.Model.FindEntityType(typeof(Favorite));
        var primaryKey = entityType?.FindPrimaryKey();

        Assert.That(primaryKey, Is.Not.Null);
        Assert.That(primaryKey!.Properties.Single().Name, Is.EqualTo("FavoriteId"));
    }

    [Test]
    public void Context_ShouldApplyConfigurations()
    {
        var entityTypes = _context.Model.GetEntityTypes().Select(e => e.ClrType.Name).ToList();

        Assert.That(entityTypes, Does.Contain("User"));
        Assert.That(entityTypes, Does.Contain("Movie"));
        Assert.That(entityTypes, Does.Contain("TVShow"));
        Assert.That(entityTypes, Does.Contain("Episode"));
        Assert.That(entityTypes, Does.Contain("WatchlistItem"));
        Assert.That(entityTypes, Does.Contain("ViewingRecord"));
        Assert.That(entityTypes, Does.Contain("Rating"));
        Assert.That(entityTypes, Does.Contain("Review"));
        Assert.That(entityTypes, Does.Contain("Favorite"));
    }

    [Test]
    public void WatchlistItemConfiguration_ShouldHaveQueryFilter()
    {
        var entityType = _context.Model.FindEntityType(typeof(WatchlistItem));
        var queryFilter = entityType?.GetQueryFilter();

        Assert.That(queryFilter, Is.Not.Null);
    }

    [Test]
    public void ContentTypeProperty_ShouldBeStoredAsString()
    {
        var entityType = _context.Model.FindEntityType(typeof(WatchlistItem));
        var property = entityType?.FindProperty(nameof(WatchlistItem.ContentType));
        var converter = property?.GetValueConverter();

        Assert.That(converter, Is.Not.Null);
    }

    [Test]
    public void PriorityLevelProperty_ShouldBeStoredAsString()
    {
        var entityType = _context.Model.FindEntityType(typeof(WatchlistItem));
        var property = entityType?.FindProperty(nameof(WatchlistItem.PriorityLevel));
        var converter = property?.GetValueConverter();

        Assert.That(converter, Is.Not.Null);
    }

    [Test]
    public void RatingScaleProperty_ShouldBeStoredAsString()
    {
        var entityType = _context.Model.FindEntityType(typeof(Rating));
        var property = entityType?.FindProperty(nameof(Rating.RatingScale));
        var converter = property?.GetValueConverter();

        Assert.That(converter, Is.Not.Null);
    }
}
