// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.Comparison;

public record GetComparisonByIdQuery(Guid ComparisonId) : IRequest<ComparisonDto>;

public class GetComparisonByIdQueryHandler : IRequestHandler<GetComparisonByIdQuery, ComparisonDto>
{
    private readonly INutritionLabelScannerContext _context;

    public GetComparisonByIdQueryHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<ComparisonDto> Handle(GetComparisonByIdQuery request, CancellationToken cancellationToken)
    {
        var comparison = await _context.Comparisons
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ComparisonId == request.ComparisonId, cancellationToken)
            ?? throw new InvalidOperationException($"Comparison with ID {request.ComparisonId} not found.");

        return comparison.ToDto();
    }
}
