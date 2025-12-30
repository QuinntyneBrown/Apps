// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.Relationships;

public class GetRelationshipById
{
    public class Query : IRequest<RelationshipDto?>
    {
        public Guid RelationshipId { get; set; }
    }

    public class Handler : IRequestHandler<Query, RelationshipDto?>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<RelationshipDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var relationship = await _context.Relationships
                .FirstOrDefaultAsync(r => r.RelationshipId == request.RelationshipId, cancellationToken);

            return relationship == null ? null : RelationshipDto.FromRelationship(relationship);
        }
    }
}
