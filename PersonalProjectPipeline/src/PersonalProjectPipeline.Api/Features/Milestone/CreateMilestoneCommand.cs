// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.Milestone;

/// <summary>
/// Command to create a new milestone.
/// </summary>
public record CreateMilestoneCommand : IRequest<MilestoneDto>
{
    public Guid ProjectId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime? TargetDate { get; init; }
}

/// <summary>
/// Handler for CreateMilestoneCommand.
/// </summary>
public class CreateMilestoneCommandHandler : IRequestHandler<CreateMilestoneCommand, MilestoneDto>
{
    private readonly IPersonalProjectPipelineContext _context;

    public CreateMilestoneCommandHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<MilestoneDto> Handle(CreateMilestoneCommand request, CancellationToken cancellationToken)
    {
        var milestone = new Core.Milestone
        {
            MilestoneId = Guid.NewGuid(),
            ProjectId = request.ProjectId,
            Name = request.Name,
            Description = request.Description,
            TargetDate = request.TargetDate,
            IsAchieved = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Milestones.Add(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        return milestone.ToDto();
    }
}
