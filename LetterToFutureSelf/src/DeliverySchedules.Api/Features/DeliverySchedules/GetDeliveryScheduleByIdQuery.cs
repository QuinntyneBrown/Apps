using DeliverySchedules.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliverySchedules.Api.Features.DeliverySchedules;

public record GetDeliveryScheduleByIdQuery : IRequest<DeliveryScheduleDto?>
{
    public Guid DeliveryScheduleId { get; init; }
}

public class GetDeliveryScheduleByIdQueryHandler : IRequestHandler<GetDeliveryScheduleByIdQuery, DeliveryScheduleDto?>
{
    private readonly IDeliverySchedulesDbContext _context;

    public GetDeliveryScheduleByIdQueryHandler(IDeliverySchedulesDbContext context)
    {
        _context = context;
    }

    public async Task<DeliveryScheduleDto?> Handle(GetDeliveryScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var schedule = await _context.DeliverySchedules
            .AsNoTracking()
            .FirstOrDefaultAsync(ds => ds.DeliveryScheduleId == request.DeliveryScheduleId, cancellationToken);

        if (schedule == null) return null;

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
