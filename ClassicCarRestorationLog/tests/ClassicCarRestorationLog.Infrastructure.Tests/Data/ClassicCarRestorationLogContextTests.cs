// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Infrastructure.Tests.Data;

/// <summary>
/// Contains unit tests for the <see cref="ClassicCarRestorationLogContext"/> class.
/// </summary>
[TestFixture]
public class ClassicCarRestorationLogContextTests
{
    private DbContextOptions<ClassicCarRestorationLogContext> _options = null!;
    private ClassicCarRestorationLogContext _context = null!;
    private Guid _testUserId;

    /// <summary>
    /// Sets up the test context before each test.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _testUserId = Guid.NewGuid();
        _options = new DbContextOptionsBuilder<ClassicCarRestorationLogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ClassicCarRestorationLogContext(_options);
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

    #region Project Tests

    /// <summary>
    /// Tests that a project can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateProject_ShouldAddProjectToDatabase()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Year = 1967,
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            EstimatedBudget = 25000m,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Projects.FindAsync(project.ProjectId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.CarMake, Is.EqualTo("Ford"));
        Assert.That(retrieved.CarModel, Is.EqualTo("Mustang"));
        Assert.That(retrieved.Year, Is.EqualTo(1967));
    }

    /// <summary>
    /// Tests that a project can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateProject_ShouldModifyExistingProject()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        // Act
        project.Phase = ProjectPhase.Repair;
        project.ActualCost = 15000m;
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Projects.FindAsync(project.ProjectId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.Phase, Is.EqualTo(ProjectPhase.Repair));
        Assert.That(updated.ActualCost, Is.EqualTo(15000m));
    }

    /// <summary>
    /// Tests that a project can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteProject_ShouldRemoveProjectFromDatabase()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        // Act
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Projects.FindAsync(project.ProjectId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Part Tests

    /// <summary>
    /// Tests that a part can be created successfully.
    /// </summary>
    [Test]
    public async Task CreatePart_ShouldAddPartToDatabase()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var part = new Part
        {
            PartId = Guid.NewGuid(),
            UserId = _testUserId,
            ProjectId = project.ProjectId,
            Name = "Carburetor",
            PartNumber = "CARB-1967",
            Supplier = "Parts Co.",
            Cost = 150.00m,
            IsInstalled = false,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Parts.Add(part);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Parts.FindAsync(part.PartId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Name, Is.EqualTo("Carburetor"));
        Assert.That(retrieved.Cost, Is.EqualTo(150.00m));
    }

    /// <summary>
    /// Tests that a part can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdatePart_ShouldModifyExistingPart()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var part = new Part
        {
            PartId = Guid.NewGuid(),
            UserId = _testUserId,
            ProjectId = project.ProjectId,
            Name = "Part",
            IsInstalled = false,
            CreatedAt = DateTime.UtcNow
        };
        _context.Parts.Add(part);
        await _context.SaveChangesAsync();

        // Act
        part.IsInstalled = true;
        part.ReceivedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Parts.FindAsync(part.PartId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.IsInstalled, Is.True);
        Assert.That(updated.ReceivedDate, Is.Not.Null);
    }

    /// <summary>
    /// Tests that a part can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeletePart_ShouldRemovePartFromDatabase()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var part = new Part
        {
            PartId = Guid.NewGuid(),
            UserId = _testUserId,
            ProjectId = project.ProjectId,
            Name = "To Delete",
            CreatedAt = DateTime.UtcNow
        };
        _context.Parts.Add(part);
        await _context.SaveChangesAsync();

        // Act
        _context.Parts.Remove(part);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Parts.FindAsync(part.PartId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region WorkLog Tests

    /// <summary>
    /// Tests that a work log can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateWorkLog_ShouldAddWorkLogToDatabase()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var workLog = new WorkLog
        {
            WorkLogId = Guid.NewGuid(),
            UserId = _testUserId,
            ProjectId = project.ProjectId,
            WorkDate = DateTime.UtcNow,
            HoursWorked = 4,
            Description = "Engine work",
            WorkPerformed = "Rebuilt carburetor",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.WorkLogs.Add(workLog);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.WorkLogs.FindAsync(workLog.WorkLogId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Description, Is.EqualTo("Engine work"));
        Assert.That(retrieved.HoursWorked, Is.EqualTo(4));
    }

    /// <summary>
    /// Tests that a work log can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateWorkLog_ShouldModifyExistingWorkLog()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var workLog = new WorkLog
        {
            WorkLogId = Guid.NewGuid(),
            UserId = _testUserId,
            ProjectId = project.ProjectId,
            WorkDate = DateTime.UtcNow,
            HoursWorked = 2,
            Description = "Original",
            CreatedAt = DateTime.UtcNow
        };
        _context.WorkLogs.Add(workLog);
        await _context.SaveChangesAsync();

        // Act
        workLog.HoursWorked = 5;
        workLog.Description = "Updated";
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.WorkLogs.FindAsync(workLog.WorkLogId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.HoursWorked, Is.EqualTo(5));
        Assert.That(updated.Description, Is.EqualTo("Updated"));
    }

    /// <summary>
    /// Tests that a work log can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteWorkLog_ShouldRemoveWorkLogFromDatabase()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var workLog = new WorkLog
        {
            WorkLogId = Guid.NewGuid(),
            UserId = _testUserId,
            ProjectId = project.ProjectId,
            WorkDate = DateTime.UtcNow,
            HoursWorked = 3,
            Description = "To Delete",
            CreatedAt = DateTime.UtcNow
        };
        _context.WorkLogs.Add(workLog);
        await _context.SaveChangesAsync();

        // Act
        _context.WorkLogs.Remove(workLog);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.WorkLogs.FindAsync(workLog.WorkLogId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region PhotoLog Tests

    /// <summary>
    /// Tests that a photo log can be created successfully.
    /// </summary>
    [Test]
    public async Task CreatePhotoLog_ShouldAddPhotoLogToDatabase()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var photoLog = new PhotoLog
        {
            PhotoLogId = Guid.NewGuid(),
            UserId = _testUserId,
            ProjectId = project.ProjectId,
            PhotoDate = DateTime.UtcNow,
            Description = "Engine bay photo",
            PhotoUrl = "/photos/engine.jpg",
            Phase = ProjectPhase.Planning,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.PhotoLogs.Add(photoLog);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.PhotoLogs.FindAsync(photoLog.PhotoLogId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Description, Is.EqualTo("Engine bay photo"));
        Assert.That(retrieved.PhotoUrl, Is.EqualTo("/photos/engine.jpg"));
    }

    /// <summary>
    /// Tests that a photo log can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdatePhotoLog_ShouldModifyExistingPhotoLog()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var photoLog = new PhotoLog
        {
            PhotoLogId = Guid.NewGuid(),
            UserId = _testUserId,
            ProjectId = project.ProjectId,
            PhotoDate = DateTime.UtcNow,
            Description = "Original",
            CreatedAt = DateTime.UtcNow
        };
        _context.PhotoLogs.Add(photoLog);
        await _context.SaveChangesAsync();

        // Act
        photoLog.Description = "Updated";
        photoLog.Phase = ProjectPhase.Repair;
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.PhotoLogs.FindAsync(photoLog.PhotoLogId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.Description, Is.EqualTo("Updated"));
        Assert.That(updated.Phase, Is.EqualTo(ProjectPhase.Repair));
    }

    /// <summary>
    /// Tests that a photo log can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeletePhotoLog_ShouldRemovePhotoLogFromDatabase()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var photoLog = new PhotoLog
        {
            PhotoLogId = Guid.NewGuid(),
            UserId = _testUserId,
            ProjectId = project.ProjectId,
            PhotoDate = DateTime.UtcNow,
            Description = "To Delete",
            CreatedAt = DateTime.UtcNow
        };
        _context.PhotoLogs.Add(photoLog);
        await _context.SaveChangesAsync();

        // Act
        _context.PhotoLogs.Remove(photoLog);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.PhotoLogs.FindAsync(photoLog.PhotoLogId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Relationship Tests

    /// <summary>
    /// Tests that relationships between projects and parts work correctly.
    /// </summary>
    [Test]
    public async Task ProjectPartRelationship_ShouldLoadCorrectly()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = _testUserId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Phase = ProjectPhase.Planning,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var part = new Part
        {
            PartId = Guid.NewGuid(),
            UserId = _testUserId,
            ProjectId = project.ProjectId,
            Name = "Test Part",
            CreatedAt = DateTime.UtcNow
        };
        _context.Parts.Add(part);
        await _context.SaveChangesAsync();

        // Act
        var loadedProject = await _context.Projects
            .Include(p => p.Parts)
            .FirstOrDefaultAsync(p => p.ProjectId == project.ProjectId);

        // Assert
        Assert.That(loadedProject, Is.Not.Null);
        Assert.That(loadedProject.Parts, Has.Count.EqualTo(1));
        Assert.That(loadedProject.Parts.First().Name, Is.EqualTo("Test Part"));
    }

    #endregion
}
