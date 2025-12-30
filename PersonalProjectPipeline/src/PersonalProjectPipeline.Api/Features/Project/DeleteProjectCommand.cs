// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.Project;

/// <summary>
/// Command to delete a project.
/// </summary>
public record DeleteProjectCommand : IRequest<Unit>
{
    public Guid ProjectId { get; init; }
}

/// <summary>
/// Handler for DeleteProjectCommand.
/// </summary>
public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
{
    private readonly IPersonalProjectPipelineContext _context;

    public DeleteProjectCommandHandler(IPersonalProjectPipelineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken)
            ?? throw new InvalidOperationException($"Project with ID {request.ProjectId} not found.");

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
