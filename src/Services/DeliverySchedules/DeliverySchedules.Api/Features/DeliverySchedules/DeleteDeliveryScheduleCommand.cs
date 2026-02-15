using DeliverySchedules.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliverySchedules.Api.Features.DeliverySchedules;

public record DeleteDeliveryScheduleCommand : IRequest<bool>
{
    public Guid DeliveryScheduleId { get; init; }
}

public class DeleteDeliveryScheduleCommandHandler : IRequestHandler<DeleteDeliveryScheduleCommand, bool>
{
    private readonly IDeliverySchedulesDbContext _context;
    private readonly ILogger<DeleteDeliveryScheduleCommandHandler> _logger;

    public DeleteDeliveryScheduleCommandHandler(IDeliverySchedulesDbContext context, ILogger<DeleteDeliveryScheduleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteDeliveryScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _context.DeliverySchedules
            .FirstOrDefaultAsync(ds => ds.DeliveryScheduleId == request.DeliveryScheduleId, cancellationToken);

        if (schedule == null) return false;

        _context.DeliverySchedules.Remove(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Delivery schedule deleted: {DeliveryScheduleId}", request.DeliveryScheduleId);

        return true;
    }
}
