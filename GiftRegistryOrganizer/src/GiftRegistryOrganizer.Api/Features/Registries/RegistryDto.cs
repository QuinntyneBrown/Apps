using GiftRegistryOrganizer.Core;

namespace GiftRegistryOrganizer.Api.Features.Registries;

public record RegistryDto
{
    public Guid RegistryId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public RegistryType Type { get; init; }
    public DateTime EventDate { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class RegistryExtensions
{
    public static RegistryDto ToDto(this Registry registry)
    {
        return new RegistryDto
        {
            RegistryId = registry.RegistryId,
            UserId = registry.UserId,
            Name = registry.Name,
            Description = registry.Description,
            Type = registry.Type,
            EventDate = registry.EventDate,
            IsActive = registry.IsActive,
            CreatedAt = registry.CreatedAt,
        };
    }
}
