// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaskPriorityMatrix.Core;

public class PriorityTask
{
    public Guid PriorityTaskId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Urgency Urgency { get; set; }
    public Importance Importance { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
    public DateTime? DueDate { get; set; }
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
}
