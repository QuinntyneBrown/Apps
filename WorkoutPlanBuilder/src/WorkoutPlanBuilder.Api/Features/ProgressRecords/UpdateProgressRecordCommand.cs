// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.ProgressRecords;

/// <summary>
/// Command to update an existing progress record.
/// </summary>
public class UpdateProgressRecordCommand : IRequest<ProgressRecordDto?>
{
    public Guid ProgressRecordId { get; set; }
    public int ActualDurationMinutes { get; set; }
    public int? CaloriesBurned { get; set; }
    public int? PerformanceRating { get; set; }
    public string? Notes { get; set; }
    public string? ExerciseDetails { get; set; }
    public DateTime CompletedAt { get; set; }
}

/// <summary>
/// Handler for UpdateProgressRecordCommand.
/// </summary>
public class UpdateProgressRecordCommandHandler : IRequestHandler<UpdateProgressRecordCommand, ProgressRecordDto?>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public UpdateProgressRecordCommandHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<ProgressRecordDto?> Handle(UpdateProgressRecordCommand request, CancellationToken cancellationToken)
    {
        var progressRecord = await _context.ProgressRecords
            .FirstOrDefaultAsync(pr => pr.ProgressRecordId == request.ProgressRecordId, cancellationToken);

        if (progressRecord == null)
        {
            return null;
        }

        progressRecord.ActualDurationMinutes = request.ActualDurationMinutes;
        progressRecord.CaloriesBurned = request.CaloriesBurned;
        progressRecord.PerformanceRating = request.PerformanceRating;
        progressRecord.Notes = request.Notes;
        progressRecord.ExerciseDetails = request.ExerciseDetails;
        progressRecord.CompletedAt = request.CompletedAt;

        await _context.SaveChangesAsync(cancellationToken);

        return progressRecord.ToDto();
    }
}
