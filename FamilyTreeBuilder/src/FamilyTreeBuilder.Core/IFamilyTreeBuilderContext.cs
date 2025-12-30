// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Core;

public interface IFamilyTreeBuilderContext
{
    DbSet<Person> Persons { get; set; }
    DbSet<Relationship> Relationships { get; set; }
    DbSet<Story> Stories { get; set; }
    DbSet<FamilyPhoto> FamilyPhotos { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
