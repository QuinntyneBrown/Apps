using PetCareManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Pets;

public record CreatePetCommand : IRequest<PetDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public PetType PetType { get; init; }
    public string? Breed { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string? Color { get; init; }
    public decimal? Weight { get; init; }
    public string? MicrochipNumber { get; init; }
}

public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, PetDto>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<CreatePetCommandHandler> _logger;

    public CreatePetCommandHandler(
        IPetCareManagerContext context,
        ILogger<CreatePetCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PetDto> Handle(CreatePetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating pet for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var pet = new Pet
        {
            PetId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            PetType = request.PetType,
            Breed = request.Breed,
            DateOfBirth = request.DateOfBirth,
            Color = request.Color,
            Weight = request.Weight,
            MicrochipNumber = request.MicrochipNumber,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Pets.Add(pet);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created pet {PetId} for user {UserId}",
            pet.PetId,
            request.UserId);

        return pet.ToDto();
    }
}
