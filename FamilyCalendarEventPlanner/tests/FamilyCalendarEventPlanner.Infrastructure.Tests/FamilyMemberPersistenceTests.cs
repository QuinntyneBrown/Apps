namespace FamilyCalendarEventPlanner.Infrastructure.Tests;

public class FamilyMemberPersistenceTests
{
    private FamilyCalendarEventPlannerContext _context = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FamilyCalendarEventPlannerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FamilyCalendarEventPlannerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task AddFamilyMember_PersistsToDatabase()
    {
        var member = CreateDefaultMember();

        _context.FamilyMembers.Add(member);
        await _context.SaveChangesAsync();

        var retrieved = await _context.FamilyMembers.FindAsync(member.MemberId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("John Doe"));
    }

    [Test]
    public async Task FamilyMember_PersistsAllProperties()
    {
        var familyId = Guid.NewGuid();
        var member = new FamilyMember(familyId, "Jane Doe", "jane@example.com", "#FF5733", MemberRole.Admin);

        _context.FamilyMembers.Add(member);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.FamilyMembers.FindAsync(member.MemberId);

        Assert.Multiple(() =>
        {
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.FamilyId, Is.EqualTo(familyId));
            Assert.That(retrieved.Name, Is.EqualTo("Jane Doe"));
            Assert.That(retrieved.Email, Is.EqualTo("jane@example.com"));
            Assert.That(retrieved.Color, Is.EqualTo("#FF5733"));
            Assert.That(retrieved.Role, Is.EqualTo(MemberRole.Admin));
        });
    }

    [Test]
    public async Task FamilyMember_CanUpdateProfile()
    {
        var member = CreateDefaultMember();
        _context.FamilyMembers.Add(member);
        await _context.SaveChangesAsync();

        member.UpdateProfile(name: "Updated Name", email: "updated@example.com");
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.FamilyMembers.FindAsync(member.MemberId);

        Assert.Multiple(() =>
        {
            Assert.That(retrieved!.Name, Is.EqualTo("Updated Name"));
            Assert.That(retrieved.Email, Is.EqualTo("updated@example.com"));
        });
    }

    [Test]
    public async Task FamilyMember_CanChangeRole()
    {
        var member = CreateDefaultMember();
        _context.FamilyMembers.Add(member);
        await _context.SaveChangesAsync();

        member.ChangeRole(MemberRole.ViewOnly);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
        var retrieved = await _context.FamilyMembers.FindAsync(member.MemberId);

        Assert.That(retrieved!.Role, Is.EqualTo(MemberRole.ViewOnly));
    }

    [Test]
    public async Task FamilyMember_CanQueryByFamilyId()
    {
        var familyId = Guid.NewGuid();

        var member1 = new FamilyMember(familyId, "Member 1", "m1@example.com", "#000001");
        var member2 = new FamilyMember(familyId, "Member 2", "m2@example.com", "#000002");
        var member3 = new FamilyMember(Guid.NewGuid(), "Member 3", "m3@example.com", "#000003");

        _context.FamilyMembers.AddRange(member1, member2, member3);
        await _context.SaveChangesAsync();

        var familyMembers = await _context.FamilyMembers
            .Where(m => m.FamilyId == familyId)
            .ToListAsync();

        Assert.That(familyMembers, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task FamilyMember_CanQueryByEmail()
    {
        var member = CreateDefaultMember();
        _context.FamilyMembers.Add(member);
        await _context.SaveChangesAsync();

        var retrieved = await _context.FamilyMembers
            .FirstOrDefaultAsync(m => m.Email == "john@example.com");

        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("John Doe"));
    }

    [Test]
    public async Task FamilyMember_CanDelete()
    {
        var member = CreateDefaultMember();
        _context.FamilyMembers.Add(member);
        await _context.SaveChangesAsync();

        _context.FamilyMembers.Remove(member);
        await _context.SaveChangesAsync();

        var retrieved = await _context.FamilyMembers.FindAsync(member.MemberId);
        Assert.That(retrieved, Is.Null);
    }

    [Test]
    public async Task FamilyMember_PersistsAllRoleTypes()
    {
        var familyId = Guid.NewGuid();

        var admin = new FamilyMember(familyId, "Admin", "admin@example.com", "#001", MemberRole.Admin);
        var regular = new FamilyMember(familyId, "Regular", "regular@example.com", "#002", MemberRole.Member);
        var viewOnly = new FamilyMember(familyId, "ViewOnly", "view@example.com", "#003", MemberRole.ViewOnly);

        _context.FamilyMembers.AddRange(admin, regular, viewOnly);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();

        var adminRetrieved = await _context.FamilyMembers.FindAsync(admin.MemberId);
        var regularRetrieved = await _context.FamilyMembers.FindAsync(regular.MemberId);
        var viewOnlyRetrieved = await _context.FamilyMembers.FindAsync(viewOnly.MemberId);

        Assert.Multiple(() =>
        {
            Assert.That(adminRetrieved!.Role, Is.EqualTo(MemberRole.Admin));
            Assert.That(regularRetrieved!.Role, Is.EqualTo(MemberRole.Member));
            Assert.That(viewOnlyRetrieved!.Role, Is.EqualTo(MemberRole.ViewOnly));
        });
    }

    private FamilyMember CreateDefaultMember()
    {
        return new FamilyMember(Guid.NewGuid(), "John Doe", "john@example.com", "#000000");
    }
}
