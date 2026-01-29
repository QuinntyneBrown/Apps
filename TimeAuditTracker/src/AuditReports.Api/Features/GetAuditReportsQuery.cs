using AuditReports.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuditReports.Api.Features;

public record GetAuditReportsQuery : IRequest<IEnumerable<AuditReportDto>>;

public class GetAuditReportsQueryHandler : IRequestHandler<GetAuditReportsQuery, IEnumerable<AuditReportDto>>
{
    private readonly IAuditReportsDbContext _context;

    public GetAuditReportsQueryHandler(IAuditReportsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuditReportDto>> Handle(GetAuditReportsQuery request, CancellationToken cancellationToken)
    {
        var reports = await _context.AuditReports.ToListAsync(cancellationToken);
        return reports.Select(r => r.ToDto());
    }
}
