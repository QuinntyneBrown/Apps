namespace FamilyCalendarEventPlanner.Infrastructure.Tests;

public class ScheduleConflictPersistenceTests
{
    private FamilyCalendarEventPlannerContext _context = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FamilyCalendarEventPlannerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FamilyCalendarEventPlannerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task AddScheduleConflict_PersistsToDatabase()
    {
        var conflict = CreateDefaultConflict();

        _context.ScheduleConflicts.Add(conflict);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ScheduleConflicts.FindAsync(conflict.ConflictId);
        Assert.That(retrieved, Is.Not.Null);
    }

    [Test]
    public async Task ScheduleConflict_PersistsAllProperties()
    {
        var eventIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
        var memberIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var conflict = new ScheduleConflict(eventIds, memberIds, ConflictSeverity.High);

        _context.ScheduleConflicts.Add(conflict);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.ScheduleConflicts.FindAsync(conflict.ConflictId);

        Assert.Multiple(() =>
        {
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.ConflictingEventIds, Has.Count.EqualTo(3));
            Assert.That(retrieved.AffectedMemberIds, Has.Count.EqualTo(2));
            Assert.That(retrieved.ConflictSeverity, Is.EqualTo(ConflictSeverity.High));
            Assert.That(retrieved.IsResolved, Is.False);
            Assert.That(retrieved.ResolvedAt, Is.Null);
        });
    }

    [Test]
    public async Task ScheduleConflict_PersistsEventIdsCorrectly()
    {
        var eventId1 = Guid.NewGuid();
        var eventId2 = Guid.NewGuid();
        var eventIds = new List<Guid> { eventId1, eventId2 };
        var memberIds = new List<Guid> { Guid.NewGuid() };
        var conflict = new ScheduleConflict(eventIds, memberIds, ConflictSeverity.Medium);

        _context.ScheduleConflicts.Add(conflict);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.ScheduleConflicts.FindAsync(conflict.ConflictId);

        Assert.Multiple(() =>
        {
            Assert.That(retrieved!.ConflictingEventIds, Contains.Item(eventId1));
            Assert.That(retrieved.ConflictingEventIds, Contains.Item(eventId2));
        });
    }

    [Test]
    public async Task ScheduleConflict_CanResolveAndPersist()
    {
        var conflict = CreateDefaultConflict();
        _context.ScheduleConflicts.Add(conflict);
        await _context.SaveChangesAsync();

        conflict.Resolve();
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.ScheduleConflicts.FindAsync(conflict.ConflictId);

        Assert.Multiple(() =>
        {
            Assert.That(retrieved!.IsResolved, Is.True);
            Assert.That(retrieved.ResolvedAt, Is.Not.Null);
        });
    }

    [Test]
    public async Task ScheduleConflict_CanUpdateSeverityAndPersist()
    {
        var conflict = CreateDefaultConflict();
        _context.ScheduleConflicts.Add(conflict);
        await _context.SaveChangesAsync();

        conflict.UpdateSeverity(ConflictSeverity.Critical);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.ScheduleConflicts.FindAsync(conflict.ConflictId);

        Assert.That(retrieved!.ConflictSeverity, Is.EqualTo(ConflictSeverity.Critical));
    }

    [Test]
    public async Task ScheduleConflict_PersistsAllSeverityLevels()
    {
        var eventIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var memberIds = new List<Guid> { Guid.NewGuid() };

        var low = new ScheduleConflict(eventIds, memberIds, ConflictSeverity.Low);
        var medium = new ScheduleConflict(eventIds, memberIds, ConflictSeverity.Medium);
        var high = new ScheduleConflict(eventIds, memberIds, ConflictSeverity.High);
        var critical = new ScheduleConflict(eventIds, memberIds, ConflictSeverity.Critical);

        _context.ScheduleConflicts.AddRange(low, medium, high, critical);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();

        var lowRetrieved = await _context.ScheduleConflicts.FindAsync(low.ConflictId);
        var mediumRetrieved = await _context.ScheduleConflicts.FindAsync(medium.ConflictId);
        var highRetrieved = await _context.ScheduleConflicts.FindAsync(high.ConflictId);
        var criticalRetrieved = await _context.ScheduleConflicts.FindAsync(critical.ConflictId);

        Assert.Multiple(() =>
        {
            Assert.That(lowRetrieved!.ConflictSeverity, Is.EqualTo(ConflictSeverity.Low));
            Assert.That(mediumRetrieved!.ConflictSeverity, Is.EqualTo(ConflictSeverity.Medium));
            Assert.That(highRetrieved!.ConflictSeverity, Is.EqualTo(ConflictSeverity.High));
            Assert.That(criticalRetrieved!.ConflictSeverity, Is.EqualTo(ConflictSeverity.Critical));
        });
    }

    [Test]
    public async Task ScheduleConflict_CanQueryByResolutionStatus()
    {
        var eventIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var memberIds = new List<Guid> { Guid.NewGuid() };

        var unresolved1 = new ScheduleConflict(eventIds, memberIds, ConflictSeverity.Low);
        var unresolved2 = new ScheduleConflict(eventIds, memberIds, ConflictSeverity.Medium);
        var resolved = new ScheduleConflict(eventIds, memberIds, ConflictSeverity.High);
        resolved.Resolve();

        _context.ScheduleConflicts.AddRange(unresolved1, unresolved2, resolved);
        await _context.SaveChangesAsync();

        var unresolvedConflicts = await _context.ScheduleConflicts
            .Where(c => !c.IsResolved)
            .ToListAsync();

        Assert.That(unresolvedConflicts, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task ScheduleConflict_CanQueryBySeverity()
    {
        var eventIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var memberIds = new List<Guid> { Guid.NewGuid() };

        var low = new ScheduleConflict(eventIds, memberIds, ConflictSeverity.Low);
        var high1 = new ScheduleConflict(eventIds, memberIds, ConflictSeverity.High);
        var high2 = new ScheduleConflict(eventIds, memberIds, ConflictSeverity.High);

        _context.ScheduleConflicts.AddRange(low, high1, high2);
        await _context.SaveChangesAsync();

        var highSeverityConflicts = await _context.ScheduleConflicts
            .Where(c => c.ConflictSeverity == ConflictSeverity.High)
            .ToListAsync();

        Assert.That(highSeverityConflicts, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task ScheduleConflict_CanDelete()
    {
        var conflict = CreateDefaultConflict();
        _context.ScheduleConflicts.Add(conflict);
        await _context.SaveChangesAsync();

        _context.ScheduleConflicts.Remove(conflict);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ScheduleConflicts.FindAsync(conflict.ConflictId);
        Assert.That(retrieved, Is.Null);
    }

    private ScheduleConflict CreateDefaultConflict()
    {
        return new ScheduleConflict(
            new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
            new List<Guid> { Guid.NewGuid() },
            ConflictSeverity.Medium);
    }
}
