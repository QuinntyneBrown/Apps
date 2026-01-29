namespace Offers.Core.Models;

public class Offer
{
    public Guid OfferId { get; set; }
    public Guid LoanId { get; set; }
    public Guid TenantId { get; set; }
    public string LenderName { get; set; } = string.Empty;
    public decimal InterestRate { get; set; }
    public int TermMonths { get; set; }
    public decimal MonthlyPayment { get; set; }
    public decimal TotalCost { get; set; }
    public decimal OriginationFee { get; set; }
    public string? Notes { get; set; }
    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
}
