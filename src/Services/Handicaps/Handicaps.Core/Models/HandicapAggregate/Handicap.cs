namespace Handicaps.Core.Models;

public class Handicap
{
    public Guid HandicapId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public decimal HandicapIndex { get; set; }
    public DateTime EffectiveDate { get; set; }
    public int RoundsConsidered { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
