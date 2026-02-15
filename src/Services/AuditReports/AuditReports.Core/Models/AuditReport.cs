namespace AuditReports.Core.Models;

public class AuditReport
{
    public Guid ReportId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalMinutes { get; set; }
    public int ProductiveMinutes { get; set; }
    public string? Summary { get; set; }
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}
