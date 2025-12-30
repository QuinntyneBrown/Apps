// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;

namespace ChoreAssignmentTracker.Api.Features.Chores;

/// <summary>
/// Command to create a chore.
/// </summary>
public class CreateChore : IRequest<ChoreDto>
{
    /// <summary>
    /// Gets or sets the chore data.
    /// </summary>
    public CreateChoreDto Chore { get; set; } = null!;
}

/// <summary>
/// Handler for CreateChore command.
/// </summary>
public class CreateChoreHandler : IRequestHandler<CreateChore, ChoreDto>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateChoreHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateChoreHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the CreateChore command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created chore DTO.</returns>
    public async Task<ChoreDto> Handle(CreateChore request, CancellationToken cancellationToken)
    {
        var chore = new Chore
        {
            ChoreId = Guid.NewGuid(),
            UserId = request.Chore.UserId,
            Name = request.Chore.Name,
            Description = request.Chore.Description,
            Frequency = request.Chore.Frequency,
            EstimatedMinutes = request.Chore.EstimatedMinutes,
            Points = request.Chore.Points,
            Category = request.Chore.Category,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Chores.Add(chore);
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
