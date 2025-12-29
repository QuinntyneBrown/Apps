using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate;
using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate.Enums;

namespace FamilyCalendarEventPlanner.Api.Features.Availability;

public record AvailabilityBlockDto
{
    public Guid BlockId { get; init; }
    public Guid MemberId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public BlockType BlockType { get; init; }
    public string Reason { get; init; } = string.Empty;
}

public static class AvailabilityBlockExtensions
{
    public static AvailabilityBlockDto ToDto(this AvailabilityBlock block)
    {
        return new AvailabilityBlockDto
        {
            BlockId = block.BlockId,
            MemberId = block.MemberId,
            StartTime = block.StartTime,
            EndTime = block.EndTime,
            BlockType = block.BlockType,
            Reason = block.Reason,
        };
    }
}
