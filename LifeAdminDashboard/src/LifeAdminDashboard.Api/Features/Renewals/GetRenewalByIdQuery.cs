using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.Renewals;

public record GetRenewalByIdQuery : IRequest<RenewalDto?>
{
    public Guid RenewalId { get; init; }
}

public class GetRenewalByIdQueryHandler : IRequestHandler<GetRenewalByIdQuery, RenewalDto?>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<GetRenewalByIdQueryHandler> _logger;

    public GetRenewalByIdQueryHandler(
        ILifeAdminDashboardContext context,
        ILogger<GetRenewalByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RenewalDto?> Handle(GetRenewalByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting renewal {RenewalId}", request.RenewalId);

        var renewal = await _context.Renewals
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RenewalId == request.RenewalId, cancellationToken);

        return renewal?.ToDto();
    }
}
