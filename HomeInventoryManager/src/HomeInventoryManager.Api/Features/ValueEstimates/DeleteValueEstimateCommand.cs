using HomeInventoryManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeInventoryManager.Api.Features.ValueEstimates;

public record DeleteValueEstimateCommand : IRequest<bool>
{
    public Guid ValueEstimateId { get; init; }
}

public class DeleteValueEstimateCommandHandler : IRequestHandler<DeleteValueEstimateCommand, bool>
{
    private readonly IHomeInventoryManagerContext _context;
    private readonly ILogger<DeleteValueEstimateCommandHandler> _logger;

    public DeleteValueEstimateCommandHandler(
        IHomeInventoryManagerContext context,
        ILogger<DeleteValueEstimateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteValueEstimateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting value estimate {ValueEstimateId}", request.ValueEstimateId);

        var valueEstimate = await _context.ValueEstimates
            .FirstOrDefaultAsync(v => v.ValueEstimateId == request.ValueEstimateId, cancellationToken);

        if (valueEstimate == null)
        {
            _logger.LogWarning("Value estimate {ValueEstimateId} not found", request.ValueEstimateId);
            return false;
        }

        _context.ValueEstimates.Remove(valueEstimate);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted value estimate {ValueEstimateId}", request.ValueEstimateId);

        return true;
    }
}
