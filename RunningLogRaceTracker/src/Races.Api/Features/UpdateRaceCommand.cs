using MediatR;
using Microsoft.EntityFrameworkCore;
using Races.Core;
using Races.Core.Models;

namespace Races.Api.Features;

public record UpdateRaceCommand : IRequest<RaceDto?>
{
    public Guid RaceId { get; init; }
    public string Name { get; init; } = string.Empty;
    public RaceType RaceType { get; init; }
    public DateTime RaceDate { get; init; }
    public string Location { get; init; } = string.Empty;
    public decimal Distance { get; init; }
    public int? FinishTimeMinutes { get; init; }
    public int? GoalTimeMinutes { get; init; }
    public int? Placement { get; init; }
    public bool IsCompleted { get; init; }
    public string? Notes { get; init; }
}

public class UpdateRaceCommandHandler : IRequestHandler<UpdateRaceCommand, RaceDto?>
{
    private readonly IRacesDbContext _context;

    public UpdateRaceCommandHandler(IRacesDbContext context) => _context = context;

    public async Task<RaceDto?> Handle(UpdateRaceCommand request, CancellationToken cancellationToken)
    {
        var race = await _context.Races.FirstOrDefaultAsync(r => r.RaceId == request.RaceId, cancellationToken);
        if (race == null) return null;

        race.Name = request.Name;
        race.RaceType = request.RaceType;
        race.RaceDate = request.RaceDate;
        race.Location = request.Location;
        race.Distance = request.Distance;
        race.FinishTimeMinutes = request.FinishTimeMinutes;
        race.GoalTimeMinutes = request.GoalTimeMinutes;
        race.Placement = request.Placement;
        race.IsCompleted = request.IsCompleted;
        race.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new RaceDto
        {
            RaceId = race.RaceId,
            UserId = race.UserId,
            Name = race.Name,
            RaceType = race.RaceType,
            RaceDate = race.RaceDate,
            Location = race.Location,
            Distance = race.Distance,
            FinishTimeMinutes = race.FinishTimeMinutes,
            GoalTimeMinutes = race.GoalTimeMinutes,
            Placement = race.Placement,
            IsCompleted = race.IsCompleted,
            Notes = race.Notes
        };
    }
}
