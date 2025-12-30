using HabitFormationApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Habits;

public record GetHabitByIdQuery : IRequest<HabitDto?>
{
    public Guid HabitId { get; init; }
}

public class GetHabitByIdQueryHandler : IRequestHandler<GetHabitByIdQuery, HabitDto?>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<GetHabitByIdQueryHandler> _logger;

    public GetHabitByIdQueryHandler(
        IHabitFormationAppContext context,
        ILogger<GetHabitByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HabitDto?> Handle(GetHabitByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting habit {HabitId}", request.HabitId);

        var habit = await _context.Habits
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.HabitId == request.HabitId, cancellationToken);

        return habit?.ToDto();
    }
}
