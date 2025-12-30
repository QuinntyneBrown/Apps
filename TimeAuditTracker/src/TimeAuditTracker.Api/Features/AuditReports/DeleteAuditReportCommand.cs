using TimeAuditTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.AuditReports;

public record DeleteAuditReportCommand : IRequest<bool>
{
    public Guid AuditReportId { get; init; }
}

public class DeleteAuditReportCommandHandler : IRequestHandler<DeleteAuditReportCommand, bool>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<DeleteAuditReportCommandHandler> _logger;

    public DeleteAuditReportCommandHandler(
        ITimeAuditTrackerContext context,
        ILogger<DeleteAuditReportCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAuditReportCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting audit report {AuditReportId}", request.AuditReportId);

        var report = await _context.AuditReports
            .FirstOrDefaultAsync(r => r.AuditReportId == request.AuditReportId, cancellationToken);

        if (report == null)
        {
            _logger.LogWarning("Audit report {AuditReportId} not found", request.AuditReportId);
            return false;
        }

        _context.AuditReports.Remove(report);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted audit report {AuditReportId}", request.AuditReportId);

        return true;
    }
}
