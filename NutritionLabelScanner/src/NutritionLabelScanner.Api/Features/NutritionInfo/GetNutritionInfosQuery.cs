// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.NutritionInfo;

public record GetNutritionInfosQuery() : IRequest<List<NutritionInfoDto>>;

public class GetNutritionInfosQueryHandler : IRequestHandler<GetNutritionInfosQuery, List<NutritionInfoDto>>
{
    private readonly INutritionLabelScannerContext _context;

    public GetNutritionInfosQueryHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<List<NutritionInfoDto>> Handle(GetNutritionInfosQuery request, CancellationToken cancellationToken)
    {
        return await _context.NutritionInfos
            .AsNoTracking()
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
