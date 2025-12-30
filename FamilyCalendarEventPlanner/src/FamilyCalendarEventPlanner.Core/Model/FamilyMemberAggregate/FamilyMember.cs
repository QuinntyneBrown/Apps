using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate;

public class FamilyMember
{
    public Guid MemberId { get; private set; }
    public Guid FamilyId { get; private set; }
    public Guid? HouseholdId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public string Color { get; private set; } = string.Empty;
    public MemberRole Role { get; private set; }
    public bool IsImmediate { get; private set; }
    public RelationType RelationType { get; private set; }

    private FamilyMember()
    {
    }

    public FamilyMember(
        Guid familyId,
        string name,
        string? email,
        string color,
        MemberRole role = MemberRole.Member,
        bool isImmediate = true,
        RelationType relationType = RelationType.Self,
        Guid? householdId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(color))
        {
            throw new ArgumentException("Color cannot be empty.", nameof(color));
        }

        MemberId = Guid.NewGuid();
        FamilyId = familyId;
        HouseholdId = householdId;
        Name = name;
        Email = string.IsNullOrWhiteSpace(email) ? null : email;
        Color = color;
        Role = role;
        IsImmediate = isImmediate;
        RelationType = relationType;
    }

    public void UpdateProfile(
        string? name = null,
        string? email = null,
        string? color = null,
        bool? isImmediate = null,
        RelationType? relationType = null,
        Guid? householdId = null,
        bool clearHousehold = false)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            }

            Name = name;
        }

        if (email != null)
        {
            Email = string.IsNullOrWhiteSpace(email) ? null : email;
        }

        if (color != null)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                throw new ArgumentException("Color cannot be empty.", nameof(color));
            }

            Color = color;
        }

        if (isImmediate.HasValue)
        {
            IsImmediate = isImmediate.Value;
        }

        if (relationType.HasValue)
        {
            RelationType = relationType.Value;
        }

        if (clearHousehold)
        {
            HouseholdId = null;
        }
        else if (householdId.HasValue)
        {
            HouseholdId = householdId.Value;
        }
    }

    public void ChangeRole(MemberRole newRole)
    {
        Role = newRole;
    }

    public AvailabilityBlock BlockAvailability(DateTime startTime, DateTime endTime, BlockType blockType, string? reason = null)
    {
        return new AvailabilityBlock(MemberId, startTime, endTime, blockType, reason);
    }

    public bool IsAvailable(DateTime startTime, DateTime endTime, IEnumerable<AvailabilityBlock> blocks)
    {
        return !blocks.Any(block => block.MemberId == MemberId && block.OverlapsWith(startTime, endTime));
    }
}
