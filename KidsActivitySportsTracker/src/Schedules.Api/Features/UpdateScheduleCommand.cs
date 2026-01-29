using Schedules.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Schedules.Api.Features;

public record UpdateScheduleCommand(
    Guid ScheduleId,
    DateTime EventDate,
    TimeSpan StartTime,
    TimeSpan? EndTime,
    string? Location,
    string? Notes,
    bool IsRecurring,
    string? RecurrencePattern) : IRequest<ScheduleDto?>;

public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, ScheduleDto?>
{
    private readonly ISchedulesDbContext _context;
    private readonly ILogger<UpdateScheduleCommandHandler> _logger;

    public UpdateScheduleCommandHandler(ISchedulesDbContext context, ILogger<UpdateScheduleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ScheduleDto?> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _context.Schedules
            .FirstOrDefaultAsync(s => s.ScheduleId == request.ScheduleId, cancellationToken);

        if (schedule == null) return null;

        schedule.EventDate = request.EventDate;
        schedule.StartTime = request.StartTime;
        schedule.EndTime = request.EndTime;
        schedule.Location = request.Location;
        schedule.Notes = request.Notes;
        schedule.IsRecurring = request.IsRecurring;
        schedule.RecurrencePattern = request.RecurrencePattern;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Schedule updated: {ScheduleId}", schedule.ScheduleId);

        return schedule.ToDto();
    }
}
