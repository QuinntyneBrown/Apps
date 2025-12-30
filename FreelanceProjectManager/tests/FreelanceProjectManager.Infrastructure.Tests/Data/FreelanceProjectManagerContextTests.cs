// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FreelanceProjectManager.Infrastructure.Tests;

/// <summary>
/// Unit tests for the FreelanceProjectManagerContext.
/// </summary>
[TestFixture]
public class FreelanceProjectManagerContextTests
{
    private FreelanceProjectManagerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FreelanceProjectManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FreelanceProjectManagerContext(options);
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
    /// Tests that Clients can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Clients_CanAddAndRetrieve()
    {
        // Arrange
        var client = new Client
        {
            ClientId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Client",
            CompanyName = "Test Company",
            Email = "test@example.com",
            Phone = "+1-555-0123",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Clients.FindAsync(client.ClientId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Client"));
        Assert.That(retrieved.CompanyName, Is.EqualTo("Test Company"));
        Assert.That(retrieved.IsActive, Is.True);
    }

    /// <summary>
    /// Tests that Projects can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Projects_CanAddAndRetrieve()
    {
        // Arrange
        var client = new Client
        {
            ClientId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Client",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = client.UserId,
            ClientId = client.ClientId,
            Name = "Test Project",
            Description = "A test project",
            Status = ProjectStatus.InProgress,
            StartDate = DateTime.UtcNow,
            HourlyRate = 150.00m,
            Currency = "USD",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Clients.Add(client);
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Projects.FindAsync(project.ProjectId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Project"));
        Assert.That(retrieved.Status, Is.EqualTo(ProjectStatus.InProgress));
        Assert.That(retrieved.HourlyRate, Is.EqualTo(150.00m));
    }

    /// <summary>
    /// Tests that TimeEntries can be added and retrieved.
    /// </summary>
    [Test]
    public async Task TimeEntries_CanAddAndRetrieve()
    {
        // Arrange
        var client = new Client
        {
            ClientId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Client",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = client.UserId,
            ClientId = client.ClientId,
            Name = "Test Project",
            Description = "Test",
            Status = ProjectStatus.InProgress,
            StartDate = DateTime.UtcNow,
            Currency = "USD",
            CreatedAt = DateTime.UtcNow,
        };

        var timeEntry = new TimeEntry
        {
            TimeEntryId = Guid.NewGuid(),
            UserId = client.UserId,
            ProjectId = project.ProjectId,
            WorkDate = DateTime.UtcNow,
            Hours = 8.5m,
            Description = "Development work",
            IsBillable = true,
            IsInvoiced = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Clients.Add(client);
        _context.Projects.Add(project);
        _context.TimeEntries.Add(timeEntry);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TimeEntries.FindAsync(timeEntry.TimeEntryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Hours, Is.EqualTo(8.5m));
        Assert.That(retrieved.Description, Is.EqualTo("Development work"));
        Assert.That(retrieved.IsBillable, Is.True);
    }

    /// <summary>
    /// Tests that Invoices can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Invoices_CanAddAndRetrieve()
    {
        // Arrange
        var client = new Client
        {
            ClientId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Client",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var invoice = new Invoice
        {
            InvoiceId = Guid.NewGuid(),
            UserId = client.UserId,
            ClientId = client.ClientId,
            InvoiceNumber = "INV-2024-TEST",
            InvoiceDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(30),
            TotalAmount = 5000.00m,
            Currency = "USD",
            Status = "Draft",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Clients.Add(client);
        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Invoices.FindAsync(invoice.InvoiceId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.InvoiceNumber, Is.EqualTo("INV-2024-TEST"));
        Assert.That(retrieved.TotalAmount, Is.EqualTo(5000.00m));
        Assert.That(retrieved.Status, Is.EqualTo("Draft"));
    }

    /// <summary>
    /// Tests that cascade delete works for Project and TimeEntries.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedTimeEntries()
    {
        // Arrange
        var client = new Client
        {
            ClientId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Client",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = client.UserId,
            ClientId = client.ClientId,
            Name = "Test Project",
            Description = "Test",
            Status = ProjectStatus.InProgress,
            StartDate = DateTime.UtcNow,
            Currency = "USD",
            CreatedAt = DateTime.UtcNow,
        };

        var timeEntry = new TimeEntry
        {
            TimeEntryId = Guid.NewGuid(),
            UserId = client.UserId,
            ProjectId = project.ProjectId,
            WorkDate = DateTime.UtcNow,
            Hours = 5.0m,
            Description = "Test work",
            IsBillable = true,
            IsInvoiced = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Clients.Add(client);
        _context.Projects.Add(project);
        _context.TimeEntries.Add(timeEntry);
        await _context.SaveChangesAsync();

        // Act
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        var retrievedTimeEntry = await _context.TimeEntries.FindAsync(timeEntry.TimeEntryId);

        // Assert
        Assert.That(retrievedTimeEntry, Is.Null);
    }

    /// <summary>
    /// Tests that deleting a project sets invoice ProjectId to null.
    /// </summary>
    [Test]
    public async Task DeleteProject_SetsInvoiceProjectIdToNull()
    {
        // Arrange
        var client = new Client
        {
            ClientId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Client",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = client.UserId,
            ClientId = client.ClientId,
            Name = "Test Project",
            Description = "Test",
            Status = ProjectStatus.Completed,
            StartDate = DateTime.UtcNow,
            Currency = "USD",
            CreatedAt = DateTime.UtcNow,
        };

        var invoice = new Invoice
        {
            InvoiceId = Guid.NewGuid(),
            UserId = client.UserId,
            ClientId = client.ClientId,
            ProjectId = project.ProjectId,
            InvoiceNumber = "INV-TEST-001",
            InvoiceDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(30),
            TotalAmount = 1000.00m,
            Currency = "USD",
            Status = "Paid",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Clients.Add(client);
        _context.Projects.Add(project);
        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        // Act
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        var retrievedInvoice = await _context.Invoices.FindAsync(invoice.InvoiceId);

        // Assert
        Assert.That(retrievedInvoice, Is.Not.Null);
        Assert.That(retrievedInvoice!.ProjectId, Is.Null);
    }
}
