// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.CompletionLogs.Commands;

/// <summary>
/// Command to create a new completion log.
/// </summary>
public class CreateCompletionLog : IRequest<CompletionLogDto>
{
    public Guid RoutineId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CompletionDate { get; set; }
    public DateTime? ActualStartTime { get; set; }
    public DateTime? ActualEndTime { get; set; }
    public int TasksCompleted { get; set; }
    public int TotalTasks { get; set; }
    public string? Notes { get; set; }
    public int? MoodRating { get; set; }
}

/// <summary>
/// Handler for CreateCompletionLog command.
/// </summary>
public class CreateCompletionLogHandler : IRequestHandler<CreateCompletionLog, CompletionLogDto>
{
    private readonly IMorningRoutineBuilderContext _context;

    public CreateCompletionLogHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<CompletionLogDto> Handle(CreateCompletionLog request, CancellationToken cancellationToken)
    {
        var completionLog = new CompletionLog
        {
            CompletionLogId = Guid.NewGuid(),
            RoutineId = request.RoutineId,
            UserId = request.UserId,
            CompletionDate = request.CompletionDate,
            ActualStartTime = request.ActualStartTime,
            ActualEndTime = request.ActualEndTime,
            TasksCompleted = request.TasksCompleted,
            TotalTasks = request.TotalTasks,
            Notes = request.Notes,
            MoodRating = request.MoodRating,
            CreatedAt = DateTime.UtcNow
        };

        _context.CompletionLogs.Add(completionLog);
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
