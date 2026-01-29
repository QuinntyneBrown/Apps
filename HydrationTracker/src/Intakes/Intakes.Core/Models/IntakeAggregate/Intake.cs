namespace Intakes.Core.Models;

public class Intake
{
    public Guid IntakeId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public BeverageType BeverageType { get; set; }
    public decimal AmountMl { get; set; }
    public DateTime LoggedAt { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public decimal GetAmountInOz() => AmountMl * 0.033814m;
}

public enum BeverageType
{
    Water,
    Coffee,
    Tea,
    Juice,
    Soda,
    Milk,
    Sports,
    Other
}
