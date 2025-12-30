// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;

namespace FamilyTreeBuilder.Api.Features.Persons;

public class CreatePerson
{
    public class Command : IRequest<PersonDto>
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string? BirthPlace { get; set; }
    }

    public class Handler : IRequestHandler<Command, PersonDto>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<PersonDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                PersonId = Guid.NewGuid(),
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                DateOfDeath = request.DateOfDeath,
                BirthPlace = request.BirthPlace,
                CreatedAt = DateTime.UtcNow
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync(cancellationToken);

            return PersonDto.FromPerson(person);
        }
    }
}
