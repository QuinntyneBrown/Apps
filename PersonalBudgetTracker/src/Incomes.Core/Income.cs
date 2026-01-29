namespace Incomes.Core;

public class Income
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Source { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public bool IsRecurring { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
