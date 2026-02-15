using AuditReports.Core.Models;

namespace AuditReports.Api.Features;

public record AuditReportDto
{
    public Guid ReportId { get; init; }
    public Guid UserId { get; init; }
    public Guid TenantId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int TotalMinutes { get; init; }
    public int ProductiveMinutes { get; init; }
    public string? Summary { get; init; }
    public DateTime GeneratedAt { get; init; }
}

public static class AuditReportDtoExtensions
{
    public static AuditReportDto ToDto(this AuditReport report) => new()
    {
        ReportId = report.ReportId,
        UserId = report.UserId,
        TenantId = report.TenantId,
        StartDate = report.StartDate,
        EndDate = report.EndDate,
        TotalMinutes = report.TotalMinutes,
        ProductiveMinutes = report.ProductiveMinutes,
        Summary = report.Summary,
        GeneratedAt = report.GeneratedAt
    };
}
