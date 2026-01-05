// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core;

public record RelationshipCreatedEvent
{
    public Guid RelationshipId { get; init; }
    public Guid PersonId { get; init; }
    public Guid RelatedPersonId { get; init; }
    public RelationshipType RelationshipType { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
