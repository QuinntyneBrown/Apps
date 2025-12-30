using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Destinations;

public record UpdateDestinationCommand : IRequest<DestinationDto?>
{
    public Guid DestinationId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public DestinationType DestinationType { get; init; }
    public string? Description { get; init; }
    public int Priority { get; init; }
    public string? Notes { get; init; }
}

public class UpdateDestinationCommandHandler : IRequestHandler<UpdateDestinationCommand, DestinationDto?>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<UpdateDestinationCommandHandler> _logger;

    public UpdateDestinationCommandHandler(
        ITravelDestinationWishlistContext context,
        ILogger<UpdateDestinationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DestinationDto?> Handle(UpdateDestinationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating destination {DestinationId}", request.DestinationId);

        var destination = await _context.Destinations
            .FirstOrDefaultAsync(d => d.DestinationId == request.DestinationId, cancellationToken);

        if (destination == null)
        {
            _logger.LogWarning("Destination {DestinationId} not found", request.DestinationId);
            return null;
        }

        destination.Name = request.Name;
        destination.Country = request.Country;
        destination.DestinationType = request.DestinationType;
        destination.Description = request.Description;
        destination.Priority = request.Priority;
        destination.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated destination {DestinationId}", request.DestinationId);

        return destination.ToDto();
    }
}
