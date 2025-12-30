// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.CompletionLogs.Commands;

/// <summary>
/// Command to delete a completion log.
/// </summary>
public class DeleteCompletionLog : IRequest
{
    public Guid CompletionLogId { get; set; }
}

/// <summary>
/// Handler for DeleteCompletionLog command.
/// </summary>
public class DeleteCompletionLogHandler : IRequestHandler<DeleteCompletionLog>
{
    private readonly IMorningRoutineBuilderContext _context;

    public DeleteCompletionLogHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCompletionLog request, CancellationToken cancellationToken)
    {
        var completionLog = await _context.CompletionLogs
            .FirstOrDefaultAsync(c => c.CompletionLogId == request.CompletionLogId, cancellationToken);

        if (completionLog == null)
        {
            throw new KeyNotFoundException($"CompletionLog with ID {request.CompletionLogId} not found.");
        }

        _context.CompletionLogs.Remove(completionLog);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
