// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Intake;

public record UpdateIntakeCommand(
    Guid IntakeId,
    Guid UserId,
    BeverageType BeverageType,
    decimal AmountMl,
    DateTime IntakeTime,
    string? Notes) : IRequest<IntakeDto>;

public class UpdateIntakeCommandHandler : IRequestHandler<UpdateIntakeCommand, IntakeDto>
{
    private readonly IHydrationTrackerContext _context;

    public UpdateIntakeCommandHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<IntakeDto> Handle(UpdateIntakeCommand request, CancellationToken cancellationToken)
    {
        var intake = await _context.Intakes
            .FirstOrDefaultAsync(x => x.IntakeId == request.IntakeId, cancellationToken)
            ?? throw new InvalidOperationException($"Intake with ID {request.IntakeId} not found.");

        intake.UserId = request.UserId;
        intake.BeverageType = request.BeverageType;
        intake.AmountMl = request.AmountMl;
        intake.IntakeTime = request.IntakeTime;
        intake.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return intake.ToDto();
    }
}
