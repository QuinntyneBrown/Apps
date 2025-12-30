using PetCareManager.Core;

namespace PetCareManager.Api.Features.Pets;

public record PetDto
{
    public Guid PetId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public PetType PetType { get; init; }
    public string? Breed { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string? Color { get; init; }
    public decimal? Weight { get; init; }
    public string? MicrochipNumber { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class PetExtensions
{
    public static PetDto ToDto(this Pet pet)
    {
        return new PetDto
        {
            PetId = pet.PetId,
            UserId = pet.UserId,
            Name = pet.Name,
            PetType = pet.PetType,
            Breed = pet.Breed,
            DateOfBirth = pet.DateOfBirth,
            Color = pet.Color,
            Weight = pet.Weight,
            MicrochipNumber = pet.MicrochipNumber,
            CreatedAt = pet.CreatedAt,
        };
    }
}
