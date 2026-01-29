namespace Families.Core.Models;

public class FamilyMember
{
    public Guid FamilyMemberId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid? UserId { get; private set; }
    public Guid HouseholdId { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? Relationship { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }

    private FamilyMember() { }

    public FamilyMember(Guid tenantId, Guid householdId, string firstName, string lastName, string? relationship = null, Guid? userId = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        FamilyMemberId = Guid.NewGuid();
        TenantId = tenantId;
        HouseholdId = householdId;
        FirstName = firstName;
        LastName = lastName;
        Relationship = relationship;
        UserId = userId;
    }

    public void UpdateProfile(string? firstName = null, string? lastName = null, string? relationship = null, DateTime? dateOfBirth = null, string? email = null, string? phone = null)
    {
        if (firstName != null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            FirstName = firstName;
        }

        if (lastName != null)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            LastName = lastName;
        }

        Relationship = relationship ?? Relationship;
        DateOfBirth = dateOfBirth ?? DateOfBirth;
        Email = email ?? Email;
        Phone = phone ?? Phone;
    }

    public void LinkToUser(Guid userId)
    {
        UserId = userId;
    }
}
