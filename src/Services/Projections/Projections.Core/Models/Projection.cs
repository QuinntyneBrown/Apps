namespace Projections.Core.Models;
public class Projection
{
    public Guid Id { get; set; }
    public Guid PlanId { get; set; }
    public int ProjectionYear { get; set; }
    public decimal ProjectedAmount { get; set; }
    public decimal AssumedReturnRate { get; set; }
    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
    public Guid? TenantId { get; set; }
}
