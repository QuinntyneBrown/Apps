namespace Persons.Core.Models;

public class Relationship
{
    public Guid RelationshipId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid Person1Id { get; private set; }
    public Guid Person2Id { get; private set; }
    public string RelationshipType { get; private set; } = string.Empty;
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    private Relationship() { }

    public Relationship(Guid tenantId, Guid person1Id, Guid person2Id, string relationshipType, DateTime? startDate = null)
    {
        RelationshipId = Guid.NewGuid();
        TenantId = tenantId;
        Person1Id = person1Id;
        Person2Id = person2Id;
        RelationshipType = relationshipType;
        StartDate = startDate;
    }

    public void Update(string? relationshipType = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        if (relationshipType != null) RelationshipType = relationshipType;
        StartDate = startDate ?? StartDate;
        EndDate = endDate ?? EndDate;
    }
}
