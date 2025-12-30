using WoodworkingProjectManager.Core;

namespace WoodworkingProjectManager.Api.Features.Materials;

public record MaterialDto
{
    public Guid MaterialId { get; init; }
    public Guid UserId { get; init; }
    public Guid? ProjectId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal Quantity { get; init; }
    public string Unit { get; init; } = string.Empty;
    public decimal? Cost { get; init; }
    public string? Supplier { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class MaterialExtensions
{
    public static MaterialDto ToDto(this Material material)
    {
        return new MaterialDto
        {
            MaterialId = material.MaterialId,
            UserId = material.UserId,
            ProjectId = material.ProjectId,
            Name = material.Name,
            Description = material.Description,
            Quantity = material.Quantity,
            Unit = material.Unit,
            Cost = material.Cost,
            Supplier = material.Supplier,
            CreatedAt = material.CreatedAt,
        };
    }
}
