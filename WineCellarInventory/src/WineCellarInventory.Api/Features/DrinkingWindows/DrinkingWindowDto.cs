using WineCellarInventory.Core;

namespace WineCellarInventory.Api.Features.DrinkingWindows;

public record DrinkingWindowDto
{
    public Guid DrinkingWindowId { get; init; }
    public Guid UserId { get; init; }
    public Guid WineId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class DrinkingWindowExtensions
{
    public static DrinkingWindowDto ToDto(this DrinkingWindow drinkingWindow)
    {
        return new DrinkingWindowDto
        {
            DrinkingWindowId = drinkingWindow.DrinkingWindowId,
            UserId = drinkingWindow.UserId,
            WineId = drinkingWindow.WineId,
            StartDate = drinkingWindow.StartDate,
            EndDate = drinkingWindow.EndDate,
            Notes = drinkingWindow.Notes,
            CreatedAt = drinkingWindow.CreatedAt,
        };
    }
}
