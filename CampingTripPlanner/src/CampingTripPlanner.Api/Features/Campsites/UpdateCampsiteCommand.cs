using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.Campsites;

public record UpdateCampsiteCommand : IRequest<CampsiteDto?>
{
    public Guid CampsiteId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public CampsiteType CampsiteType { get; init; }
    public string? Description { get; init; }
    public bool HasElectricity { get; init; }
    public bool HasWater { get; init; }
    public decimal? CostPerNight { get; init; }
    public bool IsFavorite { get; init; }
}

public class UpdateCampsiteCommandHandler : IRequestHandler<UpdateCampsiteCommand, CampsiteDto?>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<UpdateCampsiteCommandHandler> _logger;

    public UpdateCampsiteCommandHandler(
        ICampingTripPlannerContext context,
        ILogger<UpdateCampsiteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CampsiteDto?> Handle(UpdateCampsiteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating campsite {CampsiteId}", request.CampsiteId);

        var campsite = await _context.Campsites
            .FirstOrDefaultAsync(c => c.CampsiteId == request.CampsiteId, cancellationToken);

        if (campsite == null)
        {
            _logger.LogWarning("Campsite {CampsiteId} not found", request.CampsiteId);
            return null;
        }

        campsite.Name = request.Name;
        campsite.Location = request.Location;
        campsite.CampsiteType = request.CampsiteType;
        campsite.Description = request.Description;
        campsite.HasElectricity = request.HasElectricity;
        campsite.HasWater = request.HasWater;
        campsite.CostPerNight = request.CostPerNight;
        campsite.IsFavorite = request.IsFavorite;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated campsite {CampsiteId}", request.CampsiteId);

        return campsite.ToDto();
    }
}
