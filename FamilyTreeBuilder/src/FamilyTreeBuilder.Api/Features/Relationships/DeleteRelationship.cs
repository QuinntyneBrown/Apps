// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.Relationships;

public class DeleteRelationship
{
    public class Command : IRequest<bool>
    {
        public Guid RelationshipId { get; set; }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var relationship = await _context.Relationships
                .FirstOrDefaultAsync(r => r.RelationshipId == request.RelationshipId, cancellationToken);

            if (relationship == null)
            {
                return false;
            }

            _context.Relationships.Remove(relationship);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
