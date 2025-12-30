// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.Mortgage;

/// <summary>
/// Command to delete a mortgage.
/// </summary>
public record DeleteMortgageCommand : IRequest<Unit>
{
    public Guid MortgageId { get; set; }
}

/// <summary>
/// Handler for DeleteMortgageCommand.
/// </summary>
public class DeleteMortgageCommandHandler : IRequestHandler<DeleteMortgageCommand, Unit>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public DeleteMortgageCommandHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteMortgageCommand request, CancellationToken cancellationToken)
    {
        var mortgage = await _context.Mortgages
            .FirstOrDefaultAsync(m => m.MortgageId == request.MortgageId, cancellationToken);

        if (mortgage == null)
        {
            throw new Exception($"Mortgage with ID {request.MortgageId} not found.");
        }

        _context.Mortgages.Remove(mortgage);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
