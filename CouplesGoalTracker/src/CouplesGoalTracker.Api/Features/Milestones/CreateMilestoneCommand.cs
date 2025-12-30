// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;

namespace CouplesGoalTracker.Api.Features.Milestones;

/// <summary>
/// Command to create a new milestone.
/// </summary>
public class CreateMilestoneCommand : IRequest<MilestoneDto>
{
    /// <summary>
    /// Gets or sets the goal ID this milestone belongs to.
    /// </summary>
    public Guid GoalId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who is creating this milestone.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the milestone.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the milestone.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the target completion date.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets the sort order of this milestone.
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// Handler for CreateMilestoneCommand.
/// </summary>
public class CreateMilestoneCommandHandler : IRequestHandler<CreateMilestoneCommand, MilestoneDto>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMilestoneCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateMilestoneCommandHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<MilestoneDto> Handle(CreateMilestoneCommand request, CancellationToken cancellationToken)
    {
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            GoalId = request.GoalId,
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            TargetDate = request.TargetDate,
            SortOrder = request.SortOrder,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Milestones.Add(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        return MilestoneDto.FromMilestone(milestone);
    }
}
