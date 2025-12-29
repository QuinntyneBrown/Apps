namespace FamilyCalendarEventPlanner.Core;

public class AvailabilityBlock
{
    public Guid BlockId { get; private set; }
    public Guid MemberId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public BlockType BlockType { get; private set; }
    public string Reason { get; private set; } = string.Empty;

    private AvailabilityBlock()
    {
    }

    public AvailabilityBlock(Guid memberId, DateTime startTime, DateTime endTime, BlockType blockType, string? reason = null)
    {
        if (endTime <= startTime)
        {
            throw new ArgumentException("End time must be after start time.", nameof(endTime));
        }

        BlockId = Guid.NewGuid();
        MemberId = memberId;
        StartTime = startTime;
        EndTime = endTime;
        BlockType = blockType;
        Reason = reason ?? string.Empty;
    }

    public bool OverlapsWith(DateTime start, DateTime end)
    {
        return StartTime < end && EndTime > start;
    }
}
