// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;

namespace ClassicCarRestorationLog.Api.Features.Parts;

public class PartDto
{
    public Guid PartId { get; set; }
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? PartNumber { get; set; }
    public string? Supplier { get; set; }
    public decimal? Cost { get; set; }
    public DateTime? OrderedDate { get; set; }
    public DateTime? ReceivedDate { get; set; }
    public bool IsInstalled { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }

    public static PartDto FromEntity(Part part)
    {
        return new PartDto
        {
            PartId = part.PartId,
            UserId = part.UserId,
            ProjectId = part.ProjectId,
            Name = part.Name,
            PartNumber = part.PartNumber,
            Supplier = part.Supplier,
            Cost = part.Cost,
            OrderedDate = part.OrderedDate,
            ReceivedDate = part.ReceivedDate,
            IsInstalled = part.IsInstalled,
            Notes = part.Notes,
            CreatedAt = part.CreatedAt
        };
    }
}
