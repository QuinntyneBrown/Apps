using PhotographySessionLogger.Core;

namespace PhotographySessionLogger.Api.Features.Gears;

public record GearDto
{
    public Guid GearId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string GearType { get; init; } = string.Empty;
    public string? Brand { get; init; }
    public string? Model { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public decimal? PurchasePrice { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class GearExtensions
{
    public static GearDto ToDto(this Gear gear)
    {
        return new GearDto
        {
            GearId = gear.GearId,
            UserId = gear.UserId,
            Name = gear.Name,
            GearType = gear.GearType,
            Brand = gear.Brand,
            Model = gear.Model,
            PurchaseDate = gear.PurchaseDate,
            PurchasePrice = gear.PurchasePrice,
            Notes = gear.Notes,
            CreatedAt = gear.CreatedAt,
        };
    }
}
