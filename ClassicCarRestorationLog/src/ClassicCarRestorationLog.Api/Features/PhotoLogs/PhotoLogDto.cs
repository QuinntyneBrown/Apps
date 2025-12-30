// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;

namespace ClassicCarRestorationLog.Api.Features.PhotoLogs;

public class PhotoLogDto
{
    public Guid PhotoLogId { get; set; }
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public DateTime PhotoDate { get; set; }
    public string? Description { get; set; }
    public string? PhotoUrl { get; set; }
    public ProjectPhase? Phase { get; set; }
    public DateTime CreatedAt { get; set; }

    public static PhotoLogDto FromEntity(PhotoLog photoLog)
    {
        return new PhotoLogDto
        {
            PhotoLogId = photoLog.PhotoLogId,
            UserId = photoLog.UserId,
            ProjectId = photoLog.ProjectId,
            PhotoDate = photoLog.PhotoDate,
            Description = photoLog.Description,
            PhotoUrl = photoLog.PhotoUrl,
            Phase = photoLog.Phase,
            CreatedAt = photoLog.CreatedAt
        };
    }
}
