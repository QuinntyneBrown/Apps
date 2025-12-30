// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.CompletionLogs.Commands;

/// <summary>
/// Command to update an existing completion log.
/// </summary>
public class UpdateCompletionLog : IRequest<CompletionLogDto>
{
    public Guid CompletionLogId { get; set; }
    public DateTime CompletionDate { get; set; }
    public DateTime? ActualStartTime { get; set; }
    public DateTime? ActualEndTime { get; set; }
    public int TasksCompleted { get; set; }
    public int TotalTasks { get; set; }
    public string? Notes { get; set; }
    public int? MoodRating { get; set; }
}

/// <summary>
/// Handler for UpdateCompletionLog command.
/// </summary>
public class UpdateCompletionLogHandler : IRequestHandler<UpdateCompletionLog, CompletionLogDto>
{
    private readonly IMorningRoutineBuilderContext _context;

    public UpdateCompletionLogHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<CompletionLogDto> Handle(UpdateCompletionLog request, CancellationToken cancellationToken)
    {
        var completionLog = await _context.CompletionLogs
            .FirstOrDefaultAsync(c => c.CompletionLogId == request.CompletionLogId, cancellationToken);

        if (completionLog == null)
        {
            throw new KeyNotFoundException($"CompletionLog with ID {request.CompletionLogId} not found.");
        }

        completionLog.CompletionDate = request.CompletionDate;
        completionLog.ActualStartTime = request.ActualStartTime;
        completionLog.ActualEndTime = request.ActualEndTime;
        completionLog.TasksCompleted = request.TasksCompleted;
        completionLog.TotalTasks = request.TotalTasks;
        completionLog.Notes = request.Notes;
        completionLog.MoodRating = request.MoodRating;

        await _context.SaveChangesAsync(cancellationToken);

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
