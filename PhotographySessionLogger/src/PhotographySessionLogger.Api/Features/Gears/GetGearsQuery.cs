using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Gears;

public record GetGearsQuery : IRequest<IEnumerable<GearDto>>
{
    public Guid? UserId { get; init; }
    public string? GearType { get; init; }
}

public class GetGearsQueryHandler : IRequestHandler<GetGearsQuery, IEnumerable<GearDto>>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<GetGearsQueryHandler> _logger;

    public GetGearsQueryHandler(
        IPhotographySessionLoggerContext context,
        ILogger<GetGearsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<GearDto>> Handle(GetGearsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting gears for user {UserId}", request.UserId);

        var query = _context.Gears.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(g => g.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.GearType))
        {
            query = query.Where(g => g.GearType == request.GearType);
        }

        var gears = await query
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync(cancellationToken);

        return gears.Select(g => g.ToDto());
    }
}
