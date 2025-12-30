// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.Product;

public record GetProductsQuery() : IRequest<List<ProductDto>>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly INutritionLabelScannerContext _context;

    public GetProductsQueryHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .AsNoTracking()
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
