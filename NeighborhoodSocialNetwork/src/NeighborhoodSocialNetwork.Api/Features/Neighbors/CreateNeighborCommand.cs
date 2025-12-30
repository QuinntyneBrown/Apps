using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Neighbors;

public record CreateNeighborCommand : IRequest<NeighborDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Address { get; init; }
    public string? ContactInfo { get; init; }
    public string? Bio { get; init; }
    public string? Interests { get; init; }
    public bool IsVerified { get; init; }
}

public class CreateNeighborCommandHandler : IRequestHandler<CreateNeighborCommand, NeighborDto>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<CreateNeighborCommandHandler> _logger;

    public CreateNeighborCommandHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<CreateNeighborCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NeighborDto> Handle(CreateNeighborCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating neighbor for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var neighbor = new Neighbor
        {
            NeighborId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Address = request.Address,
            ContactInfo = request.ContactInfo,
            Bio = request.Bio,
            Interests = request.Interests,
            IsVerified = request.IsVerified,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Neighbors.Add(neighbor);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created neighbor {NeighborId} for user {UserId}",
            neighbor.NeighborId,
            request.UserId);

        return neighbor.ToDto();
    }
}
