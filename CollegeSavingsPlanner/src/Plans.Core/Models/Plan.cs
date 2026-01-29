namespace Plans.Core.Models;
public class Plan
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BeneficiaryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal TargetAmount { get; set; }
    public int TargetYear { get; set; }
    public decimal CurrentBalance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid? TenantId { get; set; }
}
