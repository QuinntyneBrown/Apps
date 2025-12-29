using FamilyCalendarEventPlanner.Core.Model.EventAggregate;
using FamilyCalendarEventPlanner.Core.Model.EventAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Tests;

public class RecurrencePatternTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesPattern()
    {
        var endDate = DateTime.UtcNow.AddMonths(6);
        var daysOfWeek = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday };

        var pattern = new RecurrencePattern(RecurrenceFrequency.Weekly, 2, endDate, daysOfWeek);

        Assert.Multiple(() =>
        {
            Assert.That(pattern.Frequency, Is.EqualTo(RecurrenceFrequency.Weekly));
            Assert.That(pattern.Interval, Is.EqualTo(2));
            Assert.That(pattern.EndDate, Is.EqualTo(endDate));
            Assert.That(pattern.DaysOfWeek, Is.EqualTo(daysOfWeek));
        });
    }

    [Test]
    public void Constructor_IntervalLessThanOne_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new RecurrencePattern(RecurrenceFrequency.Daily, 0));
    }

    [Test]
    public void Constructor_NegativeInterval_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new RecurrencePattern(RecurrenceFrequency.Daily, -1));
    }

    [Test]
    public void None_ReturnsPatternWithNoFrequency()
    {
        var pattern = RecurrencePattern.None();

        Assert.That(pattern.Frequency, Is.EqualTo(RecurrenceFrequency.None));
    }

    [Test]
    public void Daily_DefaultInterval_ReturnsPatternWithIntervalOne()
    {
        var pattern = RecurrencePattern.Daily();

        Assert.Multiple(() =>
        {
            Assert.That(pattern.Frequency, Is.EqualTo(RecurrenceFrequency.Daily));
            Assert.That(pattern.Interval, Is.EqualTo(1));
        });
    }

    [Test]
    public void Daily_CustomInterval_ReturnsPatternWithCustomInterval()
    {
        var pattern = RecurrencePattern.Daily(3);

        Assert.That(pattern.Interval, Is.EqualTo(3));
    }

    [Test]
    public void Weekly_WithDaysOfWeek_ReturnsPatternWithDays()
    {
        var days = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday };

        var pattern = RecurrencePattern.Weekly(1, null, days);

        Assert.Multiple(() =>
        {
            Assert.That(pattern.Frequency, Is.EqualTo(RecurrenceFrequency.Weekly));
            Assert.That(pattern.DaysOfWeek, Contains.Item(DayOfWeek.Tuesday));
            Assert.That(pattern.DaysOfWeek, Contains.Item(DayOfWeek.Thursday));
        });
    }

    [Test]
    public void Monthly_DefaultInterval_ReturnsPatternWithIntervalOne()
    {
        var pattern = RecurrencePattern.Monthly();

        Assert.Multiple(() =>
        {
            Assert.That(pattern.Frequency, Is.EqualTo(RecurrenceFrequency.Monthly));
            Assert.That(pattern.Interval, Is.EqualTo(1));
        });
    }

    [Test]
    public void Yearly_WithEndDate_ReturnsPatternWithEndDate()
    {
        var endDate = DateTime.UtcNow.AddYears(5);

        var pattern = RecurrencePattern.Yearly(1, endDate);

        Assert.Multiple(() =>
        {
            Assert.That(pattern.Frequency, Is.EqualTo(RecurrenceFrequency.Yearly));
            Assert.That(pattern.EndDate, Is.EqualTo(endDate));
        });
    }

    [Test]
    public void Constructor_NullDaysOfWeek_InitializesEmptyList()
    {
        var pattern = new RecurrencePattern(RecurrenceFrequency.Weekly);

        Assert.That(pattern.DaysOfWeek, Is.Not.Null);
        Assert.That(pattern.DaysOfWeek, Is.Empty);
    }
}
