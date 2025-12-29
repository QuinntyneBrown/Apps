namespace FamilyCalendarEventPlanner.Infrastructure.Tests;

public class AvailabilityBlockPersistenceTests
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
    public async Task AddAvailabilityBlock_PersistsToDatabase()
    {
        var block = CreateDefaultBlock();

        _context.AvailabilityBlocks.Add(block);
        await _context.SaveChangesAsync();

        var retrieved = await _context.AvailabilityBlocks.FindAsync(block.BlockId);
        Assert.That(retrieved, Is.Not.Null);
    }

    [Test]
    public async Task AvailabilityBlock_PersistsAllProperties()
    {
        var memberId = Guid.NewGuid();
        var startTime = DateTime.UtcNow.AddHours(1);
        var endTime = DateTime.UtcNow.AddHours(3);
        var block = new AvailabilityBlock(memberId, startTime, endTime, BlockType.OutOfOffice, "Doctor's appointment");

        _context.AvailabilityBlocks.Add(block);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.AvailabilityBlocks.FindAsync(block.BlockId);

        Assert.Multiple(() =>
        {
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.MemberId, Is.EqualTo(memberId));
            Assert.That(retrieved.StartTime, Is.EqualTo(startTime));
            Assert.That(retrieved.EndTime, Is.EqualTo(endTime));
            Assert.That(retrieved.BlockType, Is.EqualTo(BlockType.OutOfOffice));
            Assert.That(retrieved.Reason, Is.EqualTo("Doctor's appointment"));
        });
    }

    [Test]
    public async Task AvailabilityBlock_CanQueryByMemberId()
    {
        var memberId = Guid.NewGuid();

        var block1 = new AvailabilityBlock(memberId, DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2), BlockType.Busy);
        var block2 = new AvailabilityBlock(memberId, DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(4), BlockType.Busy);
        var block3 = new AvailabilityBlock(Guid.NewGuid(), DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(6), BlockType.Busy);

        _context.AvailabilityBlocks.AddRange(block1, block2, block3);
        await _context.SaveChangesAsync();

        var memberBlocks = await _context.AvailabilityBlocks
            .Where(b => b.MemberId == memberId)
            .ToListAsync();

        Assert.That(memberBlocks, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task AvailabilityBlock_CanQueryByTimeRange()
    {
        var memberId = Guid.NewGuid();
        var queryStart = DateTime.UtcNow.AddHours(2);
        var queryEnd = DateTime.UtcNow.AddHours(6);

        var block1 = new AvailabilityBlock(memberId, DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(3), BlockType.Busy);
        var block2 = new AvailabilityBlock(memberId, DateTime.UtcNow.AddHours(4), DateTime.UtcNow.AddHours(5), BlockType.Busy);
        var block3 = new AvailabilityBlock(memberId, DateTime.UtcNow.AddHours(7), DateTime.UtcNow.AddHours(8), BlockType.Busy);

        _context.AvailabilityBlocks.AddRange(block1, block2, block3);
        await _context.SaveChangesAsync();

        var overlappingBlocks = await _context.AvailabilityBlocks
            .Where(b => b.StartTime < queryEnd && b.EndTime > queryStart)
            .ToListAsync();

        Assert.That(overlappingBlocks, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task AvailabilityBlock_PersistsAllBlockTypes()
    {
        var memberId = Guid.NewGuid();

        var busy = new AvailabilityBlock(memberId, DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2), BlockType.Busy);
        var outOfOffice = new AvailabilityBlock(memberId, DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(4), BlockType.OutOfOffice);
        var personal = new AvailabilityBlock(memberId, DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(6), BlockType.Personal);

        _context.AvailabilityBlocks.AddRange(busy, outOfOffice, personal);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();

        var busyRetrieved = await _context.AvailabilityBlocks.FindAsync(busy.BlockId);
        var outOfOfficeRetrieved = await _context.AvailabilityBlocks.FindAsync(outOfOffice.BlockId);
        var personalRetrieved = await _context.AvailabilityBlocks.FindAsync(personal.BlockId);

        Assert.Multiple(() =>
        {
            Assert.That(busyRetrieved!.BlockType, Is.EqualTo(BlockType.Busy));
            Assert.That(outOfOfficeRetrieved!.BlockType, Is.EqualTo(BlockType.OutOfOffice));
            Assert.That(personalRetrieved!.BlockType, Is.EqualTo(BlockType.Personal));
        });
    }

    [Test]
    public async Task AvailabilityBlock_CanDelete()
    {
        var block = CreateDefaultBlock();
        _context.AvailabilityBlocks.Add(block);
        await _context.SaveChangesAsync();

        _context.AvailabilityBlocks.Remove(block);
        await _context.SaveChangesAsync();

        var retrieved = await _context.AvailabilityBlocks.FindAsync(block.BlockId);
        Assert.That(retrieved, Is.Null);
    }

    [Test]
    public async Task AvailabilityBlock_OverlapsWithMethod_WorksCorrectly()
    {
        var block = CreateDefaultBlock();
        _context.AvailabilityBlocks.Add(block);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.AvailabilityBlocks.FindAsync(block.BlockId);

        var overlappingStart = retrieved!.StartTime.AddMinutes(30);
        var overlappingEnd = retrieved.EndTime.AddMinutes(30);

        Assert.That(retrieved.OverlapsWith(overlappingStart, overlappingEnd), Is.True);
    }

    [Test]
    public async Task AvailabilityBlock_EmptyReason_PersistsAsEmptyString()
    {
        var block = new AvailabilityBlock(Guid.NewGuid(), DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2), BlockType.Busy);

        _context.AvailabilityBlocks.Add(block);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.AvailabilityBlocks.FindAsync(block.BlockId);

        Assert.That(retrieved!.Reason, Is.EqualTo(string.Empty));
    }

    private AvailabilityBlock CreateDefaultBlock()
    {
        return new AvailabilityBlock(
            Guid.NewGuid(),
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(2),
            BlockType.Busy);
    }
}
