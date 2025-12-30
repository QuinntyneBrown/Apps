// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MealPrepPlanner.Core;

/// <summary>
/// Represents nutrition information for a recipe or meal.
/// </summary>
public class Nutrition
{
    /// <summary>
    /// Gets or sets the unique identifier for the nutrition record.
    /// </summary>
    public Guid NutritionId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this nutrition record.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the recipe ID this nutrition information belongs to.
    /// </summary>
    public Guid? RecipeId { get; set; }

    /// <summary>
    /// Gets or sets the calories per serving.
    /// </summary>
    public int Calories { get; set; }

    /// <summary>
    /// Gets or sets the protein in grams.
    /// </summary>
    public decimal Protein { get; set; }

    /// <summary>
    /// Gets or sets the carbohydrates in grams.
    /// </summary>
    public decimal Carbohydrates { get; set; }

    /// <summary>
    /// Gets or sets the fat in grams.
    /// </summary>
    public decimal Fat { get; set; }

    /// <summary>
    /// Gets or sets the fiber in grams.
    /// </summary>
    public decimal? Fiber { get; set; }

    /// <summary>
    /// Gets or sets the sugar in grams.
    /// </summary>
    public decimal? Sugar { get; set; }

    /// <summary>
    /// Gets or sets the sodium in milligrams.
    /// </summary>
    public decimal? Sodium { get; set; }

    /// <summary>
    /// Gets or sets optional additional nutrients (JSON format).
    /// </summary>
    public string? AdditionalNutrients { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calculates the percentage of calories from protein.
    /// </summary>
    /// <returns>The percentage from protein.</returns>
    public decimal GetProteinCaloriesPercentage()
    {
        if (Calories == 0) return 0;
        var proteinCalories = Protein * 4; // 4 calories per gram of protein
        return (proteinCalories / Calories) * 100;
    }

    /// <summary>
    /// Checks if the meal is high in protein (protein calories > 30%).
    /// </summary>
    /// <returns>True if high in protein; otherwise, false.</returns>
    public bool IsHighProtein()
    {
        return GetProteinCaloriesPercentage() > 30;
    }
}
