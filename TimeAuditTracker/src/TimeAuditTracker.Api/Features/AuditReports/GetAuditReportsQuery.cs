using TimeAuditTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.AuditReports;

public record GetAuditReportsQuery : IRequest<IEnumerable<AuditReportDto>>
{
    public Guid? UserId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class GetAuditReportsQueryHandler : IRequestHandler<GetAuditReportsQuery, IEnumerable<AuditReportDto>>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<GetAuditReportsQueryHandler> _logger;

    public GetAuditReportsQueryHandler(
        ITimeAuditTrackerContext context,
        ILogger<GetAuditReportsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<AuditReportDto>> Handle(GetAuditReportsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting audit reports for user {UserId}", request.UserId);

        var query = _context.AuditReports.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(r => r.StartDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(r => r.EndDate <= request.EndDate.Value);
        }

        var reports = await query
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        return reports.Select(r => r.ToDto());
    }
}
