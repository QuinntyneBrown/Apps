namespace Deductions.Core.Models;

public class Deduction
{
    public Guid DeductionId { get; set; }
    public Guid TenantId { get; set; }
    public Guid UserId { get; set; }
    public Guid TaxYearId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public DeductionCategory Category { get; set; }
    public string? Notes { get; set; }
    public bool HasReceipt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public void AttachReceipt() => HasReceipt = true;
}

public enum DeductionCategory
{
    Medical,
    Charitable,
    Business,
    Education,
    HomeOffice,
    Travel,
    Other
}
