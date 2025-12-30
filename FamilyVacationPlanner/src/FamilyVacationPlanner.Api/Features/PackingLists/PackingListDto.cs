using FamilyVacationPlanner.Core;

namespace FamilyVacationPlanner.Api.Features.PackingLists;

public record PackingListDto
{
    public Guid PackingListId { get; init; }
    public Guid TripId { get; init; }
    public string ItemName { get; init; } = string.Empty;
    public bool IsPacked { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class PackingListExtensions
{
    public static PackingListDto ToDto(this PackingList packingList)
    {
        return new PackingListDto
        {
            PackingListId = packingList.PackingListId,
            TripId = packingList.TripId,
            ItemName = packingList.ItemName,
            IsPacked = packingList.IsPacked,
            CreatedAt = packingList.CreatedAt,
        };
    }
}
