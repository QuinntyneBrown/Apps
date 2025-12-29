using FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Model.FamilyMemberAggregate;

public class FamilyMember
{
    public Guid MemberId { get; private set; }
    public Guid FamilyId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public MemberRole Role { get; private set; }

    private FamilyMember()
    {
    }

    public FamilyMember(Guid familyId, string name, string email, string color, MemberRole role = MemberRole.Member)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be empty.", nameof(email));
        }

        if (string.IsNullOrWhiteSpace(color))
        {
            throw new ArgumentException("Color cannot be empty.", nameof(color));
        }

        MemberId = Guid.NewGuid();
        FamilyId = familyId;
        Name = name;
        Email = email;
        Color = color;
        Role = role;
    }

    public void UpdateProfile(string? name = null, string? email = null, string? color = null)
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
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            }

            Email = email;
        }

        if (color != null)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                throw new ArgumentException("Color cannot be empty.", nameof(color));
            }

            Color = color;
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
