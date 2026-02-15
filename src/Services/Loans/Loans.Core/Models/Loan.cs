namespace Loans.Core.Models;

public class Loan
{
    public Guid LoanId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LoanType { get; set; } = string.Empty;
    public decimal RequestedAmount { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public int CreditScore { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
