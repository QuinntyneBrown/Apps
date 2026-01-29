namespace PaymentSchedules.Core.Models;

public class PaymentSchedule
{
    public Guid ScheduleId { get; set; }
    public Guid OfferId { get; set; }
    public Guid TenantId { get; set; }
    public int PaymentNumber { get; set; }
    public DateTime DueDate { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal TotalPayment { get; set; }
    public decimal RemainingBalance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
