using PaymentSchedules.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PaymentSchedules.Api.Features;

public record GetPaymentScheduleByIdQuery(Guid ScheduleId) : IRequest<PaymentScheduleDto?>;

public class GetPaymentScheduleByIdQueryHandler : IRequestHandler<GetPaymentScheduleByIdQuery, PaymentScheduleDto?>
{
    private readonly IPaymentSchedulesDbContext _context;

    public GetPaymentScheduleByIdQueryHandler(IPaymentSchedulesDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentScheduleDto?> Handle(GetPaymentScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var schedule = await _context.PaymentSchedules.AsNoTracking()
            .FirstOrDefaultAsync(s => s.ScheduleId == request.ScheduleId, cancellationToken);
        return schedule?.ToDto();
    }
}
