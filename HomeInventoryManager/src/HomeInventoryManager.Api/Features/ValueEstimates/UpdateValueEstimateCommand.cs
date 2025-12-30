using HomeInventoryManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeInventoryManager.Api.Features.ValueEstimates;

public record UpdateValueEstimateCommand : IRequest<ValueEstimateDto?>
{
    public Guid ValueEstimateId { get; init; }
    public decimal EstimatedValue { get; init; }
    public DateTime EstimationDate { get; init; }
    public string? Source { get; init; }
    public string? Notes { get; init; }
}

public class UpdateValueEstimateCommandHandler : IRequestHandler<UpdateValueEstimateCommand, ValueEstimateDto?>
{
    private readonly IHomeInventoryManagerContext _context;
    private readonly ILogger<UpdateValueEstimateCommandHandler> _logger;

    public UpdateValueEstimateCommandHandler(
        IHomeInventoryManagerContext context,
        ILogger<UpdateValueEstimateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ValueEstimateDto?> Handle(UpdateValueEstimateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating value estimate {ValueEstimateId}", request.ValueEstimateId);

        var valueEstimate = await _context.ValueEstimates
            .FirstOrDefaultAsync(v => v.ValueEstimateId == request.ValueEstimateId, cancellationToken);

        if (valueEstimate == null)
        {
            _logger.LogWarning("Value estimate {ValueEstimateId} not found", request.ValueEstimateId);
            return null;
        }

        valueEstimate.EstimatedValue = request.EstimatedValue;
        valueEstimate.EstimationDate = request.EstimationDate;
        valueEstimate.Source = request.Source;
        valueEstimate.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated value estimate {ValueEstimateId}", request.ValueEstimateId);

        return valueEstimate.ToDto();
    }
}
