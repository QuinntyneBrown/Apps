// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace DocumentVaultOrganizer.Core;

public interface IDocumentVaultOrganizerContext
{
    DbSet<Document> Documents { get; set; }
    DbSet<DocumentCategory> DocumentCategories { get; set; }
    DbSet<ExpirationAlert> ExpirationAlerts { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
