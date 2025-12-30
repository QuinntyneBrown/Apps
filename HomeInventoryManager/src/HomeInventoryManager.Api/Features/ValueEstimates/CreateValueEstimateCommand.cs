using HomeInventoryManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeInventoryManager.Api.Features.ValueEstimates;

public record CreateValueEstimateCommand : IRequest<ValueEstimateDto>
{
    public Guid ItemId { get; init; }
    public decimal EstimatedValue { get; init; }
    public DateTime EstimationDate { get; init; }
    public string? Source { get; init; }
    public string? Notes { get; init; }
}

public class CreateValueEstimateCommandHandler : IRequestHandler<CreateValueEstimateCommand, ValueEstimateDto>
{
    private readonly IHomeInventoryManagerContext _context;
    private readonly ILogger<CreateValueEstimateCommandHandler> _logger;

    public CreateValueEstimateCommandHandler(
        IHomeInventoryManagerContext context,
        ILogger<CreateValueEstimateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ValueEstimateDto> Handle(CreateValueEstimateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating value estimate for item {ItemId}, value: {EstimatedValue}",
            request.ItemId,
            request.EstimatedValue);

        var valueEstimate = new ValueEstimate
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = request.ItemId,
            EstimatedValue = request.EstimatedValue,
            EstimationDate = request.EstimationDate,
            Source = request.Source,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.ValueEstimates.Add(valueEstimate);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created value estimate {ValueEstimateId} for item {ItemId}",
            valueEstimate.ValueEstimateId,
            request.ItemId);

        return valueEstimate.ToDto();
    }
}
