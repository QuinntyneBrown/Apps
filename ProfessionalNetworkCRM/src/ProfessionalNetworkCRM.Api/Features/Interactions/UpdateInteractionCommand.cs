using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Interactions;

public record UpdateInteractionCommand : IRequest<InteractionDto?>
{
    public Guid InteractionId { get; init; }
    public string InteractionType { get; init; } = string.Empty;
    public DateTime InteractionDate { get; init; }
    public string? Subject { get; init; }
    public string? Notes { get; init; }
    public string? Outcome { get; init; }
    public int? DurationMinutes { get; init; }
}

public class UpdateInteractionCommandHandler : IRequestHandler<UpdateInteractionCommand, InteractionDto?>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<UpdateInteractionCommandHandler> _logger;

    public UpdateInteractionCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<UpdateInteractionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InteractionDto?> Handle(UpdateInteractionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating interaction {InteractionId}",
            request.InteractionId);

        var interaction = await _context.Interactions
            .FirstOrDefaultAsync(i => i.InteractionId == request.InteractionId, cancellationToken);

        if (interaction == null)
        {
            _logger.LogWarning("Interaction {InteractionId} not found", request.InteractionId);
            return null;
        }

        interaction.InteractionType = request.InteractionType;
        interaction.InteractionDate = request.InteractionDate;
        interaction.Subject = request.Subject;
        interaction.Notes = request.Notes;
        interaction.Outcome = request.Outcome;
        interaction.DurationMinutes = request.DurationMinutes;
        interaction.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated interaction {InteractionId}", request.InteractionId);

        return interaction.ToDto();
    }
}
