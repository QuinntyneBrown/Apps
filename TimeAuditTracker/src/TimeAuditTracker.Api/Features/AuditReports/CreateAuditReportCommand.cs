using TimeAuditTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TimeAuditTracker.Api.Features.AuditReports;

public record CreateAuditReportCommand : IRequest<AuditReportDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public double TotalTrackedHours { get; init; }
    public double ProductiveHours { get; init; }
    public string? Summary { get; init; }
    public string? Insights { get; init; }
    public string? Recommendations { get; init; }
}

public class CreateAuditReportCommandHandler : IRequestHandler<CreateAuditReportCommand, AuditReportDto>
{
    private readonly ITimeAuditTrackerContext _context;
    private readonly ILogger<CreateAuditReportCommandHandler> _logger;

    public CreateAuditReportCommandHandler(
        ITimeAuditTrackerContext context,
        ILogger<CreateAuditReportCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AuditReportDto> Handle(CreateAuditReportCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating audit report for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalTrackedHours = request.TotalTrackedHours,
            ProductiveHours = request.ProductiveHours,
            Summary = request.Summary,
            Insights = request.Insights,
            Recommendations = request.Recommendations,
            CreatedAt = DateTime.UtcNow,
        };

        _context.AuditReports.Add(report);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created audit report {AuditReportId} for user {UserId}",
            report.AuditReportId,
            request.UserId);

        return report.ToDto();
    }
}
