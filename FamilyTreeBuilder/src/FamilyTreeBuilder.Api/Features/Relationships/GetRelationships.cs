// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.Relationships;

public class GetRelationships
{
    public class Query : IRequest<List<RelationshipDto>>
    {
        public Guid? PersonId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<RelationshipDto>>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<List<RelationshipDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Relationships.AsQueryable();

            if (request.PersonId.HasValue)
            {
                query = query.Where(r => r.PersonId == request.PersonId.Value);
            }

            var relationships = await query
                .OrderBy(r => r.CreatedAt)
                .ToListAsync(cancellationToken);

            return relationships.Select(RelationshipDto.FromRelationship).ToList();
        }
    }
}
