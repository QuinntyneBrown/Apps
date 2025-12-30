// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleValueTracker.Core;

/// <summary>
/// Event raised when a new value assessment is created.
/// </summary>
public record ValueAssessmentCreatedEvent
{
    /// <summary>
    /// Gets the value assessment ID.
    /// </summary>
    public Guid ValueAssessmentId { get; init; }

    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the assessment date.
    /// </summary>
    public DateTime AssessmentDate { get; init; }

    /// <summary>
    /// Gets the estimated value.
    /// </summary>
    public decimal EstimatedValue { get; init; }

    /// <summary>
    /// Gets the condition grade.
    /// </summary>
    public ConditionGrade ConditionGrade { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
