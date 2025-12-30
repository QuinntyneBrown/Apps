// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Intake;

public record CreateIntakeCommand(
    Guid UserId,
    BeverageType BeverageType,
    decimal AmountMl,
    DateTime IntakeTime,
    string? Notes) : IRequest<IntakeDto>;

public class CreateIntakeCommandHandler : IRequestHandler<CreateIntakeCommand, IntakeDto>
{
    private readonly IHydrationTrackerContext _context;

    public CreateIntakeCommandHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<IntakeDto> Handle(CreateIntakeCommand request, CancellationToken cancellationToken)
    {
        var intake = new Core.Intake
        {
            IntakeId = Guid.NewGuid(),
            UserId = request.UserId,
            BeverageType = request.BeverageType,
            AmountMl = request.AmountMl,
            IntakeTime = request.IntakeTime,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Intakes.Add(intake);
        await _context.SaveChangesAsync(cancellationToken);

        return intake.ToDto();
    }
}
