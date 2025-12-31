using FamilyCalendarEventPlanner.Core.Model.ConflictAggregate;
using FamilyCalendarEventPlanner.Core.Model.ConflictAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Tests;

public class ScheduleConflictTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesConflict()
    {
        var eventIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var memberIds = new List<Guid> { Guid.NewGuid() };

        var conflict = new ScheduleConflict(TestHelpers.DefaultTenantId, eventIds, memberIds, ConflictSeverity.Medium);

        Assert.Multiple(() =>
        {
            Assert.That(conflict.ConflictId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(conflict.ConflictingEventIds, Has.Count.EqualTo(2));
            Assert.That(conflict.AffectedMemberIds, Has.Count.EqualTo(1));
            Assert.That(conflict.ConflictSeverity, Is.EqualTo(ConflictSeverity.Medium));
            Assert.That(conflict.IsResolved, Is.False);
            Assert.That(conflict.ResolvedAt, Is.Null);
        });
    }

    [Test]
    public void Constructor_SingleEventId_ThrowsArgumentException()
    {
        var eventIds = new List<Guid> { Guid.NewGuid() };
        var memberIds = new List<Guid> { Guid.NewGuid() };

        Assert.Throws<ArgumentException>(() =>
            new ScheduleConflict(TestHelpers.DefaultTenantId, eventIds, memberIds, ConflictSeverity.Low));
    }

    [Test]
    public void Constructor_EmptyEventIds_ThrowsArgumentException()
    {
        var eventIds = new List<Guid>();
        var memberIds = new List<Guid> { Guid.NewGuid() };

        Assert.Throws<ArgumentException>(() =>
            new ScheduleConflict(TestHelpers.DefaultTenantId, eventIds, memberIds, ConflictSeverity.Low));
    }

    [Test]
    public void Constructor_EmptyMemberIds_ThrowsArgumentException()
    {
        var eventIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var memberIds = new List<Guid>();

        Assert.Throws<ArgumentException>(() =>
            new ScheduleConflict(TestHelpers.DefaultTenantId, eventIds, memberIds, ConflictSeverity.Low));
    }

    [Test]
    public void Constructor_LowSeverity_SetsCorrectSeverity()
    {
        var conflict = CreateDefaultConflict(ConflictSeverity.Low);

        Assert.That(conflict.ConflictSeverity, Is.EqualTo(ConflictSeverity.Low));
    }

    [Test]
    public void Constructor_HighSeverity_SetsCorrectSeverity()
    {
        var conflict = CreateDefaultConflict(ConflictSeverity.High);

        Assert.That(conflict.ConflictSeverity, Is.EqualTo(ConflictSeverity.High));
    }

    [Test]
    public void Constructor_CriticalSeverity_SetsCorrectSeverity()
    {
        var conflict = CreateDefaultConflict(ConflictSeverity.Critical);

        Assert.That(conflict.ConflictSeverity, Is.EqualTo(ConflictSeverity.Critical));
    }

    [Test]
    public void Resolve_UnresolvedConflict_SetsIsResolvedAndResolvedAt()
    {
        var conflict = CreateDefaultConflict();
        var beforeResolve = DateTime.UtcNow;

        conflict.Resolve();

        Assert.Multiple(() =>
        {
            Assert.That(conflict.IsResolved, Is.True);
            Assert.That(conflict.ResolvedAt, Is.Not.Null);
            Assert.That(conflict.ResolvedAt, Is.GreaterThanOrEqualTo(beforeResolve));
        });
    }

    [Test]
    public void Resolve_AlreadyResolvedConflict_ThrowsInvalidOperationException()
    {
        var conflict = CreateDefaultConflict();
        conflict.Resolve();

        Assert.Throws<InvalidOperationException>(() => conflict.Resolve());
    }

    [Test]
    public void UpdateSeverity_UnresolvedConflict_UpdatesSeverity()
    {
        var conflict = CreateDefaultConflict(ConflictSeverity.Low);

        conflict.UpdateSeverity(ConflictSeverity.Critical);

        Assert.That(conflict.ConflictSeverity, Is.EqualTo(ConflictSeverity.Critical));
    }

    [Test]
    public void UpdateSeverity_ResolvedConflict_ThrowsInvalidOperationException()
    {
        var conflict = CreateDefaultConflict();
        conflict.Resolve();

        Assert.Throws<InvalidOperationException>(() =>
            conflict.UpdateSeverity(ConflictSeverity.High));
    }

    [Test]
    public void Constructor_MultipleEventIds_StoresAllEventIds()
    {
        var eventIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
        var memberIds = new List<Guid> { Guid.NewGuid() };

        var conflict = new ScheduleConflict(TestHelpers.DefaultTenantId, eventIds, memberIds, ConflictSeverity.High);

        Assert.That(conflict.ConflictingEventIds, Has.Count.EqualTo(3));
    }

    [Test]
    public void Constructor_MultipleMemberIds_StoresAllMemberIds()
    {
        var eventIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var memberIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

        var conflict = new ScheduleConflict(TestHelpers.DefaultTenantId, eventIds, memberIds, ConflictSeverity.Medium);

        Assert.That(conflict.AffectedMemberIds, Has.Count.EqualTo(3));
    }

    private static ScheduleConflict CreateDefaultConflict(ConflictSeverity severity = ConflictSeverity.Medium)
    {
        return new ScheduleConflict(
            TestHelpers.DefaultTenantId,
            new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
            new List<Guid> { Guid.NewGuid() },
            severity);
    }
}
