// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;

namespace DocumentVaultOrganizer.Api.Features.Documents;

public class DocumentDto
{
    public Guid DocumentId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DocumentCategoryEnum Category { get; set; }
    public string? FileUrl { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
