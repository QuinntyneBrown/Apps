// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Progresses;

/// <summary>
/// Query to get a progress entry by ID.
/// </summary>
public class GetProgressByIdQuery : IRequest<ProgressDto?>
{
    /// <summary>
    /// Gets or sets the progress ID.
    /// </summary>
    public Guid ProgressId { get; set; }
}

/// <summary>
/// Handler for GetProgressByIdQuery.
/// </summary>
public class GetProgressByIdQueryHandler : IRequestHandler<GetProgressByIdQuery, ProgressDto?>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProgressByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetProgressByIdQueryHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ProgressDto?> Handle(GetProgressByIdQuery request, CancellationToken cancellationToken)
    {
        var progress = await _context.Progresses
            .FirstOrDefaultAsync(p => p.ProgressId == request.ProgressId, cancellationToken);

        return progress == null ? null : ProgressDto.FromProgress(progress);
    }
}
