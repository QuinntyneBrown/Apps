using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.Renewals;

public record GetRenewalsQuery : IRequest<IEnumerable<RenewalDto>>
{
    public Guid? UserId { get; init; }
    public string? RenewalType { get; init; }
    public bool? IsActive { get; init; }
    public bool? IsDueSoon { get; init; }
}

public class GetRenewalsQueryHandler : IRequestHandler<GetRenewalsQuery, IEnumerable<RenewalDto>>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<GetRenewalsQueryHandler> _logger;

    public GetRenewalsQueryHandler(
        ILifeAdminDashboardContext context,
        ILogger<GetRenewalsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<RenewalDto>> Handle(GetRenewalsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting renewals for user {UserId}", request.UserId);

        var query = _context.Renewals.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.RenewalType))
        {
            query = query.Where(r => r.RenewalType == request.RenewalType);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(r => r.IsActive == request.IsActive.Value);
        }

        var renewals = await query
            .OrderBy(r => r.RenewalDate)
            .ToListAsync(cancellationToken);

        if (request.IsDueSoon == true)
        {
            renewals = renewals.Where(r => r.IsDueSoon()).ToList();
        }

        return renewals.Select(r => r.ToDto());
    }
}
