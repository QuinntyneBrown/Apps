using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.WearableData;

public record DeleteWearableDataCommand : IRequest<bool>
{
    public Guid WearableDataId { get; init; }
}

public class DeleteWearableDataCommandHandler : IRequestHandler<DeleteWearableDataCommand, bool>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<DeleteWearableDataCommandHandler> _logger;

    public DeleteWearableDataCommandHandler(
        IPersonalHealthDashboardContext context,
        ILogger<DeleteWearableDataCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteWearableDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting wearable data {WearableDataId}", request.WearableDataId);

        var wearableData = await _context.WearableData
            .FirstOrDefaultAsync(w => w.WearableDataId == request.WearableDataId, cancellationToken);

        if (wearableData == null)
        {
            _logger.LogWarning("Wearable data {WearableDataId} not found", request.WearableDataId);
            return false;
        }

        _context.WearableData.Remove(wearableData);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted wearable data {WearableDataId}", request.WearableDataId);

        return true;
    }
}
