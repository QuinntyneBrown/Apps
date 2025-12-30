// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.Comparison;

public record CreateComparisonCommand(
    Guid UserId,
    string Name,
    string ProductIds,
    string? Results,
    Guid? WinnerProductId) : IRequest<ComparisonDto>;

public class CreateComparisonCommandHandler : IRequestHandler<CreateComparisonCommand, ComparisonDto>
{
    private readonly INutritionLabelScannerContext _context;

    public CreateComparisonCommandHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<ComparisonDto> Handle(CreateComparisonCommand request, CancellationToken cancellationToken)
    {
        var comparison = new Core.Comparison
        {
            ComparisonId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            ProductIds = request.ProductIds,
            Results = request.Results,
            WinnerProductId = request.WinnerProductId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comparisons.Add(comparison);
        await _context.SaveChangesAsync(cancellationToken);

        return comparison.ToDto();
    }
}
