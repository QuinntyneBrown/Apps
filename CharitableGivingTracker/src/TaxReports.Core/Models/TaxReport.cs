namespace TaxReports.Core.Models;

public class TaxReport
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int TaxYear { get; set; }
    public decimal TotalDonations { get; set; }
    public DateTime GeneratedAt { get; set; }
    public string? ReportData { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid? TenantId { get; set; }
}
