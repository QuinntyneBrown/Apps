using TimeAuditTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.AuditReports;

public record UpdateAuditReportCommand : IRequest<AuditReportDto?>
{
    public Guid AuditReportId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public double TotalTrackedHours { get; init; }
    public double ProductiveHours { get; init; }
    public string? Summary { get; init; }
    public string? Insights { get; init; }
    public string? Recommendations { get; init; }
}

public class UpdateAuditReportCommandHandler : IRequestHandler<UpdateAuditReportCommand, AuditReportDto?>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<UpdateAuditReportCommandHandler> _logger;

    public UpdateAuditReportCommandHandler(
        ITimeAuditTrackerContext context,
        ILogger<UpdateAuditReportCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AuditReportDto?> Handle(UpdateAuditReportCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating audit report {AuditReportId}", request.AuditReportId);

        var report = await _context.AuditReports
            .FirstOrDefaultAsync(r => r.AuditReportId == request.AuditReportId, cancellationToken);

        if (report == null)
        {
            _logger.LogWarning("Audit report {AuditReportId} not found", request.AuditReportId);
            return null;
        }

        report.Title = request.Title;
        report.StartDate = request.StartDate;
        report.EndDate = request.EndDate;
        report.TotalTrackedHours = request.TotalTrackedHours;
        report.ProductiveHours = request.ProductiveHours;
        report.Summary = request.Summary;
        report.Insights = request.Insights;
        report.Recommendations = request.Recommendations;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated audit report {AuditReportId}", request.AuditReportId);

        return report.ToDto();
    }
}
