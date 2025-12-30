using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Itineraries;

public record DeleteItineraryCommand : IRequest<bool>
{
    public Guid ItineraryId { get; init; }
}

public class DeleteItineraryCommandHandler : IRequestHandler<DeleteItineraryCommand, bool>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<DeleteItineraryCommandHandler> _logger;

    public DeleteItineraryCommandHandler(
        IFamilyVacationPlannerContext context,
        ILogger<DeleteItineraryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteItineraryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting itinerary {ItineraryId}", request.ItineraryId);

        var itinerary = await _context.Itineraries
            .FirstOrDefaultAsync(i => i.ItineraryId == request.ItineraryId, cancellationToken);

        if (itinerary == null)
        {
            _logger.LogWarning("Itinerary {ItineraryId} not found", request.ItineraryId);
            return false;
        }

        _context.Itineraries.Remove(itinerary);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted itinerary {ItineraryId}", request.ItineraryId);

        return true;
    }
}
