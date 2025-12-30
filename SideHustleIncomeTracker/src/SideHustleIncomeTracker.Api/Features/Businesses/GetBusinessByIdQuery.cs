using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Businesses;

public record GetBusinessByIdQuery : IRequest<BusinessDto?>
{
    public Guid BusinessId { get; init; }
}

public class GetBusinessByIdQueryHandler : IRequestHandler<GetBusinessByIdQuery, BusinessDto?>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<GetBusinessByIdQueryHandler> _logger;

    public GetBusinessByIdQueryHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<GetBusinessByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BusinessDto?> Handle(GetBusinessByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting business {BusinessId}", request.BusinessId);

        var business = await _context.Businesses
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.BusinessId == request.BusinessId, cancellationToken);

        return business?.ToDto();
    }
}
