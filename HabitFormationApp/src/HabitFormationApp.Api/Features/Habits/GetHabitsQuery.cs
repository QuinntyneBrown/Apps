using HabitFormationApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Habits;

public record GetHabitsQuery : IRequest<IEnumerable<HabitDto>>
{
    public Guid? UserId { get; init; }
    public bool? IsActive { get; init; }
}

public class GetHabitsQueryHandler : IRequestHandler<GetHabitsQuery, IEnumerable<HabitDto>>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<GetHabitsQueryHandler> _logger;

    public GetHabitsQueryHandler(
        IHabitFormationAppContext context,
        ILogger<GetHabitsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<HabitDto>> Handle(GetHabitsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting habits for user {UserId}", request.UserId);

        var query = _context.Habits.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(h => h.UserId == request.UserId.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(h => h.IsActive == request.IsActive.Value);
        }

        var habits = await query
            .OrderByDescending(h => h.CreatedAt)
            .ToListAsync(cancellationToken);

        return habits.Select(h => h.ToDto());
    }
}
