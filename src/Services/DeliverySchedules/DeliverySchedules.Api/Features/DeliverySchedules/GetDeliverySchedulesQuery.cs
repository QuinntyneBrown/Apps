using DeliverySchedules.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliverySchedules.Api.Features.DeliverySchedules;

public record GetDeliverySchedulesQuery : IRequest<IEnumerable<DeliveryScheduleDto>>
{
    public Guid? LetterId { get; init; }
}

public class GetDeliverySchedulesQueryHandler : IRequestHandler<GetDeliverySchedulesQuery, IEnumerable<DeliveryScheduleDto>>
{
    private readonly IDeliverySchedulesDbContext _context;

    public GetDeliverySchedulesQueryHandler(IDeliverySchedulesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DeliveryScheduleDto>> Handle(GetDeliverySchedulesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.DeliverySchedules.AsNoTracking();

        if (request.LetterId.HasValue)
        {
            query = query.Where(ds => ds.LetterId == request.LetterId.Value);
        }

        return await query
            .OrderBy(ds => ds.ScheduledDateTime)
            .Select(ds => new DeliveryScheduleDto
            {
                DeliveryScheduleId = ds.DeliveryScheduleId,
                LetterId = ds.LetterId,
                ScheduledDateTime = ds.ScheduledDateTime,
                DeliveryMethod = ds.DeliveryMethod,
                RecipientContact = ds.RecipientContact,
                IsActive = ds.IsActive,
                CreatedAt = ds.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
