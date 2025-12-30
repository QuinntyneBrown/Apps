// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.FillUps.Commands;

/// <summary>
/// Command to delete a fill-up.
/// </summary>
public class DeleteFillUp : IRequest<Unit>
{
    public Guid FillUpId { get; set; }
}

/// <summary>
/// Handler for DeleteFillUp command.
/// </summary>
public class DeleteFillUpHandler : IRequestHandler<DeleteFillUp, Unit>
{
    private readonly IFuelEconomyTrackerContext _context;

    public DeleteFillUpHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteFillUp request, CancellationToken cancellationToken)
    {
        var fillUp = await _context.FillUps
            .FirstOrDefaultAsync(f => f.FillUpId == request.FillUpId, cancellationToken);

        if (fillUp == null)
        {
            throw new KeyNotFoundException($"FillUp with ID {request.FillUpId} not found.");
        }

        _context.FillUps.Remove(fillUp);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
