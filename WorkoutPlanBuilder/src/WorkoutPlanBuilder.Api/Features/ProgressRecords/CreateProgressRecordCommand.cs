// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.ProgressRecords;

/// <summary>
/// Command to create a new progress record.
/// </summary>
public class CreateProgressRecordCommand : IRequest<ProgressRecordDto>
{
    public Guid UserId { get; set; }
    public Guid WorkoutId { get; set; }
    public int ActualDurationMinutes { get; set; }
    public int? CaloriesBurned { get; set; }
    public int? PerformanceRating { get; set; }
    public string? Notes { get; set; }
    public string? ExerciseDetails { get; set; }
    public DateTime? CompletedAt { get; set; }
}

/// <summary>
/// Handler for CreateProgressRecordCommand.
/// </summary>
public class CreateProgressRecordCommandHandler : IRequestHandler<CreateProgressRecordCommand, ProgressRecordDto>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public CreateProgressRecordCommandHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<ProgressRecordDto> Handle(CreateProgressRecordCommand request, CancellationToken cancellationToken)
    {
        var progressRecord = new ProgressRecord
        {
            ProgressRecordId = Guid.NewGuid(),
            UserId = request.UserId,
            WorkoutId = request.WorkoutId,
            ActualDurationMinutes = request.ActualDurationMinutes,
            CaloriesBurned = request.CaloriesBurned,
            PerformanceRating = request.PerformanceRating,
            Notes = request.Notes,
            ExerciseDetails = request.ExerciseDetails,
            CompletedAt = request.CompletedAt ?? DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.ProgressRecords.Add(progressRecord);
        await _context.SaveChangesAsync(cancellationToken);

        return progressRecord.ToDto();
    }
}
