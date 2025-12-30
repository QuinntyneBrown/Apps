using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.Compensations;

public record GetCompensationsQuery : IRequest<IEnumerable<CompensationDto>>
{
    public Guid? UserId { get; init; }
    public CompensationType? CompensationType { get; init; }
    public bool? ActiveOnly { get; init; }
}

public class GetCompensationsQueryHandler : IRequestHandler<GetCompensationsQuery, IEnumerable<CompensationDto>>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<GetCompensationsQueryHandler> _logger;

    public GetCompensationsQueryHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<GetCompensationsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CompensationDto>> Handle(GetCompensationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting compensations for user {UserId}", request.UserId);

        var query = _context.Compensations.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        if (request.CompensationType.HasValue)
        {
            query = query.Where(c => c.CompensationType == request.CompensationType.Value);
        }

        if (request.ActiveOnly == true)
        {
            var now = DateTime.UtcNow;
            query = query.Where(c => c.EffectiveDate <= now && (c.EndDate == null || c.EndDate > now));
        }

        var compensations = await query
            .OrderByDescending(c => c.EffectiveDate)
            .ToListAsync(cancellationToken);

        return compensations.Select(c => c.ToDto());
    }
}
