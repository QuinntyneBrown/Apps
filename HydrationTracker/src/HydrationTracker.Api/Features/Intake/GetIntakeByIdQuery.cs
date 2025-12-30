// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Intake;

public record GetIntakeByIdQuery(Guid IntakeId) : IRequest<IntakeDto>;

public class GetIntakeByIdQueryHandler : IRequestHandler<GetIntakeByIdQuery, IntakeDto>
{
    private readonly IHydrationTrackerContext _context;

    public GetIntakeByIdQueryHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<IntakeDto> Handle(GetIntakeByIdQuery request, CancellationToken cancellationToken)
    {
        var intake = await _context.Intakes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IntakeId == request.IntakeId, cancellationToken)
            ?? throw new InvalidOperationException($"Intake with ID {request.IntakeId} not found.");

        return intake.ToDto();
    }
}
