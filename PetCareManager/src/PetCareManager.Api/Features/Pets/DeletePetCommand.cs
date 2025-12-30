using PetCareManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareManager.Api.Features.Pets;

public record DeletePetCommand : IRequest<bool>
{
    public Guid PetId { get; init; }
}

public class DeletePetCommandHandler : IRequestHandler<DeletePetCommand, bool>
{
    private readonly IPetCareManagerContext _context;
    private readonly ILogger<DeletePetCommandHandler> _logger;

    public DeletePetCommandHandler(
        IPetCareManagerContext context,
        ILogger<DeletePetCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting pet {PetId}", request.PetId);

        var pet = await _context.Pets
            .FirstOrDefaultAsync(p => p.PetId == request.PetId, cancellationToken);

        if (pet == null)
        {
            _logger.LogWarning("Pet {PetId} not found", request.PetId);
            return false;
        }

        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted pet {PetId}", request.PetId);

        return true;
    }
}
