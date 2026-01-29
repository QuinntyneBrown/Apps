using MediatR;
using Races.Core;
using Races.Core.Models;

namespace Races.Api.Features;

public record CreateRaceCommand : IRequest<RaceDto>
{
    public Guid UserId { get; init; }
    public Guid TenantId { get; init; }
    public string Name { get; init; } = string.Empty;
    public RaceType RaceType { get; init; }
    public DateTime RaceDate { get; init; }
    public string Location { get; init; } = string.Empty;
    public decimal Distance { get; init; }
    public int? GoalTimeMinutes { get; init; }
    public string? Notes { get; init; }
}

public class CreateRaceCommandHandler : IRequestHandler<CreateRaceCommand, RaceDto>
{
    private readonly IRacesDbContext _context;

    public CreateRaceCommandHandler(IRacesDbContext context) => _context = context;

    public async Task<RaceDto> Handle(CreateRaceCommand request, CancellationToken cancellationToken)
    {
        var race = new Race
        {
            RaceId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Name = request.Name,
            RaceType = request.RaceType,
            RaceDate = request.RaceDate,
            Location = request.Location,
            Distance = request.Distance,
            GoalTimeMinutes = request.GoalTimeMinutes,
            Notes = request.Notes,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Races.Add(race);
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
            GoalTimeMinutes = race.GoalTimeMinutes,
            IsCompleted = race.IsCompleted,
            Notes = race.Notes
        };
    }
}
