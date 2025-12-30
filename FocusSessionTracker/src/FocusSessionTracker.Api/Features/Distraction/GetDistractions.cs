// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Features.Distraction;

/// <summary>
/// Query to get distractions.
/// </summary>
public class GetDistractionsQuery : IRequest<List<DistractionDto>>
{
    /// <summary>
    /// Gets or sets the optional session ID filter.
    /// </summary>
    public Guid? FocusSessionId { get; set; }
}

/// <summary>
/// Handler for getting distractions.
/// </summary>
public class GetDistractionsQueryHandler : IRequestHandler<GetDistractionsQuery, List<DistractionDto>>
{
    private readonly IFocusSessionTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDistractionsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetDistractionsQueryHandler(IFocusSessionTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<DistractionDto>> Handle(GetDistractionsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Distractions.AsQueryable();

        if (request.FocusSessionId.HasValue)
        {
            query = query.Where(d => d.FocusSessionId == request.FocusSessionId.Value);
        }

        var distractions = await query
            .OrderByDescending(d => d.OccurredAt)
            .ToListAsync(cancellationToken);

        return distractions.Select(d => new DistractionDto
        {
            DistractionId = d.DistractionId,
            FocusSessionId = d.FocusSessionId,
            Type = d.Type,
            Description = d.Description,
            OccurredAt = d.OccurredAt,
            DurationMinutes = d.DurationMinutes,
            IsInternal = d.IsInternal,
            CreatedAt = d.CreatedAt
        }).ToList();
    }
}
