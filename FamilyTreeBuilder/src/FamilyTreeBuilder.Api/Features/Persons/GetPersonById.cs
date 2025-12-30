// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.Persons;

public class GetPersonById
{
    public class Query : IRequest<PersonDto?>
    {
        public Guid PersonId { get; set; }
    }

    public class Handler : IRequestHandler<Query, PersonDto?>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<PersonDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons
                .FirstOrDefaultAsync(p => p.PersonId == request.PersonId, cancellationToken);

            return person == null ? null : PersonDto.FromPerson(person);
        }
    }
}
