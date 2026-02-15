namespace Nutrition.Core.Models;

public class NutritionEntry
{
    public Guid NutritionEntryId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public int Calories { get; set; }
    public decimal Protein { get; set; }
    public decimal Carbohydrates { get; set; }
    public decimal Fat { get; set; }
    public decimal? Fiber { get; set; }
    public decimal? Sugar { get; set; }
    public DateTime TrackedDate { get; set; }
    public string? MealType { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
