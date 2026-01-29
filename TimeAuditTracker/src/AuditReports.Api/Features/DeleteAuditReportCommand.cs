using AuditReports.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuditReports.Api.Features;

public record DeleteAuditReportCommand(Guid ReportId) : IRequest<bool>;

public class DeleteAuditReportCommandHandler : IRequestHandler<DeleteAuditReportCommand, bool>
{
    private readonly IAuditReportsDbContext _context;

    public DeleteAuditReportCommandHandler(IAuditReportsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteAuditReportCommand request, CancellationToken cancellationToken)
    {
        var report = await _context.AuditReports
            .FirstOrDefaultAsync(r => r.ReportId == request.ReportId, cancellationToken);
        if (report == null) return false;

        _context.AuditReports.Remove(report);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
