// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Infrastructure.Tests;

/// <summary>
/// Unit tests for the HomeImprovementProjectManagerContext.
/// </summary>
[TestFixture]
public class HomeImprovementProjectManagerContextTests
{
    private HomeImprovementProjectManagerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HomeImprovementProjectManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new HomeImprovementProjectManagerContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Projects can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Projects_CanAddAndRetrieve()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Description = "Test Description",
            Status = ProjectStatus.Planning,
            EstimatedCost = 10000m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Projects.FindAsync(project.ProjectId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Project"));
        Assert.That(retrieved.Status, Is.EqualTo(ProjectStatus.Planning));
    }

    /// <summary>
    /// Tests that Budgets can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Budgets_CanAddAndRetrieve()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Status = ProjectStatus.Planning,
            CreatedAt = DateTime.UtcNow,
        };

        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = project.ProjectId,
            Category = "Materials",
            AllocatedAmount = 5000m,
            SpentAmount = 2500m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Projects.Add(project);
        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Budgets.FindAsync(budget.BudgetId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Category, Is.EqualTo("Materials"));
        Assert.That(retrieved.AllocatedAmount, Is.EqualTo(5000m));
    }

    /// <summary>
    /// Tests that Contractors can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Contractors_CanAddAndRetrieve()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Status = ProjectStatus.Planning,
            CreatedAt = DateTime.UtcNow,
        };

        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            ProjectId = project.ProjectId,
            Name = "Test Contractor",
            Trade = "Plumbing",
            PhoneNumber = "555-1234",
            Rating = 5,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Projects.Add(project);
        _context.Contractors.Add(contractor);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Contractors.FindAsync(contractor.ContractorId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Contractor"));
        Assert.That(retrieved.Trade, Is.EqualTo("Plumbing"));
    }

    /// <summary>
    /// Tests that Materials can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Materials_CanAddAndRetrieve()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Status = ProjectStatus.Planning,
            CreatedAt = DateTime.UtcNow,
        };

        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = project.ProjectId,
            Name = "Test Material",
            Quantity = 100,
            Unit = "sq ft",
            UnitCost = 25m,
            TotalCost = 2500m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Projects.Add(project);
        _context.Materials.Add(material);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Materials.FindAsync(material.MaterialId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Material"));
        Assert.That(retrieved.Quantity, Is.EqualTo(100));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Status = ProjectStatus.Planning,
            CreatedAt = DateTime.UtcNow,
        };

        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = project.ProjectId,
            Category = "Materials",
            AllocatedAmount = 1000m,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Projects.Add(project);
        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync();

        // Act
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        var retrievedBudget = await _context.Budgets.FindAsync(budget.BudgetId);

        // Assert
        Assert.That(retrievedBudget, Is.Null);
    }
}
