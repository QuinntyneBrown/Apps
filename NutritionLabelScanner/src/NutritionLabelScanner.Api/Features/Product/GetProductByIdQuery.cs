// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.Product;

public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductDto>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly INutritionLabelScannerContext _context;

    public GetProductByIdQueryHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ProductId == request.ProductId, cancellationToken)
            ?? throw new InvalidOperationException($"Product with ID {request.ProductId} not found.");

        return product.ToDto();
    }
}
