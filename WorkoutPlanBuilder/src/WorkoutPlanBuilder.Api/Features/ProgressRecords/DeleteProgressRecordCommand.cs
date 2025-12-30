// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.ProgressRecords;

/// <summary>
/// Command to delete a progress record.
/// </summary>
public class DeleteProgressRecordCommand : IRequest<bool>
{
    public Guid ProgressRecordId { get; set; }
}

/// <summary>
/// Handler for DeleteProgressRecordCommand.
/// </summary>
public class DeleteProgressRecordCommandHandler : IRequestHandler<DeleteProgressRecordCommand, bool>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public DeleteProgressRecordCommandHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProgressRecordCommand request, CancellationToken cancellationToken)
    {
        var progressRecord = await _context.ProgressRecords
            .FirstOrDefaultAsync(pr => pr.ProgressRecordId == request.ProgressRecordId, cancellationToken);

        if (progressRecord == null)
        {
            return false;
        }

        _context.ProgressRecords.Remove(progressRecord);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
