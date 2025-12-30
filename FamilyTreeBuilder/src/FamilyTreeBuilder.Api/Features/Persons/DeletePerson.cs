// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.Persons;

public class DeletePerson
{
    public class Command : IRequest<bool>
    {
        public Guid PersonId { get; set; }
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
            var person = await _context.Persons
                .FirstOrDefaultAsync(p => p.PersonId == request.PersonId, cancellationToken);

            if (person == null)
            {
                return false;
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
