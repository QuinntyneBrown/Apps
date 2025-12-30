// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.Milestone;

/// <summary>
/// Command to delete a milestone.
/// </summary>
public record DeleteMilestoneCommand : IRequest<Unit>
{
    public Guid MilestoneId { get; init; }
}

/// <summary>
/// Handler for DeleteMilestoneCommand.
/// </summary>
public class DeleteMilestoneCommandHandler : IRequestHandler<DeleteMilestoneCommand, Unit>
{
    private readonly IPersonalProjectPipelineContext _context;

    public DeleteMilestoneCommandHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Unit> Handle(DeleteMilestoneCommand request, CancellationToken cancellationToken)
    {
        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(m => m.MilestoneId == request.MilestoneId, cancellationToken)
            ?? throw new InvalidOperationException($"Milestone with ID {request.MilestoneId} not found.");

        _context.Milestones.Remove(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
