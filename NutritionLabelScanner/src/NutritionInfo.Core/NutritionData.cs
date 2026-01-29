namespace NutritionInfo.Core;

public class NutritionData
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Calories { get; set; }
    public decimal TotalFat { get; set; }
    public decimal SaturatedFat { get; set; }
    public decimal Carbohydrates { get; set; }
    public decimal Sugars { get; set; }
    public decimal Protein { get; set; }
    public decimal Sodium { get; set; }
    public decimal Fiber { get; set; }
    public DateTime ExtractedAt { get; set; } = DateTime.UtcNow;
}
