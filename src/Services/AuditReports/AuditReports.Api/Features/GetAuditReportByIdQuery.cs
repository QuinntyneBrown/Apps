using AuditReports.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuditReports.Api.Features;

public record GetAuditReportByIdQuery(Guid ReportId) : IRequest<AuditReportDto?>;

public class GetAuditReportByIdQueryHandler : IRequestHandler<GetAuditReportByIdQuery, AuditReportDto?>
{
    private readonly IAuditReportsDbContext _context;

    public GetAuditReportByIdQueryHandler(IAuditReportsDbContext context)
    {
        _context = context;
    }

    public async Task<AuditReportDto?> Handle(GetAuditReportByIdQuery request, CancellationToken cancellationToken)
    {
        var report = await _context.AuditReports
            .FirstOrDefaultAsync(r => r.ReportId == request.ReportId, cancellationToken);
        return report?.ToDto();
    }
}
