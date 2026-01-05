using FamilyCalendarEventPlanner.Core.Models.FamilyMemberAggregate;
using FamilyCalendarEventPlanner.Core.Models.FamilyMemberAggregate.Enums;

namespace FamilyCalendarEventPlanner.Api.Features.FamilyMembers;

public record FamilyMemberDto
{
    public Guid MemberId { get; init; }
    public Guid FamilyId { get; init; }
    public Guid? HouseholdId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Email { get; init; }
    public string Color { get; init; } = string.Empty;
    public MemberRole Role { get; init; }
    public bool IsImmediate { get; init; }
    public RelationType RelationType { get; init; }
}

public static class FamilyMemberExtensions
{
    public static FamilyMemberDto ToDto(this FamilyMember member)
    {
        return new FamilyMemberDto
        {
            MemberId = member.MemberId,
            FamilyId = member.FamilyId,
            HouseholdId = member.HouseholdId,
            Name = member.Name,
            Email = member.Email,
            Color = member.Color,
            Role = member.Role,
            IsImmediate = member.IsImmediate,
            RelationType = member.RelationType,
        };
    }
}
