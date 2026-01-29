using MediatR;
using Microsoft.EntityFrameworkCore;
using Races.Core;

namespace Races.Api.Features;

public record DeleteRaceCommand(Guid RaceId) : IRequest<bool>;

public class DeleteRaceCommandHandler : IRequestHandler<DeleteRaceCommand, bool>
{
    private readonly IRacesDbContext _context;

    public DeleteRaceCommandHandler(IRacesDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteRaceCommand request, CancellationToken cancellationToken)
    {
        var race = await _context.Races.FirstOrDefaultAsync(r => r.RaceId == request.RaceId, cancellationToken);
        if (race == null) return false;

        _context.Races.Remove(race);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
