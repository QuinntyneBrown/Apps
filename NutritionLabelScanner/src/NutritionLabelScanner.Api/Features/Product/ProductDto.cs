// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;

namespace NutritionLabelScanner.Api.Features.Product;

public record ProductDto(
    Guid ProductId,
    Guid UserId,
    string Name,
    string? Brand,
    string? Barcode,
    string? Category,
    string? ServingSize,
    DateTime ScannedAt,
    string? Notes,
    DateTime CreatedAt);

public static class ProductExtensions
{
    public static ProductDto ToDto(this Core.Product product)
    {
        return new ProductDto(
            product.ProductId,
            product.UserId,
            product.Name,
            product.Brand,
            product.Barcode,
            product.Category,
            product.ServingSize,
            product.ScannedAt,
            product.Notes,
            product.CreatedAt);
    }
}
