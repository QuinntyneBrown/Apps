using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.Vitals;

public record DeleteVitalCommand : IRequest<bool>
{
    public Guid VitalId { get; init; }
}

public class DeleteVitalCommandHandler : IRequestHandler<DeleteVitalCommand, bool>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<DeleteVitalCommandHandler> _logger;

    public DeleteVitalCommandHandler(
        IPersonalHealthDashboardContext context,
        ILogger<DeleteVitalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteVitalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting vital {VitalId}", request.VitalId);

        var vital = await _context.Vitals
            .FirstOrDefaultAsync(v => v.VitalId == request.VitalId, cancellationToken);

        if (vital == null)
        {
            _logger.LogWarning("Vital {VitalId} not found", request.VitalId);
            return false;
        }

        _context.Vitals.Remove(vital);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted vital {VitalId}", request.VitalId);

        return true;
    }
}
