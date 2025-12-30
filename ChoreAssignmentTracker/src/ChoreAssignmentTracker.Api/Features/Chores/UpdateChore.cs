// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Chores;

/// <summary>
/// Command to update a chore.
/// </summary>
public class UpdateChore : IRequest<ChoreDto?>
{
    /// <summary>
    /// Gets or sets the chore ID.
    /// </summary>
    public Guid ChoreId { get; set; }

    /// <summary>
    /// Gets or sets the chore data.
    /// </summary>
    public UpdateChoreDto Chore { get; set; } = null!;
}

/// <summary>
/// Handler for UpdateChore command.
/// </summary>
public class UpdateChoreHandler : IRequestHandler<UpdateChore, ChoreDto?>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateChoreHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateChoreHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the UpdateChore command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated chore DTO or null if not found.</returns>
    public async Task<ChoreDto?> Handle(UpdateChore request, CancellationToken cancellationToken)
    {
        var chore = await _context.Chores
            .FirstOrDefaultAsync(c => c.ChoreId == request.ChoreId, cancellationToken);

        if (chore == null)
        {
            return null;
        }

        chore.Name = request.Chore.Name;
        chore.Description = request.Chore.Description;
        chore.Frequency = request.Chore.Frequency;
        chore.EstimatedMinutes = request.Chore.EstimatedMinutes;
        chore.Points = request.Chore.Points;
        chore.Category = request.Chore.Category;
        chore.IsActive = request.Chore.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

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
