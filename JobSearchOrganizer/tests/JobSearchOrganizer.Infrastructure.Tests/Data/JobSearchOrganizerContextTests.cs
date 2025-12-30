// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Infrastructure.Tests;

/// <summary>
/// Unit tests for the JobSearchOrganizerContext.
/// </summary>
[TestFixture]
public class JobSearchOrganizerContextTests
{
    private JobSearchOrganizerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<JobSearchOrganizerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new JobSearchOrganizerContext(options);
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
    /// Tests that Companies can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Companies_CanAddAndRetrieve()
    {
        // Arrange
        var company = new Company
        {
            CompanyId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Company",
            Industry = "Technology",
            Website = "https://testcompany.com",
            Location = "San Francisco, CA",
            IsTargetCompany = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Companies.FindAsync(company.CompanyId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Company"));
        Assert.That(retrieved.Industry, Is.EqualTo("Technology"));
        Assert.That(retrieved.IsTargetCompany, Is.True);
    }

    /// <summary>
    /// Tests that Applications can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Applications_CanAddAndRetrieve()
    {
        // Arrange
        var company = new Company
        {
            CompanyId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Company",
            IsTargetCompany = true,
            CreatedAt = DateTime.UtcNow,
        };

        var application = new Application
        {
            ApplicationId = Guid.NewGuid(),
            UserId = company.UserId,
            CompanyId = company.CompanyId,
            JobTitle = "Software Engineer",
            Status = ApplicationStatus.Applied,
            AppliedDate = DateTime.UtcNow,
            SalaryRange = "$100,000 - $150,000",
            Location = "Remote",
            IsRemote = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Companies.Add(company);
        _context.Applications.Add(application);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Applications.FindAsync(application.ApplicationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.JobTitle, Is.EqualTo("Software Engineer"));
        Assert.That(retrieved.Status, Is.EqualTo(ApplicationStatus.Applied));
        Assert.That(retrieved.IsRemote, Is.True);
    }

    /// <summary>
    /// Tests that Interviews can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Interviews_CanAddAndRetrieve()
    {
        // Arrange
        var company = new Company
        {
            CompanyId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Company",
            IsTargetCompany = true,
            CreatedAt = DateTime.UtcNow,
        };

        var application = new Application
        {
            ApplicationId = Guid.NewGuid(),
            UserId = company.UserId,
            CompanyId = company.CompanyId,
            JobTitle = "Software Engineer",
            Status = ApplicationStatus.Interview,
            AppliedDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var interview = new Interview
        {
            InterviewId = Guid.NewGuid(),
            UserId = company.UserId,
            ApplicationId = application.ApplicationId,
            InterviewType = "Phone Screen",
            ScheduledDateTime = DateTime.UtcNow.AddDays(3),
            DurationMinutes = 30,
            Interviewers = new List<string> { "John Doe" },
            Location = "Phone Call",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Companies.Add(company);
        _context.Applications.Add(application);
        _context.Interviews.Add(interview);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Interviews.FindAsync(interview.InterviewId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.InterviewType, Is.EqualTo("Phone Screen"));
        Assert.That(retrieved.DurationMinutes, Is.EqualTo(30));
        Assert.That(retrieved.IsCompleted, Is.False);
    }

    /// <summary>
    /// Tests that Offers can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Offers_CanAddAndRetrieve()
    {
        // Arrange
        var company = new Company
        {
            CompanyId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Company",
            IsTargetCompany = true,
            CreatedAt = DateTime.UtcNow,
        };

        var application = new Application
        {
            ApplicationId = Guid.NewGuid(),
            UserId = company.UserId,
            CompanyId = company.CompanyId,
            JobTitle = "Software Engineer",
            Status = ApplicationStatus.Offer,
            AppliedDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var offer = new Offer
        {
            OfferId = Guid.NewGuid(),
            UserId = company.UserId,
            ApplicationId = application.ApplicationId,
            Salary = 150000m,
            Currency = "USD",
            Bonus = 20000m,
            OfferDate = DateTime.UtcNow,
            ExpirationDate = DateTime.UtcNow.AddDays(7),
            IsAccepted = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Companies.Add(company);
        _context.Applications.Add(application);
        _context.Offers.Add(offer);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Offers.FindAsync(offer.OfferId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Salary, Is.EqualTo(150000m));
        Assert.That(retrieved.Bonus, Is.EqualTo(20000m));
        Assert.That(retrieved.IsAccepted, Is.False);
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var company = new Company
        {
            CompanyId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Company",
            IsTargetCompany = true,
            CreatedAt = DateTime.UtcNow,
        };

        var application = new Application
        {
            ApplicationId = Guid.NewGuid(),
            UserId = company.UserId,
            CompanyId = company.CompanyId,
            JobTitle = "Software Engineer",
            Status = ApplicationStatus.Applied,
            AppliedDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Companies.Add(company);
        _context.Applications.Add(application);
        await _context.SaveChangesAsync();

        // Act
        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();

        var retrievedApplication = await _context.Applications.FindAsync(application.ApplicationId);

        // Assert
        Assert.That(retrievedApplication, Is.Null);
    }
}
