using Itineraries.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itineraries.Api.Features;

public record DeleteItineraryCommand(Guid ItineraryId) : IRequest<bool>;

public class DeleteItineraryCommandHandler : IRequestHandler<DeleteItineraryCommand, bool>
{
    private readonly IItinerariesDbContext _context;

    public DeleteItineraryCommandHandler(IItinerariesDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteItineraryCommand request, CancellationToken cancellationToken)
    {
        var itinerary = await _context.Itineraries
            .FirstOrDefaultAsync(i => i.ItineraryId == request.ItineraryId, cancellationToken);

        if (itinerary == null) return false;

        _context.Itineraries.Remove(itinerary);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
