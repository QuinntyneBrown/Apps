using CampingTripPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.Campsites;

public record CreateCampsiteCommand : IRequest<CampsiteDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public CampsiteType CampsiteType { get; init; }
    public string? Description { get; init; }
    public bool HasElectricity { get; init; }
    public bool HasWater { get; init; }
    public decimal? CostPerNight { get; init; }
    public bool IsFavorite { get; init; }
}

public class CreateCampsiteCommandHandler : IRequestHandler<CreateCampsiteCommand, CampsiteDto>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<CreateCampsiteCommandHandler> _logger;

    public CreateCampsiteCommandHandler(
        ICampingTripPlannerContext context,
        ILogger<CreateCampsiteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CampsiteDto> Handle(CreateCampsiteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating campsite for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var campsite = new Campsite
        {
            CampsiteId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Location = request.Location,
            CampsiteType = request.CampsiteType,
            Description = request.Description,
            HasElectricity = request.HasElectricity,
            HasWater = request.HasWater,
            CostPerNight = request.CostPerNight,
            IsFavorite = request.IsFavorite,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Campsites.Add(campsite);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created campsite {CampsiteId} for user {UserId}",
            campsite.CampsiteId,
            request.UserId);

        return campsite.ToDto();
    }
}
