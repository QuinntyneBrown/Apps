// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.Comparison;

public record DeleteComparisonCommand(Guid ComparisonId) : IRequest<Unit>;

public class DeleteComparisonCommandHandler : IRequestHandler<DeleteComparisonCommand, Unit>
{
    private readonly INutritionLabelScannerContext _context;

    public DeleteComparisonCommandHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteComparisonCommand request, CancellationToken cancellationToken)
    {
        var comparison = await _context.Comparisons
            .FirstOrDefaultAsync(x => x.ComparisonId == request.ComparisonId, cancellationToken)
            ?? throw new InvalidOperationException($"Comparison with ID {request.ComparisonId} not found.");

        _context.Comparisons.Remove(comparison);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
