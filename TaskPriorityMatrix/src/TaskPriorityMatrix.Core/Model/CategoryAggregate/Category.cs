// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaskPriorityMatrix.Core;

public class Category
{
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#000000";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<PriorityTask> Tasks { get; set; } = new List<PriorityTask>();
}
