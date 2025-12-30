// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.Persons;

public class GetPersons
{
    public class Query : IRequest<List<PersonDto>>
    {
        public Guid? UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<PersonDto>>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<List<PersonDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Persons.AsQueryable();

            if (request.UserId.HasValue)
            {
                query = query.Where(p => p.UserId == request.UserId.Value);
            }

            var persons = await query
                .OrderBy(p => p.FirstName)
                .ToListAsync(cancellationToken);

            return persons.Select(PersonDto.FromPerson).ToList();
        }
    }
}
