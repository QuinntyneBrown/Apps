using WoodworkingProjectManager.Core;

namespace WoodworkingProjectManager.Api.Features.Tools;

public record ToolDto
{
    public Guid ToolId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Brand { get; init; }
    public string? Model { get; init; }
    public string? Description { get; init; }
    public decimal? PurchasePrice { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public string? Location { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ToolExtensions
{
    public static ToolDto ToDto(this Tool tool)
    {
        return new ToolDto
        {
            ToolId = tool.ToolId,
            UserId = tool.UserId,
            Name = tool.Name,
            Brand = tool.Brand,
            Model = tool.Model,
            Description = tool.Description,
            PurchasePrice = tool.PurchasePrice,
            PurchaseDate = tool.PurchaseDate,
            Location = tool.Location,
            Notes = tool.Notes,
            CreatedAt = tool.CreatedAt,
        };
    }
}
