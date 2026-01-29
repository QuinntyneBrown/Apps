namespace TaxYears.Core.Models;

public class TaxYear
{
    public Guid TaxYearId { get; set; }
    public Guid TenantId { get; set; }
    public Guid UserId { get; set; }
    public int Year { get; set; }
    public bool IsClosed { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public void Close() => IsClosed = true;
    public void Reopen() => IsClosed = false;
}
