// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Accomplishments;

/// <summary>
/// Query to get an accomplishment by ID.
/// </summary>
public class GetAccomplishmentByIdQuery : IRequest<AccomplishmentDto?>
{
    public Guid AccomplishmentId { get; set; }
}

/// <summary>
/// Handler for GetAccomplishmentByIdQuery.
/// </summary>
public class GetAccomplishmentByIdQueryHandler : IRequestHandler<GetAccomplishmentByIdQuery, AccomplishmentDto?>
{
    private readonly IWeeklyReviewSystemContext _context;

    public GetAccomplishmentByIdQueryHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<AccomplishmentDto?> Handle(GetAccomplishmentByIdQuery request, CancellationToken cancellationToken)
    {
        var accomplishment = await _context.Accomplishments
            .FirstOrDefaultAsync(a => a.AccomplishmentId == request.AccomplishmentId, cancellationToken);

        return accomplishment?.ToDto();
    }
}
