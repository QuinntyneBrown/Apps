namespace Scheduling.Core.Models;

public class Availability
{
    public Guid AvailabilityId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public bool IsAvailable { get; private set; }

    private Availability() { }

    public Availability(Guid tenantId, Guid userId, DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime, bool isAvailable = true)
    {
        if (endTime <= startTime)
            throw new ArgumentException("End time must be after start time.", nameof(endTime));

        AvailabilityId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        IsAvailable = isAvailable;
    }

    public void Update(TimeOnly? startTime = null, TimeOnly? endTime = null, bool? isAvailable = null)
    {
        if (startTime.HasValue)
            StartTime = startTime.Value;

        if (endTime.HasValue)
        {
            if (endTime.Value <= StartTime)
                throw new ArgumentException("End time must be after start time.", nameof(endTime));
            EndTime = endTime.Value;
        }

        if (isAvailable.HasValue)
            IsAvailable = isAvailable.Value;
    }
}
