// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;

namespace ClassicCarRestorationLog.Api.Features.WorkLogs;

public class WorkLogDto
{
    public Guid WorkLogId { get; set; }
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public DateTime WorkDate { get; set; }
    public int HoursWorked { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? WorkPerformed { get; set; }
    public DateTime CreatedAt { get; set; }

    public static WorkLogDto FromEntity(WorkLog workLog)
    {
        return new WorkLogDto
        {
            WorkLogId = workLog.WorkLogId,
            UserId = workLog.UserId,
            ProjectId = workLog.ProjectId,
            WorkDate = workLog.WorkDate,
            HoursWorked = workLog.HoursWorked,
            Description = workLog.Description,
            WorkPerformed = workLog.WorkPerformed,
            CreatedAt = workLog.CreatedAt
        };
    }
}
