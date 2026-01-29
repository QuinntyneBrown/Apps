namespace Persons.Core.Models;

public class Person
{
    public Guid PersonId { get; private set; }
    public Guid TenantId { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateTime? DateOfBirth { get; private set; }
    public DateTime? DateOfDeath { get; private set; }
    public string? Gender { get; private set; }
    public string? BirthPlace { get; private set; }
    public string? Biography { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Person() { }

    public Person(Guid tenantId, string firstName, string lastName, DateTime? dateOfBirth = null, string? gender = null)
    {
        PersonId = Guid.NewGuid();
        TenantId = tenantId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? firstName = null, string? lastName = null, DateTime? dateOfBirth = null, 
        DateTime? dateOfDeath = null, string? gender = null, string? birthPlace = null, string? biography = null)
    {
        if (firstName != null) FirstName = firstName;
        if (lastName != null) LastName = lastName;
        DateOfBirth = dateOfBirth ?? DateOfBirth;
        DateOfDeath = dateOfDeath ?? DateOfDeath;
        Gender = gender ?? Gender;
        BirthPlace = birthPlace ?? BirthPlace;
        Biography = biography ?? Biography;
    }
}
