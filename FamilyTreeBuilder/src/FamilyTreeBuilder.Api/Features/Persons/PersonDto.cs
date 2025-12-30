// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;

namespace FamilyTreeBuilder.Api.Features.Persons;

public class PersonDto
{
    public Guid PersonId { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public Gender? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; }
    public string? BirthPlace { get; set; }
    public DateTime CreatedAt { get; set; }

    public static PersonDto FromPerson(Person person)
    {
        return new PersonDto
        {
            PersonId = person.PersonId,
            UserId = person.UserId,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Gender = person.Gender,
            DateOfBirth = person.DateOfBirth,
            DateOfDeath = person.DateOfDeath,
            BirthPlace = person.BirthPlace,
            CreatedAt = person.CreatedAt
        };
    }
}
