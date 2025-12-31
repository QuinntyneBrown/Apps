// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TaskPriorityMatrix.Core;

namespace TaskPriorityMatrix.Api.Features.Tasks;

public class CreatePriorityTaskCommand
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Urgency Urgency { get; set; }
    public Importance Importance { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid? CategoryId { get; set; }
}
