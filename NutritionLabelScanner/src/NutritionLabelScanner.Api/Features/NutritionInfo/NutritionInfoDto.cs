// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;

namespace NutritionLabelScanner.Api.Features.NutritionInfo;

public record NutritionInfoDto(
    Guid NutritionInfoId,
    Guid ProductId,
    int Calories,
    decimal TotalFat,
    decimal? SaturatedFat,
    decimal? TransFat,
    decimal? Cholesterol,
    decimal Sodium,
    decimal TotalCarbohydrates,
    decimal? DietaryFiber,
    decimal? TotalSugars,
    decimal Protein,
    string? AdditionalNutrients,
    DateTime CreatedAt);

public static class NutritionInfoExtensions
{
    public static NutritionInfoDto ToDto(this Core.NutritionInfo nutritionInfo)
    {
        return new NutritionInfoDto(
            nutritionInfo.NutritionInfoId,
            nutritionInfo.ProductId,
            nutritionInfo.Calories,
            nutritionInfo.TotalFat,
            nutritionInfo.SaturatedFat,
            nutritionInfo.TransFat,
            nutritionInfo.Cholesterol,
            nutritionInfo.Sodium,
            nutritionInfo.TotalCarbohydrates,
            nutritionInfo.DietaryFiber,
            nutritionInfo.TotalSugars,
            nutritionInfo.Protein,
            nutritionInfo.AdditionalNutrients,
            nutritionInfo.CreatedAt);
    }
}
