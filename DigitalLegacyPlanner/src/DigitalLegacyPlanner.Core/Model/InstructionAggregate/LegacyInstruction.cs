// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DigitalLegacyPlanner.Core;

/// <summary>
/// Represents instructions for handling digital legacy.
/// </summary>
public class LegacyInstruction
{
    /// <summary>
    /// Gets or sets the unique identifier for the instruction.
    /// </summary>
    public Guid LegacyInstructionId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this instruction.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the instruction title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the instruction content.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category or type of instruction.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the priority level.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Gets or sets who should execute these instructions.
    /// </summary>
    public string? AssignedTo { get; set; }

    /// <summary>
    /// Gets or sets when these instructions should be executed.
    /// </summary>
    public string? ExecutionTiming { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Updates the instruction.
    /// </summary>
    /// <param name="newContent">The new content.</param>
    public void UpdateContent(string newContent)
    {
        Content = newContent;
        LastUpdatedAt = DateTime.UtcNow;
    }
}
