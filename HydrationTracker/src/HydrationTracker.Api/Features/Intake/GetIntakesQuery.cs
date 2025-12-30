// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Intake;

public record GetIntakesQuery() : IRequest<List<IntakeDto>>;

public class GetIntakesQueryHandler : IRequestHandler<GetIntakesQuery, List<IntakeDto>>
{
    private readonly IHydrationTrackerContext _context;

    public GetIntakesQueryHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<IntakeDto>> Handle(GetIntakesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Intakes
            .AsNoTracking()
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
