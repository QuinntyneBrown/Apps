// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core;

/// <summary>
/// Represents a job interview.
/// </summary>
public class Interview
{
    /// <summary>
    /// Gets or sets the unique identifier for the interview.
    /// </summary>
    public Guid InterviewId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this interview.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the application ID.
    /// </summary>
    public Guid ApplicationId { get; set; }

    /// <summary>
    /// Gets or sets the interview type (Phone, Video, On-site, Technical, etc.).
    /// </summary>
    public string InterviewType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the scheduled date and time.
    /// </summary>
    public DateTime ScheduledDateTime { get; set; }

    /// <summary>
    /// Gets or sets the duration in minutes.
    /// </summary>
    public int? DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the interviewer names.
    /// </summary>
    public List<string> Interviewers { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the location or meeting link.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets preparation notes.
    /// </summary>
    public string? PreparationNotes { get; set; }

    /// <summary>
    /// Gets or sets feedback or notes after the interview.
    /// </summary>
    public string? Feedback { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the interview is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime? CompletedDate { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the application.
    /// </summary>
    public Application? Application { get; set; }

    /// <summary>
    /// Marks the interview as completed.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
        CompletedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Reschedules the interview to a new date and time.
    /// </summary>
    /// <param name="newDateTime">The new scheduled date and time.</param>
    public void Reschedule(DateTime newDateTime)
    {
        ScheduledDateTime = newDateTime;
        UpdatedAt = DateTime.UtcNow;
    }
}
