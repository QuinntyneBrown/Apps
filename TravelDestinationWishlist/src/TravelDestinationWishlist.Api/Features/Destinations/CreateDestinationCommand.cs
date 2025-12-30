using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Destinations;

public record CreateDestinationCommand : IRequest<DestinationDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public DestinationType DestinationType { get; init; }
    public string? Description { get; init; }
    public int Priority { get; init; } = 3;
    public string? Notes { get; init; }
}

public class CreateDestinationCommandHandler : IRequestHandler<CreateDestinationCommand, DestinationDto>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<CreateDestinationCommandHandler> _logger;

    public CreateDestinationCommandHandler(
        ITravelDestinationWishlistContext context,
        ILogger<CreateDestinationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DestinationDto> Handle(CreateDestinationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating destination for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var destination = new Destination
        {
            DestinationId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Country = request.Country,
            DestinationType = request.DestinationType,
            Description = request.Description,
            Priority = request.Priority,
            Notes = request.Notes,
            IsVisited = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Destinations.Add(destination);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created destination {DestinationId} for user {UserId}",
            destination.DestinationId,
            request.UserId);

        return destination.ToDto();
    }
}
