// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core;

public class Relationship
{
    public Guid RelationshipId { get; set; }
    public Guid PersonId { get; set; }
    public Person? Person { get; set; }
    public Guid RelatedPersonId { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
