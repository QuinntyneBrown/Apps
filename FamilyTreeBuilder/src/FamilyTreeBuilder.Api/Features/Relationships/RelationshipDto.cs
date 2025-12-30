// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;

namespace FamilyTreeBuilder.Api.Features.Relationships;

public class RelationshipDto
{
    public Guid RelationshipId { get; set; }
    public Guid PersonId { get; set; }
    public Guid RelatedPersonId { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public DateTime CreatedAt { get; set; }

    public static RelationshipDto FromRelationship(Relationship relationship)
    {
        return new RelationshipDto
        {
            RelationshipId = relationship.RelationshipId,
            PersonId = relationship.PersonId,
            RelatedPersonId = relationship.RelatedPersonId,
            RelationshipType = relationship.RelationshipType,
            CreatedAt = relationship.CreatedAt
        };
    }
}
