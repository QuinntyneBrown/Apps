using DeliverySchedules.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliverySchedules.Api.Features.DeliverySchedules;

public record UpdateDeliveryScheduleCommand : IRequest<DeliveryScheduleDto?>
{
    public Guid DeliveryScheduleId { get; init; }
    public DateTime? ScheduledDateTime { get; init; }
    public string? DeliveryMethod { get; init; }
    public string? RecipientContact { get; init; }
    public bool? IsActive { get; init; }
}

public class UpdateDeliveryScheduleCommandHandler : IRequestHandler<UpdateDeliveryScheduleCommand, DeliveryScheduleDto?>
{
    private readonly IDeliverySchedulesDbContext _context;
    private readonly ILogger<UpdateDeliveryScheduleCommandHandler> _logger;

    public UpdateDeliveryScheduleCommandHandler(IDeliverySchedulesDbContext context, ILogger<UpdateDeliveryScheduleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DeliveryScheduleDto?> Handle(UpdateDeliveryScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _context.DeliverySchedules
            .FirstOrDefaultAsync(ds => ds.DeliveryScheduleId == request.DeliveryScheduleId, cancellationToken);

        if (schedule == null) return null;

        if (request.ScheduledDateTime.HasValue) schedule.ScheduledDateTime = request.ScheduledDateTime.Value;
        if (request.DeliveryMethod != null) schedule.DeliveryMethod = request.DeliveryMethod;
        if (request.RecipientContact != null) schedule.RecipientContact = request.RecipientContact;
        if (request.IsActive.HasValue) schedule.IsActive = request.IsActive.Value;
        schedule.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Delivery schedule updated: {DeliveryScheduleId}", schedule.DeliveryScheduleId);

        return new DeliveryScheduleDto
        {
            DeliveryScheduleId = schedule.DeliveryScheduleId,
            LetterId = schedule.LetterId,
            ScheduledDateTime = schedule.ScheduledDateTime,
            DeliveryMethod = schedule.DeliveryMethod,
            RecipientContact = schedule.RecipientContact,
            IsActive = schedule.IsActive,
            CreatedAt = schedule.CreatedAt
        };
    }
}
