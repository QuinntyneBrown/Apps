using TimeAuditTracker.Core;

namespace TimeAuditTracker.Api.Features.AuditReports;

public record AuditReportDto
{
    public Guid AuditReportId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public double TotalTrackedHours { get; init; }
    public double ProductiveHours { get; init; }
    public string? Summary { get; init; }
    public string? Insights { get; init; }
    public string? Recommendations { get; init; }
    public DateTime CreatedAt { get; init; }
    public double ProductivityPercentage { get; init; }
    public int PeriodDays { get; init; }
}

public static class AuditReportExtensions
{
    public static AuditReportDto ToDto(this AuditReport report)
    {
        return new AuditReportDto
        {
            AuditReportId = report.AuditReportId,
            UserId = report.UserId,
            Title = report.Title,
            StartDate = report.StartDate,
            EndDate = report.EndDate,
            TotalTrackedHours = report.TotalTrackedHours,
            ProductiveHours = report.ProductiveHours,
            Summary = report.Summary,
            Insights = report.Insights,
            Recommendations = report.Recommendations,
            CreatedAt = report.CreatedAt,
            ProductivityPercentage = report.GetProductivityPercentage(),
            PeriodDays = report.GetPeriodDays(),
        };
    }
}
