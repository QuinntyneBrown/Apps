using Schedules.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Schedules.Api.Features;

public record DeleteScheduleCommand(Guid ScheduleId) : IRequest<bool>;

public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, bool>
{
    private readonly ISchedulesDbContext _context;
    private readonly ILogger<DeleteScheduleCommandHandler> _logger;

    public DeleteScheduleCommandHandler(ISchedulesDbContext context, ILogger<DeleteScheduleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _context.Schedules
            .FirstOrDefaultAsync(s => s.ScheduleId == request.ScheduleId, cancellationToken);

        if (schedule == null) return false;

        _context.Schedules.Remove(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Schedule deleted: {ScheduleId}", request.ScheduleId);

        return true;
    }
}
