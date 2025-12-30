// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;

namespace FamilyTreeBuilder.Api.Features.Relationships;

public class CreateRelationship
{
    public class Command : IRequest<RelationshipDto>
    {
        public Guid PersonId { get; set; }
        public Guid RelatedPersonId { get; set; }
        public RelationshipType RelationshipType { get; set; }
    }

    public class Handler : IRequestHandler<Command, RelationshipDto>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<RelationshipDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var relationship = new Relationship
            {
                RelationshipId = Guid.NewGuid(),
                PersonId = request.PersonId,
                RelatedPersonId = request.RelatedPersonId,
                RelationshipType = request.RelationshipType,
                CreatedAt = DateTime.UtcNow
            };

            _context.Relationships.Add(relationship);
            await _context.SaveChangesAsync(cancellationToken);

            return RelationshipDto.FromRelationship(relationship);
        }
    }
}
