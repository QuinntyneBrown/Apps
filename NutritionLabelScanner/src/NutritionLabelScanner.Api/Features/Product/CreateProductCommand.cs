// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.Product;

public record CreateProductCommand(
    Guid UserId,
    string Name,
    string? Brand,
    string? Barcode,
    string? Category,
    string? ServingSize,
    DateTime ScannedAt,
    string? Notes) : IRequest<ProductDto>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly INutritionLabelScannerContext _context;

    public CreateProductCommandHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Core.Product
        {
            ProductId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Brand = request.Brand,
            Barcode = request.Barcode,
            Category = request.Category,
            ServingSize = request.ServingSize,
            ScannedAt = request.ScannedAt,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product.ToDto();
    }
}
