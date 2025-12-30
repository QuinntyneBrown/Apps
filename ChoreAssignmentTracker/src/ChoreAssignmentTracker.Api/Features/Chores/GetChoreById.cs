// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Chores;

/// <summary>
/// Query to get a chore by ID.
/// </summary>
public class GetChoreById : IRequest<ChoreDto?>
{
    /// <summary>
    /// Gets or sets the chore ID.
    /// </summary>
    public Guid ChoreId { get; set; }
}

/// <summary>
/// Handler for GetChoreById query.
/// </summary>
public class GetChoreByIdHandler : IRequestHandler<GetChoreById, ChoreDto?>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetChoreByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetChoreByIdHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the GetChoreById query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The chore DTO or null if not found.</returns>
    public async Task<ChoreDto?> Handle(GetChoreById request, CancellationToken cancellationToken)
    {
        var chore = await _context.Chores
            .FirstOrDefaultAsync(c => c.ChoreId == request.ChoreId, cancellationToken);

        if (chore == null)
        {
            return null;
        }

        return new ChoreDto
        {
            ChoreId = chore.ChoreId,
            UserId = chore.UserId,
            Name = chore.Name,
            Description = chore.Description,
            Frequency = chore.Frequency,
            EstimatedMinutes = chore.EstimatedMinutes,
            Points = chore.Points,
            Category = chore.Category,
            IsActive = chore.IsActive,
            CreatedAt = chore.CreatedAt
        };
    }
}
