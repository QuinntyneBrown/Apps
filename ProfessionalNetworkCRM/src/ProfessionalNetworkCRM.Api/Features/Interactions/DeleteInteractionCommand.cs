using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Interactions;

public record DeleteInteractionCommand : IRequest<bool>
{
    public Guid InteractionId { get; init; }
}

public class DeleteInteractionCommandHandler : IRequestHandler<DeleteInteractionCommand, bool>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<DeleteInteractionCommandHandler> _logger;

    public DeleteInteractionCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<DeleteInteractionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteInteractionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting interaction {InteractionId}", request.InteractionId);

        var interaction = await _context.Interactions
            .FirstOrDefaultAsync(i => i.InteractionId == request.InteractionId, cancellationToken);

        if (interaction == null)
        {
            _logger.LogWarning("Interaction {InteractionId} not found", request.InteractionId);
            return false;
        }

        _context.Interactions.Remove(interaction);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted interaction {InteractionId}", request.InteractionId);

        return true;
    }
}
