using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Interactions;

public record GetInteractionByIdQuery : IRequest<InteractionDto?>
{
    public Guid InteractionId { get; init; }
}

public class GetInteractionByIdQueryHandler : IRequestHandler<GetInteractionByIdQuery, InteractionDto?>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<GetInteractionByIdQueryHandler> _logger;

    public GetInteractionByIdQueryHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<GetInteractionByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InteractionDto?> Handle(GetInteractionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting interaction {InteractionId}", request.InteractionId);

        var interaction = await _context.Interactions
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.InteractionId == request.InteractionId, cancellationToken);

        if (interaction == null)
        {
            _logger.LogWarning("Interaction {InteractionId} not found", request.InteractionId);
            return null;
        }

        return interaction.ToDto();
    }
}
