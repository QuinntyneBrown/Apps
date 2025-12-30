using TimeAuditTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.AuditReports;

public record GetAuditReportByIdQuery : IRequest<AuditReportDto?>
{
    public Guid AuditReportId { get; init; }
}

public class GetAuditReportByIdQueryHandler : IRequestHandler<GetAuditReportByIdQuery, AuditReportDto?>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<GetAuditReportByIdQueryHandler> _logger;

    public GetAuditReportByIdQueryHandler(
        ITimeAuditTrackerContext context,
        ILogger<GetAuditReportByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AuditReportDto?> Handle(GetAuditReportByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting audit report {AuditReportId}", request.AuditReportId);

        var report = await _context.AuditReports
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.AuditReportId == request.AuditReportId, cancellationToken);

        if (report == null)
        {
            _logger.LogWarning("Audit report {AuditReportId} not found", request.AuditReportId);
            return null;
        }

        return report.ToDto();
    }
}
