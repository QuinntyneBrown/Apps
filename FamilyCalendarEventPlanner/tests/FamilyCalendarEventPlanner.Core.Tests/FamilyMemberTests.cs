using FamilyCalendarEventPlanner.Core.Models.FamilyMemberAggregate;
using FamilyCalendarEventPlanner.Core.Models.FamilyMemberAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Tests;

public class FamilyMemberTests
{
    private readonly Guid _familyId = Guid.NewGuid();

    [Test]
    public void Constructor_ValidParameters_CreatesMember()
    {
        var member = new FamilyMember(TestHelpers.DefaultTenantId, _familyId, "John Doe", "john@example.com", "#FF5733");

        Assert.Multiple(() =>
        {
            Assert.That(member.MemberId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(member.FamilyId, Is.EqualTo(_familyId));
            Assert.That(member.Name, Is.EqualTo("John Doe"));
            Assert.That(member.Email, Is.EqualTo("john@example.com"));
            Assert.That(member.Color, Is.EqualTo("#FF5733"));
            Assert.That(member.Role, Is.EqualTo(MemberRole.Member));
            Assert.That(member.IsImmediate, Is.True);
            Assert.That(member.RelationType, Is.EqualTo(RelationType.Self));
        });
    }

    [Test]
    public void Constructor_WithRole_SetsRole()
    {
        var member = new FamilyMember(TestHelpers.DefaultTenantId, _familyId, "Jane Doe", "jane@example.com", "#0000FF", MemberRole.Admin);

        Assert.That(member.Role, Is.EqualTo(MemberRole.Admin));
    }

    [Test]
    public void Constructor_WithIsImmediateAndRelationType_SetsProperties()
    {
        var member = new FamilyMember(
            TestHelpers.DefaultTenantId,
            _familyId,
            "Uncle Bob",
            "bob@example.com",
            "#00FF00",
            MemberRole.Member,
            isImmediate: false,
            relationType: RelationType.AuntUncle);

        Assert.Multiple(() =>
        {
            Assert.That(member.IsImmediate, Is.False);
            Assert.That(member.RelationType, Is.EqualTo(RelationType.AuntUncle));
        });
    }

    [Test]
    public void Constructor_WithNullEmail_SetsEmailToNull()
    {
        var member = new FamilyMember(TestHelpers.DefaultTenantId, _familyId, "John Doe", null, "#FF5733");

        Assert.That(member.Email, Is.Null);
    }

    [Test]
    public void Constructor_WithEmptyEmail_SetsEmailToNull()
    {
        var member = new FamilyMember(TestHelpers.DefaultTenantId, _familyId, "John Doe", "", "#FF5733");

        Assert.That(member.Email, Is.Null);
    }

    [Test]
    public void Constructor_WithWhitespaceEmail_SetsEmailToNull()
    {
        var member = new FamilyMember(TestHelpers.DefaultTenantId, _familyId, "John Doe", "   ", "#FF5733");

        Assert.That(member.Email, Is.Null);
    }

    [Test]
    public void Constructor_EmptyName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new FamilyMember(TestHelpers.DefaultTenantId, _familyId, "", "test@example.com", "#000000"));
    }

    [Test]
    public void Constructor_WhitespaceName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new FamilyMember(TestHelpers.DefaultTenantId, _familyId, "   ", "test@example.com", "#000000"));
    }

    [Test]
    public void Constructor_EmptyColor_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new FamilyMember(TestHelpers.DefaultTenantId, _familyId, "Test", "test@example.com", ""));
    }

    [Test]
    public void UpdateProfile_ValidName_UpdatesName()
    {
        var member = CreateDefaultMember();

        member.UpdateProfile(name: "New Name");

        Assert.That(member.Name, Is.EqualTo("New Name"));
    }

    [Test]
    public void UpdateProfile_EmptyName_ThrowsArgumentException()
    {
        var member = CreateDefaultMember();

        Assert.Throws<ArgumentException>(() => member.UpdateProfile(name: ""));
    }

    [Test]
    public void UpdateProfile_ValidEmail_UpdatesEmail()
    {
        var member = CreateDefaultMember();

        member.UpdateProfile(email: "newemail@example.com");

        Assert.That(member.Email, Is.EqualTo("newemail@example.com"));
    }

    [Test]
    public void UpdateProfile_EmptyEmail_SetsEmailToNull()
    {
        var member = CreateDefaultMember();

        member.UpdateProfile(email: "");

        Assert.That(member.Email, Is.Null);
    }

    [Test]
    public void UpdateProfile_ValidColor_UpdatesColor()
    {
        var member = CreateDefaultMember();

        member.UpdateProfile(color: "#00FF00");

        Assert.That(member.Color, Is.EqualTo("#00FF00"));
    }

    [Test]
    public void UpdateProfile_EmptyColor_ThrowsArgumentException()
    {
        var member = CreateDefaultMember();

        Assert.Throws<ArgumentException>(() => member.UpdateProfile(color: ""));
    }

    [Test]
    public void UpdateProfile_IsImmediate_UpdatesIsImmediate()
    {
        var member = CreateDefaultMember();
        Assert.That(member.IsImmediate, Is.True);

        member.UpdateProfile(isImmediate: false);

        Assert.That(member.IsImmediate, Is.False);
    }

    [Test]
    public void UpdateProfile_RelationType_UpdatesRelationType()
    {
        var member = CreateDefaultMember();

        member.UpdateProfile(relationType: RelationType.Spouse);

        Assert.That(member.RelationType, Is.EqualTo(RelationType.Spouse));
    }

    [Test]
    public void UpdateProfile_MultipleProperties_UpdatesAll()
    {
        var member = CreateDefaultMember();

        member.UpdateProfile(
            name: "New Name",
            email: "new@example.com",
            color: "#123456",
            isImmediate: false,
            relationType: RelationType.Parent);

        Assert.Multiple(() =>
        {
            Assert.That(member.Name, Is.EqualTo("New Name"));
            Assert.That(member.Email, Is.EqualTo("new@example.com"));
            Assert.That(member.Color, Is.EqualTo("#123456"));
            Assert.That(member.IsImmediate, Is.False);
            Assert.That(member.RelationType, Is.EqualTo(RelationType.Parent));
        });
    }

    [Test]
    public void ChangeRole_ToAdmin_UpdatesRole()
    {
        var member = CreateDefaultMember();

        member.ChangeRole(MemberRole.Admin);

        Assert.That(member.Role, Is.EqualTo(MemberRole.Admin));
    }

    [Test]
    public void ChangeRole_ToViewOnly_UpdatesRole()
    {
        var member = CreateDefaultMember();

        member.ChangeRole(MemberRole.ViewOnly);

        Assert.That(member.Role, Is.EqualTo(MemberRole.ViewOnly));
    }

    [Test]
    public void BlockAvailability_ValidParameters_ReturnsAvailabilityBlock()
    {
        var member = CreateDefaultMember();
        var startTime = DateTime.UtcNow.AddHours(1);
        var endTime = DateTime.UtcNow.AddHours(2);

        var block = member.BlockAvailability(startTime, endTime, BlockType.Busy, "Meeting");

        Assert.Multiple(() =>
        {
            Assert.That(block.MemberId, Is.EqualTo(member.MemberId));
            Assert.That(block.StartTime, Is.EqualTo(startTime));
            Assert.That(block.EndTime, Is.EqualTo(endTime));
            Assert.That(block.BlockType, Is.EqualTo(BlockType.Busy));
            Assert.That(block.Reason, Is.EqualTo("Meeting"));
        });
    }

    [Test]
    public void IsAvailable_NoBlocks_ReturnsTrue()
    {
        var member = CreateDefaultMember();
        var blocks = new List<AvailabilityBlock>();

        var isAvailable = member.IsAvailable(DateTime.UtcNow, DateTime.UtcNow.AddHours(1), blocks);

        Assert.That(isAvailable, Is.True);
    }

    [Test]
    public void IsAvailable_OverlappingBlock_ReturnsFalse()
    {
        var member = CreateDefaultMember();
        var blocks = new List<AvailabilityBlock>
        {
            new AvailabilityBlock(TestHelpers.DefaultTenantId, member.MemberId, DateTime.UtcNow.AddMinutes(30), DateTime.UtcNow.AddHours(2), BlockType.Busy)
        };

        var isAvailable = member.IsAvailable(DateTime.UtcNow, DateTime.UtcNow.AddHours(1), blocks);

        Assert.That(isAvailable, Is.False);
    }

    [Test]
    public void IsAvailable_NonOverlappingBlock_ReturnsTrue()
    {
        var member = CreateDefaultMember();
        var blocks = new List<AvailabilityBlock>
        {
            new AvailabilityBlock(TestHelpers.DefaultTenantId, member.MemberId, DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(4), BlockType.Busy)
        };

        var isAvailable = member.IsAvailable(DateTime.UtcNow, DateTime.UtcNow.AddHours(1), blocks);

        Assert.That(isAvailable, Is.True);
    }

    [Test]
    public void IsAvailable_BlockForDifferentMember_ReturnsTrue()
    {
        var member = CreateDefaultMember();
        var otherMemberId = Guid.NewGuid();
        var blocks = new List<AvailabilityBlock>
        {
            new AvailabilityBlock(TestHelpers.DefaultTenantId, otherMemberId, DateTime.UtcNow, DateTime.UtcNow.AddHours(2), BlockType.Busy)
        };

        var isAvailable = member.IsAvailable(DateTime.UtcNow, DateTime.UtcNow.AddHours(1), blocks);

        Assert.That(isAvailable, Is.True);
    }

    private FamilyMember CreateDefaultMember()
    {
        return new FamilyMember(TestHelpers.DefaultTenantId, _familyId, "Test User", "test@example.com", "#FF0000");
    }
}
