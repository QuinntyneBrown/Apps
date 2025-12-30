// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.CompletionLogs.Queries;

/// <summary>
/// Query to get all completion logs.
/// </summary>
public class GetCompletionLogs : IRequest<List<CompletionLogDto>>
{
    public Guid? RoutineId { get; set; }
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for GetCompletionLogs query.
/// </summary>
public class GetCompletionLogsHandler : IRequestHandler<GetCompletionLogs, List<CompletionLogDto>>
{
    private readonly IMorningRoutineBuilderContext _context;

    public GetCompletionLogsHandler(IMorningRoutineBuilderContext context)
    {
        _context = context;
    }

    public async Task<List<CompletionLogDto>> Handle(GetCompletionLogs request, CancellationToken cancellationToken)
    {
        var query = _context.CompletionLogs.AsQueryable();

        if (request.RoutineId.HasValue)
        {
            query = query.Where(c => c.RoutineId == request.RoutineId.Value);
        }

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        var completionLogs = await query
            .OrderByDescending(c => c.CompletionDate)
            .ToListAsync(cancellationToken);

        return completionLogs.Select(c => new CompletionLogDto
        {
            CompletionLogId = c.CompletionLogId,
            RoutineId = c.RoutineId,
            UserId = c.UserId,
            CompletionDate = c.CompletionDate,
            ActualStartTime = c.ActualStartTime,
            ActualEndTime = c.ActualEndTime,
            TasksCompleted = c.TasksCompleted,
            TotalTasks = c.TotalTasks,
            Notes = c.Notes,
            MoodRating = c.MoodRating,
            CreatedAt = c.CreatedAt
        }).ToList();
    }
}
