// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Api.Features.Assignments;

/// <summary>
/// Represents an assignment data transfer object.
/// </summary>
public class AssignmentDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the assignment.
    /// </summary>
    public Guid AssignmentId { get; set; }

    /// <summary>
    /// Gets or sets the chore ID.
    /// </summary>
    public Guid ChoreId { get; set; }

    /// <summary>
    /// Gets or sets the chore name.
    /// </summary>
    public string? ChoreName { get; set; }

    /// <summary>
    /// Gets or sets the family member ID.
    /// </summary>
    public Guid FamilyMemberId { get; set; }

    /// <summary>
    /// Gets or sets the family member name.
    /// </summary>
    public string? FamilyMemberName { get; set; }

    /// <summary>
    /// Gets or sets the assigned date.
    /// </summary>
    public DateTime AssignedDate { get; set; }

    /// <summary>
    /// Gets or sets the due date.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime? CompletedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the assignment is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the assignment is verified by a parent.
    /// </summary>
    public bool IsVerified { get; set; }

    /// <summary>
    /// Gets or sets notes about the assignment.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the points earned.
    /// </summary>
    public int PointsEarned { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the assignment is overdue.
    /// </summary>
    public bool IsOverdue { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Represents a request to create an assignment.
/// </summary>
public class CreateAssignmentDto
{
    /// <summary>
    /// Gets or sets the chore ID.
    /// </summary>
    public Guid ChoreId { get; set; }

    /// <summary>
    /// Gets or sets the family member ID.
    /// </summary>
    public Guid FamilyMemberId { get; set; }

    /// <summary>
    /// Gets or sets the assigned date.
    /// </summary>
    public DateTime AssignedDate { get; set; }

    /// <summary>
    /// Gets or sets the due date.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Gets or sets notes about the assignment.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a request to update an assignment.
/// </summary>
public class UpdateAssignmentDto
{
    /// <summary>
    /// Gets or sets the due date.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Gets or sets notes about the assignment.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a request to complete an assignment.
/// </summary>
public class CompleteAssignmentDto
{
    /// <summary>
    /// Gets or sets notes about the completion.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a request to verify an assignment.
/// </summary>
public class VerifyAssignmentDto
{
    /// <summary>
    /// Gets or sets the points to award.
    /// </summary>
    public int Points { get; set; }
}
