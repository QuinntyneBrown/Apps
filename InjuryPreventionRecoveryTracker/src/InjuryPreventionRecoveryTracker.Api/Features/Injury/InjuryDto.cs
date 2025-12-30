// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Data transfer object for Injury.
/// </summary>
public record InjuryDto
{
    /// <summary>
    /// Gets or sets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the injury type.
    /// </summary>
    public InjuryType InjuryType { get; init; }

    /// <summary>
    /// Gets or sets the injury severity.
    /// </summary>
    public InjurySeverity Severity { get; init; }

    /// <summary>
    /// Gets or sets the body part affected.
    /// </summary>
    public string BodyPart { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the injury occurred.
    /// </summary>
    public DateTime InjuryDate { get; init; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the diagnosis.
    /// </summary>
    public string? Diagnosis { get; init; }

    /// <summary>
    /// Gets or sets the expected recovery days.
    /// </summary>
    public int? ExpectedRecoveryDays { get; init; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public string Status { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the progress percentage.
    /// </summary>
    public int ProgressPercentage { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for Injury.
/// </summary>
public static class InjuryExtensions
{
    /// <summary>
    /// Converts an Injury to a DTO.
    /// </summary>
    /// <param name="injury">The injury.</param>
    /// <returns>The DTO.</returns>
    public static InjuryDto ToDto(this Injury injury)
    {
        return new InjuryDto
        {
            InjuryId = injury.InjuryId,
            UserId = injury.UserId,
            InjuryType = injury.InjuryType,
            Severity = injury.Severity,
            BodyPart = injury.BodyPart,
            InjuryDate = injury.InjuryDate,
            Description = injury.Description,
            Diagnosis = injury.Diagnosis,
            ExpectedRecoveryDays = injury.ExpectedRecoveryDays,
            Status = injury.Status,
            ProgressPercentage = injury.ProgressPercentage,
            Notes = injury.Notes,
            CreatedAt = injury.CreatedAt,
        };
    }
}
