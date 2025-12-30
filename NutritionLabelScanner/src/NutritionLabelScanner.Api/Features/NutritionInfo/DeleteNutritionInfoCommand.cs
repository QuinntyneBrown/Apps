// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.NutritionInfo;

public record DeleteNutritionInfoCommand(Guid NutritionInfoId) : IRequest<Unit>;

public class DeleteNutritionInfoCommandHandler : IRequestHandler<DeleteNutritionInfoCommand, Unit>
{
    private readonly INutritionLabelScannerContext _context;

    public DeleteNutritionInfoCommandHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteNutritionInfoCommand request, CancellationToken cancellationToken)
    {
        var nutritionInfo = await _context.NutritionInfos
            .FirstOrDefaultAsync(x => x.NutritionInfoId == request.NutritionInfoId, cancellationToken)
            ?? throw new InvalidOperationException($"NutritionInfo with ID {request.NutritionInfoId} not found.");

        _context.NutritionInfos.Remove(nutritionInfo);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
