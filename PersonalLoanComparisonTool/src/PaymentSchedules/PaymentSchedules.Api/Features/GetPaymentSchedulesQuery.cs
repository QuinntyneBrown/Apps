using PaymentSchedules.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PaymentSchedules.Api.Features;

public record GetPaymentSchedulesQuery : IRequest<IEnumerable<PaymentScheduleDto>>;

public class GetPaymentSchedulesQueryHandler : IRequestHandler<GetPaymentSchedulesQuery, IEnumerable<PaymentScheduleDto>>
{
    private readonly IPaymentSchedulesDbContext _context;

    public GetPaymentSchedulesQueryHandler(IPaymentSchedulesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PaymentScheduleDto>> Handle(GetPaymentSchedulesQuery request, CancellationToken cancellationToken)
    {
        var schedules = await _context.PaymentSchedules.AsNoTracking().ToListAsync(cancellationToken);
        return schedules.Select(s => s.ToDto());
    }
}
