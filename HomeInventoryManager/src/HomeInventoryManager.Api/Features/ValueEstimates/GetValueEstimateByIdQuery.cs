using HomeInventoryManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeInventoryManager.Api.Features.ValueEstimates;

public record GetValueEstimateByIdQuery : IRequest<ValueEstimateDto?>
{
    public Guid ValueEstimateId { get; init; }
}

public class GetValueEstimateByIdQueryHandler : IRequestHandler<GetValueEstimateByIdQuery, ValueEstimateDto?>
{
    private readonly IHomeInventoryManagerContext _context;
    private readonly ILogger<GetValueEstimateByIdQueryHandler> _logger;

    public GetValueEstimateByIdQueryHandler(
        IHomeInventoryManagerContext context,
        ILogger<GetValueEstimateByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ValueEstimateDto?> Handle(GetValueEstimateByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting value estimate {ValueEstimateId}", request.ValueEstimateId);

        var valueEstimate = await _context.ValueEstimates
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.ValueEstimateId == request.ValueEstimateId, cancellationToken);

        return valueEstimate?.ToDto();
    }
}
