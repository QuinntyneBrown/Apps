// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.Comparison;

public record UpdateComparisonCommand(
    Guid ComparisonId,
    Guid UserId,
    string Name,
    string ProductIds,
    string? Results,
    Guid? WinnerProductId) : IRequest<ComparisonDto>;

public class UpdateComparisonCommandHandler : IRequestHandler<UpdateComparisonCommand, ComparisonDto>
{
    private readonly INutritionLabelScannerContext _context;

    public UpdateComparisonCommandHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<ComparisonDto> Handle(UpdateComparisonCommand request, CancellationToken cancellationToken)
    {
        var comparison = await _context.Comparisons
            .FirstOrDefaultAsync(x => x.ComparisonId == request.ComparisonId, cancellationToken)
            ?? throw new InvalidOperationException($"Comparison with ID {request.ComparisonId} not found.");

        comparison.UserId = request.UserId;
        comparison.Name = request.Name;
        comparison.ProductIds = request.ProductIds;
        comparison.Results = request.Results;
        comparison.WinnerProductId = request.WinnerProductId;

        await _context.SaveChangesAsync(cancellationToken);

        return comparison.ToDto();
    }
}
