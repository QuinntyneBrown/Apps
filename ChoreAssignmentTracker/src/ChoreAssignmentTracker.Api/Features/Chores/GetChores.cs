// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Chores;

/// <summary>
/// Query to get all chores.
/// </summary>
public class GetChores : IRequest<List<ChoreDto>>
{
    /// <summary>
    /// Gets or sets the user ID to filter by.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include only active chores.
    /// </summary>
    public bool? IsActive { get; set; }
}

/// <summary>
/// Handler for GetChores query.
/// </summary>
public class GetChoresHandler : IRequestHandler<GetChores, List<ChoreDto>>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetChoresHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetChoresHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the GetChores query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of chore DTOs.</returns>
    public async Task<List<ChoreDto>> Handle(GetChores request, CancellationToken cancellationToken)
    {
        var query = _context.Chores.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(c => c.IsActive == request.IsActive.Value);
        }

        var chores = await query.ToListAsync(cancellationToken);

        return chores.Select(c => new ChoreDto
        {
            ChoreId = c.ChoreId,
            UserId = c.UserId,
            Name = c.Name,
            Description = c.Description,
            Frequency = c.Frequency,
            EstimatedMinutes = c.EstimatedMinutes,
            Points = c.Points,
            Category = c.Category,
            IsActive = c.IsActive,
            CreatedAt = c.CreatedAt
        }).ToList();
    }
}
