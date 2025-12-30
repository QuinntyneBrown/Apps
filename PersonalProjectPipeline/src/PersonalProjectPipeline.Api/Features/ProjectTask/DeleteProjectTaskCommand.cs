// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.ProjectTask;

/// <summary>
/// Command to delete a project task.
/// </summary>
public record DeleteProjectTaskCommand : IRequest<Unit>
{
    public Guid ProjectTaskId { get; init; }
}

/// <summary>
/// Handler for DeleteProjectTaskCommand.
/// </summary>
public class DeleteProjectTaskCommandHandler : IRequestHandler<DeleteProjectTaskCommand, Unit>
{
    private readonly IPersonalProjectPipelineContext _context;

    public DeleteProjectTaskCommandHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Unit> Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.ProjectTaskId == request.ProjectTaskId, cancellationToken)
            ?? throw new InvalidOperationException($"Task with ID {request.ProjectTaskId} not found.");

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
