// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WoodworkingProjectManager.Infrastructure.Tests;

/// <summary>
/// Unit tests for the WoodworkingProjectManagerContext.
/// </summary>
[TestFixture]
public class WoodworkingProjectManagerContextTests
{
    private WoodworkingProjectManagerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<WoodworkingProjectManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new WoodworkingProjectManagerContext(options);
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
            WoodType = WoodType.Oak,
            EstimatedCost = 100.00m,
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
        Assert.That(retrieved.WoodType, Is.EqualTo(WoodType.Oak));
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
            Description = "Test Description",
            Status = ProjectStatus.Planning,
            WoodType = WoodType.Oak,
            CreatedAt = DateTime.UtcNow,
        };

        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = project.UserId,
            ProjectId = project.ProjectId,
            Name = "Test Material",
            Description = "Test Description",
            Quantity = 10,
            Unit = "board feet",
            Cost = 50.00m,
            Supplier = "Test Supplier",
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
        Assert.That(retrieved.Quantity, Is.EqualTo(10));
        Assert.That(retrieved.ProjectId, Is.EqualTo(project.ProjectId));
    }

    /// <summary>
    /// Tests that Tools can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Tools_CanAddAndRetrieve()
    {
        // Arrange
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Tool",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            PurchasePrice = 200.00m,
            PurchaseDate = DateTime.UtcNow,
            Location = "Test Location",
            Notes = "Test Notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Tools.Add(tool);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Tools.FindAsync(tool.ToolId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Tool"));
        Assert.That(retrieved.Brand, Is.EqualTo("Test Brand"));
        Assert.That(retrieved.Model, Is.EqualTo("Test Model"));
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
            Description = "Test Description",
            Status = ProjectStatus.Planning,
            WoodType = WoodType.Oak,
            CreatedAt = DateTime.UtcNow,
        };

        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = project.UserId,
            ProjectId = project.ProjectId,
            Name = "Test Material",
            Quantity = 10,
            Unit = "board feet",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Projects.Add(project);
        _context.Materials.Add(material);
        await _context.SaveChangesAsync();

        // Act
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        var retrievedMaterial = await _context.Materials.FindAsync(material.MaterialId);

        // Assert
        Assert.That(retrievedMaterial, Is.Null);
    }
}
