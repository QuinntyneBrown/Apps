using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Businesses;

public record GetBusinessesQuery : IRequest<IEnumerable<BusinessDto>>
{
    public bool? IsActive { get; init; }
}

public class GetBusinessesQueryHandler : IRequestHandler<GetBusinessesQuery, IEnumerable<BusinessDto>>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<GetBusinessesQueryHandler> _logger;

    public GetBusinessesQueryHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<GetBusinessesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<BusinessDto>> Handle(GetBusinessesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting businesses");

        var query = _context.Businesses.AsNoTracking();

        if (request.IsActive.HasValue)
        {
            query = query.Where(b => b.IsActive == request.IsActive.Value);
        }

        var businesses = await query
            .OrderBy(b => b.Name)
            .ToListAsync(cancellationToken);

        return businesses.Select(b => b.ToDto());
    }
}
