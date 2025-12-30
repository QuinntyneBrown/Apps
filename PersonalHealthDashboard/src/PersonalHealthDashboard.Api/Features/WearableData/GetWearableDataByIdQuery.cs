using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.WearableData;

public record GetWearableDataByIdQuery : IRequest<WearableDataDto?>
{
    public Guid WearableDataId { get; init; }
}

public class GetWearableDataByIdQueryHandler : IRequestHandler<GetWearableDataByIdQuery, WearableDataDto?>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<GetWearableDataByIdQueryHandler> _logger;

    public GetWearableDataByIdQueryHandler(
        IPersonalHealthDashboardContext context,
        ILogger<GetWearableDataByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WearableDataDto?> Handle(GetWearableDataByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wearable data {WearableDataId}", request.WearableDataId);

        var wearableData = await _context.WearableData
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.WearableDataId == request.WearableDataId, cancellationToken);

        if (wearableData == null)
        {
            _logger.LogWarning("Wearable data {WearableDataId} not found", request.WearableDataId);
            return null;
        }

        return wearableData.ToDto();
    }
}
