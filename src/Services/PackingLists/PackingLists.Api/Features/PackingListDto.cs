namespace PackingLists.Api.Features;

public record PackingListDto
{
    public Guid PackingListId { get; init; }
    public Guid TripId { get; init; }
    public string ItemName { get; init; } = string.Empty;
    public bool IsPacked { get; init; }
    public DateTime CreatedAt { get; init; }
}
