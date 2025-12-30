// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Infrastructure.Tests.Data;

/// <summary>
/// Contains unit tests for the <see cref="CollegeSavingsPlannerContext"/> class.
/// </summary>
[TestFixture]
public class CollegeSavingsPlannerContextTests
{
    private DbContextOptions<CollegeSavingsPlannerContext> _options = null!;
    private CollegeSavingsPlannerContext _context = null!;

    /// <summary>
    /// Sets up the test context before each test.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<CollegeSavingsPlannerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new CollegeSavingsPlannerContext(_options);
    }

    /// <summary>
    /// Cleans up resources after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    #region Plan Tests

    /// <summary>
    /// Tests that a plan can be created successfully.
    /// </summary>
    [Test]
    public async Task CreatePlan_ShouldAddPlanToDatabase()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "New York 529",
            State = "New York",
            AccountNumber = "NY-001",
            CurrentBalance = 10000.00m,
            OpenedDate = DateTime.UtcNow,
            Administrator = "Vanguard",
            IsActive = true
        };

        // Act
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Plans.FindAsync(plan.PlanId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Name, Is.EqualTo("New York 529"));
        Assert.That(retrieved.CurrentBalance, Is.EqualTo(10000.00m));
    }

    /// <summary>
    /// Tests that a plan can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdatePlan_ShouldModifyExistingPlan()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "Original",
            State = "NY",
            CurrentBalance = 5000.00m,
            OpenedDate = DateTime.UtcNow,
            IsActive = true
        };
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        // Act
        plan.CurrentBalance = 7500.00m;
        plan.IsActive = false;
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Plans.FindAsync(plan.PlanId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.CurrentBalance, Is.EqualTo(7500.00m));
        Assert.That(updated.IsActive, Is.False);
    }

    /// <summary>
    /// Tests that a plan can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeletePlan_ShouldRemovePlanFromDatabase()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "To Delete",
            State = "CA",
            CurrentBalance = 1000.00m,
            OpenedDate = DateTime.UtcNow
        };
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        // Act
        _context.Plans.Remove(plan);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Plans.FindAsync(plan.PlanId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Contribution Tests

    /// <summary>
    /// Tests that a contribution can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateContribution_ShouldAddContributionToDatabase()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "Test Plan",
            State = "NY",
            CurrentBalance = 5000.00m,
            OpenedDate = DateTime.UtcNow
        };
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            PlanId = plan.PlanId,
            Amount = 500.00m,
            ContributionDate = DateTime.UtcNow,
            Contributor = "John Doe",
            Notes = "Monthly contribution",
            IsRecurring = true
        };

        // Act
        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Contributions.FindAsync(contribution.ContributionId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Amount, Is.EqualTo(500.00m));
        Assert.That(retrieved.Contributor, Is.EqualTo("John Doe"));
    }

    /// <summary>
    /// Tests that a contribution can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateContribution_ShouldModifyExistingContribution()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "Test Plan",
            State = "NY",
            CurrentBalance = 5000.00m,
            OpenedDate = DateTime.UtcNow
        };
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            PlanId = plan.PlanId,
            Amount = 300.00m,
            ContributionDate = DateTime.UtcNow,
            IsRecurring = false
        };
        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync();

        // Act
        contribution.Amount = 450.00m;
        contribution.Notes = "Updated amount";
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Contributions.FindAsync(contribution.ContributionId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.Amount, Is.EqualTo(450.00m));
        Assert.That(updated.Notes, Is.EqualTo("Updated amount"));
    }

    /// <summary>
    /// Tests that a contribution can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteContribution_ShouldRemoveContributionFromDatabase()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "Test Plan",
            State = "NY",
            CurrentBalance = 5000.00m,
            OpenedDate = DateTime.UtcNow
        };
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            PlanId = plan.PlanId,
            Amount = 200.00m,
            ContributionDate = DateTime.UtcNow
        };
        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync();

        // Act
        _context.Contributions.Remove(contribution);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Contributions.FindAsync(contribution.ContributionId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Beneficiary Tests

    /// <summary>
    /// Tests that a beneficiary can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateBeneficiary_ShouldAddBeneficiaryToDatabase()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "Test Plan",
            State = "NY",
            CurrentBalance = 5000.00m,
            OpenedDate = DateTime.UtcNow
        };
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        var beneficiary = new Beneficiary
        {
            BeneficiaryId = Guid.NewGuid(),
            PlanId = plan.PlanId,
            FirstName = "Emma",
            LastName = "Smith",
            DateOfBirth = DateTime.UtcNow.AddYears(-10),
            Relationship = "Daughter",
            ExpectedCollegeStartYear = 2033,
            IsPrimary = true
        };

        // Act
        _context.Beneficiaries.Add(beneficiary);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Beneficiaries.FindAsync(beneficiary.BeneficiaryId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.FirstName, Is.EqualTo("Emma"));
        Assert.That(retrieved.LastName, Is.EqualTo("Smith"));
        Assert.That(retrieved.IsPrimary, Is.True);
    }

    /// <summary>
    /// Tests that a beneficiary can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateBeneficiary_ShouldModifyExistingBeneficiary()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "Test Plan",
            State = "NY",
            CurrentBalance = 5000.00m,
            OpenedDate = DateTime.UtcNow
        };
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        var beneficiary = new Beneficiary
        {
            BeneficiaryId = Guid.NewGuid(),
            PlanId = plan.PlanId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.UtcNow.AddYears(-12),
            IsPrimary = false
        };
        _context.Beneficiaries.Add(beneficiary);
        await _context.SaveChangesAsync();

        // Act
        beneficiary.IsPrimary = true;
        beneficiary.ExpectedCollegeStartYear = 2031;
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Beneficiaries.FindAsync(beneficiary.BeneficiaryId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.IsPrimary, Is.True);
        Assert.That(updated.ExpectedCollegeStartYear, Is.EqualTo(2031));
    }

    /// <summary>
    /// Tests that a beneficiary can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteBeneficiary_ShouldRemoveBeneficiaryFromDatabase()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "Test Plan",
            State = "NY",
            CurrentBalance = 5000.00m,
            OpenedDate = DateTime.UtcNow
        };
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        var beneficiary = new Beneficiary
        {
            BeneficiaryId = Guid.NewGuid(),
            PlanId = plan.PlanId,
            FirstName = "To",
            LastName = "Delete",
            DateOfBirth = DateTime.UtcNow.AddYears(-10)
        };
        _context.Beneficiaries.Add(beneficiary);
        await _context.SaveChangesAsync();

        // Act
        _context.Beneficiaries.Remove(beneficiary);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Beneficiaries.FindAsync(beneficiary.BeneficiaryId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Projection Tests

    /// <summary>
    /// Tests that a projection can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateProjection_ShouldAddProjectionToDatabase()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "Test Plan",
            State = "NY",
            CurrentBalance = 5000.00m,
            OpenedDate = DateTime.UtcNow
        };
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        var projection = new Projection
        {
            ProjectionId = Guid.NewGuid(),
            PlanId = plan.PlanId,
            Name = "Conservative Projection",
            CurrentSavings = 10000.00m,
            MonthlyContribution = 500.00m,
            ExpectedReturnRate = 5.0m,
            YearsUntilCollege = 10,
            TargetGoal = 100000.00m,
            ProjectedBalance = 85000.00m,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Projections.Add(projection);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Projections.FindAsync(projection.ProjectionId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Name, Is.EqualTo("Conservative Projection"));
        Assert.That(retrieved.ExpectedReturnRate, Is.EqualTo(5.0m));
    }

    /// <summary>
    /// Tests that a projection can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateProjection_ShouldModifyExistingProjection()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "Test Plan",
            State = "NY",
            CurrentBalance = 5000.00m,
            OpenedDate = DateTime.UtcNow
        };
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        var projection = new Projection
        {
            ProjectionId = Guid.NewGuid(),
            PlanId = plan.PlanId,
            Name = "Original",
            CurrentSavings = 5000.00m,
            MonthlyContribution = 300.00m,
            ExpectedReturnRate = 6.0m,
            YearsUntilCollege = 8,
            TargetGoal = 80000.00m,
            ProjectedBalance = 60000.00m,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projections.Add(projection);
        await _context.SaveChangesAsync();

        // Act
        projection.MonthlyContribution = 500.00m;
        projection.ProjectedBalance = 75000.00m;
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Projections.FindAsync(projection.ProjectionId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.MonthlyContribution, Is.EqualTo(500.00m));
        Assert.That(updated.ProjectedBalance, Is.EqualTo(75000.00m));
    }

    /// <summary>
    /// Tests that a projection can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteProjection_ShouldRemoveProjectionFromDatabase()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "Test Plan",
            State = "NY",
            CurrentBalance = 5000.00m,
            OpenedDate = DateTime.UtcNow
        };
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        var projection = new Projection
        {
            ProjectionId = Guid.NewGuid(),
            PlanId = plan.PlanId,
            Name = "To Delete",
            CurrentSavings = 1000.00m,
            MonthlyContribution = 100.00m,
            ExpectedReturnRate = 5.0m,
            YearsUntilCollege = 5,
            TargetGoal = 20000.00m,
            ProjectedBalance = 15000.00m,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projections.Add(projection);
        await _context.SaveChangesAsync();

        // Act
        _context.Projections.Remove(projection);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Projections.FindAsync(projection.ProjectionId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Relationship Tests

    /// <summary>
    /// Tests that relationships between plans and contributions work correctly.
    /// </summary>
    [Test]
    public async Task PlanContributionRelationship_ShouldLoadCorrectly()
    {
        // Arrange
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = "Test Plan",
            State = "NY",
            CurrentBalance = 5000.00m,
            OpenedDate = DateTime.UtcNow
        };
        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();

        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            PlanId = plan.PlanId,
            Amount = 500.00m,
            ContributionDate = DateTime.UtcNow
        };
        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync();

        // Act
        var loadedContribution = await _context.Contributions
            .Include(c => c.Plan)
            .FirstOrDefaultAsync(c => c.ContributionId == contribution.ContributionId);

        // Assert
        Assert.That(loadedContribution, Is.Not.Null);
        Assert.That(loadedContribution.Plan, Is.Not.Null);
        Assert.That(loadedContribution.Plan.PlanId, Is.EqualTo(plan.PlanId));
    }

    #endregion
}
