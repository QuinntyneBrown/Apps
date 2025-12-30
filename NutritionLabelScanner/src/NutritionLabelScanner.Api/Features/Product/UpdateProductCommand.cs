// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.Product;

public record UpdateProductCommand(
    Guid ProductId,
    Guid UserId,
    string Name,
    string? Brand,
    string? Barcode,
    string? Category,
    string? ServingSize,
    DateTime ScannedAt,
    string? Notes) : IRequest<ProductDto>;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly INutritionLabelScannerContext _context;

    public UpdateProductCommandHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.ProductId == request.ProductId, cancellationToken)
            ?? throw new InvalidOperationException($"Product with ID {request.ProductId} not found.");

        product.UserId = request.UserId;
        product.Name = request.Name;
        product.Brand = request.Brand;
        product.Barcode = request.Barcode;
        product.Category = request.Category;
        product.ServingSize = request.ServingSize;
        product.ScannedAt = request.ScannedAt;
        product.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return product.ToDto();
    }
}
