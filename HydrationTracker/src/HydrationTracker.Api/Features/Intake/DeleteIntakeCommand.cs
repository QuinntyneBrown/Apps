// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Intake;

public record DeleteIntakeCommand(Guid IntakeId) : IRequest<Unit>;

public class DeleteIntakeCommandHandler : IRequestHandler<DeleteIntakeCommand, Unit>
{
    private readonly IHydrationTrackerContext _context;

    public DeleteIntakeCommandHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteIntakeCommand request, CancellationToken cancellationToken)
    {
        var intake = await _context.Intakes
            .FirstOrDefaultAsync(x => x.IntakeId == request.IntakeId, cancellationToken)
            ?? throw new InvalidOperationException($"Intake with ID {request.IntakeId} not found.");

        _context.Intakes.Remove(intake);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
