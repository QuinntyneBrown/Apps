using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.Campsites;

public record DeleteCampsiteCommand : IRequest<bool>
{
    public Guid CampsiteId { get; init; }
}

public class DeleteCampsiteCommandHandler : IRequestHandler<DeleteCampsiteCommand, bool>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<DeleteCampsiteCommandHandler> _logger;

    public DeleteCampsiteCommandHandler(
        ICampingTripPlannerContext context,
        ILogger<DeleteCampsiteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCampsiteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting campsite {CampsiteId}", request.CampsiteId);

        var campsite = await _context.Campsites
            .FirstOrDefaultAsync(c => c.CampsiteId == request.CampsiteId, cancellationToken);

        if (campsite == null)
        {
            _logger.LogWarning("Campsite {CampsiteId} not found", request.CampsiteId);
            return false;
        }

        _context.Campsites.Remove(campsite);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted campsite {CampsiteId}", request.CampsiteId);

        return true;
    }
}
