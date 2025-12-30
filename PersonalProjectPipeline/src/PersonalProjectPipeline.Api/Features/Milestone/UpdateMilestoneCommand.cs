// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.Milestone;

/// <summary>
/// Command to update an existing milestone.
/// </summary>
public record UpdateMilestoneCommand : IRequest<MilestoneDto>
{
    public Guid MilestoneId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime? TargetDate { get; init; }
    public bool IsAchieved { get; init; }
}

/// <summary>
/// Handler for UpdateMilestoneCommand.
/// </summary>
public class UpdateMilestoneCommandHandler : IRequestHandler<UpdateMilestoneCommand, MilestoneDto>
{
    private readonly IPersonalProjectPipelineContext _context;

    public UpdateMilestoneCommandHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<MilestoneDto> Handle(UpdateMilestoneCommand request, CancellationToken cancellationToken)
    {
        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(m => m.MilestoneId == request.MilestoneId, cancellationToken)
            ?? throw new InvalidOperationException($"Milestone with ID {request.MilestoneId} not found.");

        milestone.Name = request.Name;
        milestone.Description = request.Description;
        milestone.TargetDate = request.TargetDate;

        if (request.IsAchieved && !milestone.IsAchieved)
        {
            milestone.Achieve();
        }
        else if (!request.IsAchieved && milestone.IsAchieved)
        {
            milestone.IsAchieved = false;
            milestone.AchievementDate = null;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return milestone.ToDto();
    }
}
