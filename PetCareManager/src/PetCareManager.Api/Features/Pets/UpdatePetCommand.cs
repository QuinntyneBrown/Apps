using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Pets;

public record UpdatePetCommand : IRequest<PetDto?>
{
    public Guid PetId { get; init; }
    public string Name { get; init; } = string.Empty;
    public PetType PetType { get; init; }
    public string? Breed { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string? Color { get; init; }
    public decimal? Weight { get; init; }
    public string? MicrochipNumber { get; init; }
}

public class UpdatePetCommandHandler : IRequestHandler<UpdatePetCommand, PetDto?>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<UpdatePetCommandHandler> _logger;

    public UpdatePetCommandHandler(
        IPetCareManagerContext context,
        ILogger<UpdatePetCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PetDto?> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating pet {PetId}", request.PetId);

        var pet = await _context.Pets
            .FirstOrDefaultAsync(p => p.PetId == request.PetId, cancellationToken);

        if (pet == null)
        {
            _logger.LogWarning("Pet {PetId} not found", request.PetId);
            return null;
        }

        pet.Name = request.Name;
        pet.PetType = request.PetType;
        pet.Breed = request.Breed;
        pet.DateOfBirth = request.DateOfBirth;
        pet.Color = request.Color;
        pet.Weight = request.Weight;
        pet.MicrochipNumber = request.MicrochipNumber;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated pet {PetId}", request.PetId);

        return pet.ToDto();
    }
}
