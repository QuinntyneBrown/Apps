using Races.Core.Models;

namespace Races.Api.Features;

public record RaceDto
{
    public Guid RaceId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public RaceType RaceType { get; init; }
    public DateTime RaceDate { get; init; }
    public string Location { get; init; } = string.Empty;
    public decimal Distance { get; init; }
    public int? FinishTimeMinutes { get; init; }
    public int? GoalTimeMinutes { get; init; }
    public int? Placement { get; init; }
    public bool IsCompleted { get; init; }
    public string? Notes { get; init; }
}
