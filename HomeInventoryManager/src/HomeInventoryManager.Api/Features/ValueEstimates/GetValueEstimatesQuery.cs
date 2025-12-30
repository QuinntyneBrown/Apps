using HomeInventoryManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeInventoryManager.Api.Features.ValueEstimates;

public record GetValueEstimatesQuery : IRequest<IEnumerable<ValueEstimateDto>>
{
    public Guid? ItemId { get; init; }
}

public class GetValueEstimatesQueryHandler : IRequestHandler<GetValueEstimatesQuery, IEnumerable<ValueEstimateDto>>
{
    private readonly IHomeInventoryManagerContext _context;
    private readonly ILogger<GetValueEstimatesQueryHandler> _logger;

    public GetValueEstimatesQueryHandler(
        IHomeInventoryManagerContext context,
        ILogger<GetValueEstimatesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ValueEstimateDto>> Handle(GetValueEstimatesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting value estimates for item {ItemId}", request.ItemId);

        var query = _context.ValueEstimates.AsNoTracking();

        if (request.ItemId.HasValue)
        {
            query = query.Where(v => v.ItemId == request.ItemId.Value);
        }

        var valueEstimates = await query
            .OrderByDescending(v => v.EstimationDate)
            .ToListAsync(cancellationToken);

        return valueEstimates.Select(v => v.ToDto());
    }
}
