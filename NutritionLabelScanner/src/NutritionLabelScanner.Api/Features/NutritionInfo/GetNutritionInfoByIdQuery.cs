// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.NutritionInfo;

public record GetNutritionInfoByIdQuery(Guid NutritionInfoId) : IRequest<NutritionInfoDto>;

public class GetNutritionInfoByIdQueryHandler : IRequestHandler<GetNutritionInfoByIdQuery, NutritionInfoDto>
{
    private readonly INutritionLabelScannerContext _context;

    public GetNutritionInfoByIdQueryHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<NutritionInfoDto> Handle(GetNutritionInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var nutritionInfo = await _context.NutritionInfos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.NutritionInfoId == request.NutritionInfoId, cancellationToken)
            ?? throw new InvalidOperationException($"NutritionInfo with ID {request.NutritionInfoId} not found.");

        return nutritionInfo.ToDto();
    }
}
