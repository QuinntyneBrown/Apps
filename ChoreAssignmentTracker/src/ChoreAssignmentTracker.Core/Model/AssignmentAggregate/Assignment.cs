// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core;

/// <summary>
/// Represents a chore assignment to a family member.
/// </summary>
public class Assignment
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
    /// Gets or sets the chore.
    /// </summary>
    public Chore? Chore { get; set; }

    /// <summary>
    /// Gets or sets the family member ID.
    /// </summary>
    public Guid FamilyMemberId { get; set; }

    /// <summary>
    /// Gets or sets the family member.
    /// </summary>
    public FamilyMember? FamilyMember { get; set; }

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
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Marks the assignment as completed.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
        CompletedDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Verifies the completed assignment and awards points.
    /// </summary>
    /// <param name="points">The points to award.</param>
    public void Verify(int points)
    {
        IsVerified = true;
        PointsEarned = points;
    }

    /// <summary>
    /// Checks if the assignment is overdue.
    /// </summary>
    /// <returns>True if overdue; otherwise, false.</returns>
    public bool IsOverdue()
    {
        return !IsCompleted && DueDate < DateTime.UtcNow;
    }
}
