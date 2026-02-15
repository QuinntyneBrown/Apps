using PaymentSchedules.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PaymentSchedules.Api.Features;

public record DeletePaymentScheduleCommand(Guid ScheduleId) : IRequest<bool>;

public class DeletePaymentScheduleCommandHandler : IRequestHandler<DeletePaymentScheduleCommand, bool>
{
    private readonly IPaymentSchedulesDbContext _context;
    private readonly ILogger<DeletePaymentScheduleCommandHandler> _logger;

    public DeletePaymentScheduleCommandHandler(IPaymentSchedulesDbContext context, ILogger<DeletePaymentScheduleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePaymentScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _context.PaymentSchedules.FirstOrDefaultAsync(s => s.ScheduleId == request.ScheduleId, cancellationToken);
        if (schedule == null) return false;

        _context.PaymentSchedules.Remove(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("PaymentSchedule deleted: {ScheduleId}", request.ScheduleId);

        return true;
    }
}
