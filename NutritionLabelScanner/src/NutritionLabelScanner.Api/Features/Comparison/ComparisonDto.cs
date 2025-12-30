// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;

namespace NutritionLabelScanner.Api.Features.Comparison;

public record ComparisonDto(
    Guid ComparisonId,
    Guid UserId,
    string Name,
    string ProductIds,
    string? Results,
    Guid? WinnerProductId,
    DateTime CreatedAt);

public static class ComparisonExtensions
{
    public static ComparisonDto ToDto(this Core.Comparison comparison)
    {
        return new ComparisonDto(
            comparison.ComparisonId,
            comparison.UserId,
            comparison.Name,
            comparison.ProductIds,
            comparison.Results,
            comparison.WinnerProductId,
            comparison.CreatedAt);
    }
}
