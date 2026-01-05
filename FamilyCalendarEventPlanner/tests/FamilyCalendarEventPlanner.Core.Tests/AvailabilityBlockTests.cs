using FamilyCalendarEventPlanner.Core.Models.FamilyMemberAggregate;
using FamilyCalendarEventPlanner.Core.Models.FamilyMemberAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Tests;

public class AvailabilityBlockTests
{
    private readonly Guid _memberId = Guid.NewGuid();

    [Test]
    public void Constructor_ValidParameters_CreatesBlock()
    {
        var startTime = DateTime.UtcNow.AddHours(1);
        var endTime = DateTime.UtcNow.AddHours(2);

        var block = new AvailabilityBlock(TestHelpers.DefaultTenantId, _memberId, startTime, endTime, BlockType.Busy);

        Assert.Multiple(() =>
        {
            Assert.That(block.BlockId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(block.MemberId, Is.EqualTo(_memberId));
            Assert.That(block.StartTime, Is.EqualTo(startTime));
            Assert.That(block.EndTime, Is.EqualTo(endTime));
            Assert.That(block.BlockType, Is.EqualTo(BlockType.Busy));
        });
    }

    [Test]
    public void Constructor_WithReason_SetsReason()
    {
        var startTime = DateTime.UtcNow.AddHours(1);
        var endTime = DateTime.UtcNow.AddHours(2);

        var block = new AvailabilityBlock(TestHelpers.DefaultTenantId, _memberId, startTime, endTime, BlockType.OutOfOffice, "Vacation");

        Assert.That(block.Reason, Is.EqualTo("Vacation"));
    }

    [Test]
    public void Constructor_EndTimeBeforeStartTime_ThrowsArgumentException()
    {
        var startTime = DateTime.UtcNow.AddHours(2);
        var endTime = DateTime.UtcNow.AddHours(1);

        Assert.Throws<ArgumentException>(() =>
            new AvailabilityBlock(TestHelpers.DefaultTenantId, _memberId, startTime, endTime, BlockType.Busy));
    }

    [Test]
    public void Constructor_EndTimeEqualsStartTime_ThrowsArgumentException()
    {
        var time = DateTime.UtcNow.AddHours(1);

        Assert.Throws<ArgumentException>(() =>
            new AvailabilityBlock(TestHelpers.DefaultTenantId, _memberId, time, time, BlockType.Busy));
    }

    [Test]
    public void Constructor_NullReason_SetsEmptyString()
    {
        var block = new AvailabilityBlock(
            TestHelpers.DefaultTenantId,
            _memberId,
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(2),
            BlockType.Personal);

        Assert.That(block.Reason, Is.EqualTo(string.Empty));
    }

    [Test]
    public void OverlapsWith_FullyOverlapping_ReturnsTrue()
    {
        var block = new AvailabilityBlock(
            TestHelpers.DefaultTenantId,
            _memberId,
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(3),
            BlockType.Busy);

        var overlaps = block.OverlapsWith(DateTime.UtcNow.AddHours(2), DateTime.UtcNow.AddHours(4));

        Assert.That(overlaps, Is.True);
    }

    [Test]
    public void OverlapsWith_PartiallyOverlappingStart_ReturnsTrue()
    {
        var block = new AvailabilityBlock(
            TestHelpers.DefaultTenantId,
            _memberId,
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(3),
            BlockType.Busy);

        var overlaps = block.OverlapsWith(DateTime.UtcNow, DateTime.UtcNow.AddHours(2));

        Assert.That(overlaps, Is.True);
    }

    [Test]
    public void OverlapsWith_PartiallyOverlappingEnd_ReturnsTrue()
    {
        var block = new AvailabilityBlock(
            TestHelpers.DefaultTenantId,
            _memberId,
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(3),
            BlockType.Busy);

        var overlaps = block.OverlapsWith(DateTime.UtcNow.AddHours(2), DateTime.UtcNow.AddHours(4));

        Assert.That(overlaps, Is.True);
    }

    [Test]
    public void OverlapsWith_ContainedWithinBlock_ReturnsTrue()
    {
        var block = new AvailabilityBlock(
            TestHelpers.DefaultTenantId,
            _memberId,
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(5),
            BlockType.Busy);

        var overlaps = block.OverlapsWith(DateTime.UtcNow.AddHours(2), DateTime.UtcNow.AddHours(4));

        Assert.That(overlaps, Is.True);
    }

    [Test]
    public void OverlapsWith_NoOverlapBefore_ReturnsFalse()
    {
        var block = new AvailabilityBlock(
            TestHelpers.DefaultTenantId,
            _memberId,
            DateTime.UtcNow.AddHours(3),
            DateTime.UtcNow.AddHours(4),
            BlockType.Busy);

        var overlaps = block.OverlapsWith(DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2));

        Assert.That(overlaps, Is.False);
    }

    [Test]
    public void OverlapsWith_NoOverlapAfter_ReturnsFalse()
    {
        var block = new AvailabilityBlock(
            TestHelpers.DefaultTenantId,
            _memberId,
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(2),
            BlockType.Busy);

        var overlaps = block.OverlapsWith(DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(4));

        Assert.That(overlaps, Is.False);
    }

    [Test]
    public void OverlapsWith_TouchingAtEnd_ReturnsFalse()
    {
        var block = new AvailabilityBlock(
            TestHelpers.DefaultTenantId,
            _memberId,
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(2),
            BlockType.Busy);

        var overlaps = block.OverlapsWith(DateTime.UtcNow.AddHours(2), DateTime.UtcNow.AddHours(3));

        Assert.That(overlaps, Is.False);
    }

    [Test]
    public void OverlapsWith_TouchingAtStart_ReturnsFalse()
    {
        var now = DateTime.UtcNow;
        var block = new AvailabilityBlock(
            TestHelpers.DefaultTenantId,
            _memberId,
            now.AddHours(2),
            now.AddHours(3),
            BlockType.Busy);

        var overlaps = block.OverlapsWith(now.AddHours(1), now.AddHours(2));

        Assert.That(overlaps, Is.False);
    }
}
