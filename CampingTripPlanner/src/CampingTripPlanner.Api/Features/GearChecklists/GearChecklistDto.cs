using CampingTripPlanner.Core;

namespace CampingTripPlanner.Api.Features.GearChecklists;

public record GearChecklistDto
{
    public Guid GearChecklistId { get; init; }
    public Guid UserId { get; init; }
    public Guid TripId { get; init; }
    public string ItemName { get; init; } = string.Empty;
    public bool IsPacked { get; init; }
    public int Quantity { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class GearChecklistExtensions
{
    public static GearChecklistDto ToDto(this GearChecklist gearChecklist)
    {
        return new GearChecklistDto
        {
            GearChecklistId = gearChecklist.GearChecklistId,
            UserId = gearChecklist.UserId,
            TripId = gearChecklist.TripId,
            ItemName = gearChecklist.ItemName,
            IsPacked = gearChecklist.IsPacked,
            Quantity = gearChecklist.Quantity,
            Notes = gearChecklist.Notes,
            CreatedAt = gearChecklist.CreatedAt,
        };
    }
}
