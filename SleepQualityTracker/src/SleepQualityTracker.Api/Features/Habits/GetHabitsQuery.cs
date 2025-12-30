using SleepQualityTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.Habits;

public record GetHabitsQuery : IRequest<IEnumerable<HabitDto>>
{
    public Guid? UserId { get; init; }
    public string? HabitType { get; init; }
    public bool? IsPositive { get; init; }
    public bool? IsActive { get; init; }
    public bool? IsHighImpact { get; init; }
}

public class GetHabitsQueryHandler : IRequestHandler<GetHabitsQuery, IEnumerable<HabitDto>>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<GetHabitsQueryHandler> _logger;

    public GetHabitsQueryHandler(
        ISleepQualityTrackerContext context,
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

        if (!string.IsNullOrEmpty(request.HabitType))
        {
            query = query.Where(h => h.HabitType == request.HabitType);
        }

        if (request.IsPositive.HasValue)
        {
            query = query.Where(h => h.IsPositive == request.IsPositive.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(h => h.IsActive == request.IsActive.Value);
        }

        var habits = await query
            .OrderBy(h => h.Name)
            .ToListAsync(cancellationToken);

        var results = habits.Select(h => h.ToDto());

        if (request.IsHighImpact.HasValue)
        {
            results = results.Where(h => h.IsHighImpact == request.IsHighImpact.Value);
        }

        return results;
    }
}
