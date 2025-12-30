// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.CompletionLogs.Queries;

/// <summary>
/// Query to get a completion log by ID.
/// </summary>
public class GetCompletionLogById : IRequest<CompletionLogDto?>
{
    public Guid CompletionLogId { get; set; }
}

/// <summary>
/// Handler for GetCompletionLogById query.
/// </summary>
public class GetCompletionLogByIdHandler : IRequestHandler<GetCompletionLogById, CompletionLogDto?>
{
    private readonly IMorningRoutineBuilderContext _context;

    public GetCompletionLogByIdHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<CompletionLogDto?> Handle(GetCompletionLogById request, CancellationToken cancellationToken)
    {
        var completionLog = await _context.CompletionLogs
            .FirstOrDefaultAsync(c => c.CompletionLogId == request.CompletionLogId, cancellationToken);

        if (completionLog == null)
        {
            return null;
        }

        return new CompletionLogDto
        {
            CompletionLogId = completionLog.CompletionLogId,
            RoutineId = completionLog.RoutineId,
            UserId = completionLog.UserId,
            CompletionDate = completionLog.CompletionDate,
            ActualStartTime = completionLog.ActualStartTime,
            ActualEndTime = completionLog.ActualEndTime,
            TasksCompleted = completionLog.TasksCompleted,
            TotalTasks = completionLog.TotalTasks,
            Notes = completionLog.Notes,
            MoodRating = completionLog.MoodRating,
            CreatedAt = completionLog.CreatedAt
        };
    }
}
