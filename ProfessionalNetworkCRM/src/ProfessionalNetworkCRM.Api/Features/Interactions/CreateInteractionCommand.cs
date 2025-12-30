using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Interactions;

public record CreateInteractionCommand : IRequest<InteractionDto>
{
    public Guid UserId { get; init; }
    public Guid ContactId { get; init; }
    public string InteractionType { get; init; } = string.Empty;
    public DateTime? InteractionDate { get; init; }
    public string? Subject { get; init; }
    public string? Notes { get; init; }
    public string? Outcome { get; init; }
    public int? DurationMinutes { get; init; }
}

public class CreateInteractionCommandHandler : IRequestHandler<CreateInteractionCommand, InteractionDto>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<CreateInteractionCommandHandler> _logger;

    public CreateInteractionCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<CreateInteractionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InteractionDto> Handle(CreateInteractionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating interaction for contact {ContactId}, type: {InteractionType}",
            request.ContactId,
            request.InteractionType);

        var interaction = new Interaction
        {
            InteractionId = Guid.NewGuid(),
            UserId = request.UserId,
            ContactId = request.ContactId,
            InteractionType = request.InteractionType,
            InteractionDate = request.InteractionDate ?? DateTime.UtcNow,
            Subject = request.Subject,
            Notes = request.Notes,
            Outcome = request.Outcome,
            DurationMinutes = request.DurationMinutes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Interactions.Add(interaction);

        // Update contact's last contacted date
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.ContactId == request.ContactId, cancellationToken);

        if (contact != null)
        {
            contact.UpdateLastContactedDate(interaction.InteractionDate);
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created interaction {InteractionId} for contact {ContactId}",
            interaction.InteractionId,
            request.ContactId);

        return interaction.ToDto();
    }
}
