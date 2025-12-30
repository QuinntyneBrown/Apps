using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.WearableData;

public record GetWearableDataQuery : IRequest<IEnumerable<WearableDataDto>>
{
    public Guid? UserId { get; init; }
    public string? DeviceName { get; init; }
    public string? DataType { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class GetWearableDataQueryHandler : IRequestHandler<GetWearableDataQuery, IEnumerable<WearableDataDto>>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<GetWearableDataQueryHandler> _logger;

    public GetWearableDataQueryHandler(
        IPersonalHealthDashboardContext context,
        ILogger<GetWearableDataQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<WearableDataDto>> Handle(GetWearableDataQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wearable data for user {UserId}", request.UserId);

        var query = _context.WearableData.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(w => w.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrEmpty(request.DeviceName))
        {
            query = query.Where(w => w.DeviceName == request.DeviceName);
        }

        if (!string.IsNullOrEmpty(request.DataType))
        {
            query = query.Where(w => w.DataType == request.DataType);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(w => w.RecordedAt >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(w => w.RecordedAt <= request.EndDate.Value);
        }

        var wearableData = await query
            .OrderByDescending(w => w.RecordedAt)
            .ToListAsync(cancellationToken);

        return wearableData.Select(w => w.ToDto());
    }
}
