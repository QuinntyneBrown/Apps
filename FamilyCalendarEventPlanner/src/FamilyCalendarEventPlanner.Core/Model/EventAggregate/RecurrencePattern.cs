using FamilyCalendarEventPlanner.Core.Model.EventAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Model.EventAggregate;

public class RecurrencePattern
{
    public RecurrenceFrequency Frequency { get; private set; }
    public int Interval { get; private set; }
    public DateTime? EndDate { get; private set; }
    public List<DayOfWeek> DaysOfWeek { get; private set; }

    private RecurrencePattern()
    {
        DaysOfWeek = new List<DayOfWeek>();
    }

    public RecurrencePattern(RecurrenceFrequency frequency, int interval = 1, DateTime? endDate = null, List<DayOfWeek>? daysOfWeek = null)
    {
        if (interval < 1)
        {
            throw new ArgumentException("Interval must be at least 1.", nameof(interval));
        }

        Frequency = frequency;
        Interval = interval;
        EndDate = endDate;
        DaysOfWeek = daysOfWeek ?? new List<DayOfWeek>();
    }

    public static RecurrencePattern None()
    {
        return new RecurrencePattern(RecurrenceFrequency.None);
    }

    public static RecurrencePattern Daily(int interval = 1, DateTime? endDate = null)
    {
        return new RecurrencePattern(RecurrenceFrequency.Daily, interval, endDate);
    }

    public static RecurrencePattern Weekly(int interval = 1, DateTime? endDate = null, List<DayOfWeek>? daysOfWeek = null)
    {
        return new RecurrencePattern(RecurrenceFrequency.Weekly, interval, endDate, daysOfWeek);
    }

    public static RecurrencePattern Monthly(int interval = 1, DateTime? endDate = null)
    {
        return new RecurrencePattern(RecurrenceFrequency.Monthly, interval, endDate);
    }

    public static RecurrencePattern Yearly(int interval = 1, DateTime? endDate = null)
    {
        return new RecurrencePattern(RecurrenceFrequency.Yearly, interval, endDate);
    }
}
