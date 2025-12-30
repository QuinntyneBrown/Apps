// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.Persons;

public class UpdatePerson
{
    public class Command : IRequest<PersonDto?>
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string? BirthPlace { get; set; }
    }

    public class Handler : IRequestHandler<Command, PersonDto?>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<PersonDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons
                .FirstOrDefaultAsync(p => p.PersonId == request.PersonId, cancellationToken);

            if (person == null)
            {
                return null;
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Gender = request.Gender;
            person.DateOfBirth = request.DateOfBirth;
            person.DateOfDeath = request.DateOfDeath;
            person.BirthPlace = request.BirthPlace;

            await _context.SaveChangesAsync(cancellationToken);

            return PersonDto.FromPerson(person);
        }
    }
}
