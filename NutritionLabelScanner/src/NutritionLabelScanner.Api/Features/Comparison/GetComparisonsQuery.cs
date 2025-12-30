// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.Comparison;

public record GetComparisonsQuery() : IRequest<List<ComparisonDto>>;

public class GetComparisonsQueryHandler : IRequestHandler<GetComparisonsQuery, List<ComparisonDto>>
{
    private readonly INutritionLabelScannerContext _context;

    public GetComparisonsQueryHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<List<ComparisonDto>> Handle(GetComparisonsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Comparisons
            .AsNoTracking()
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
