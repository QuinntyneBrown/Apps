// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Infrastructure.Tests;

/// <summary>
/// Unit tests for the ProfessionalNetworkCRMContext.
/// </summary>
[TestFixture]
public class ProfessionalNetworkCRMContextTests
{
    private ProfessionalNetworkCRMContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ProfessionalNetworkCRMContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ProfessionalNetworkCRMContext(options);
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
    /// Tests that Contacts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Contacts_CanAddAndRetrieve()
    {
        // Arrange
        var contact = new Contact
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            ContactType = ContactType.Colleague,
            Company = "Test Company",
            JobTitle = "Software Engineer",
            Email = "john.doe@test.com",
            Phone = "+1-555-0100",
            Location = "Test City",
            Tags = new List<string> { "test", "colleague" },
            DateMet = DateTime.UtcNow,
            IsPriority = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Contacts.FindAsync(contact.ContactId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.FirstName, Is.EqualTo("John"));
        Assert.That(retrieved.LastName, Is.EqualTo("Doe"));
        Assert.That(retrieved.ContactType, Is.EqualTo(ContactType.Colleague));
    }

    /// <summary>
    /// Tests that Interactions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Interactions_CanAddAndRetrieve()
    {
        // Arrange
        var contact = new Contact
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Smith",
            ContactType = ContactType.Client,
            DateMet = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var interaction = new Interaction
        {
            InteractionId = Guid.NewGuid(),
            UserId = contact.UserId,
            ContactId = contact.ContactId,
            InteractionType = "Email",
            InteractionDate = DateTime.UtcNow,
            Subject = "Test subject",
            Notes = "Test notes",
            Outcome = "Test outcome",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Contacts.Add(contact);
        _context.Interactions.Add(interaction);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Interactions.FindAsync(interaction.InteractionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.InteractionType, Is.EqualTo("Email"));
        Assert.That(retrieved.Subject, Is.EqualTo("Test subject"));
    }

    /// <summary>
    /// Tests that FollowUps can be added and retrieved.
    /// </summary>
    [Test]
    public async Task FollowUps_CanAddAndRetrieve()
    {
        // Arrange
        var contact = new Contact
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "Bob",
            LastName = "Wilson",
            ContactType = ContactType.Mentor,
            DateMet = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var followUp = new FollowUp
        {
            FollowUpId = Guid.NewGuid(),
            UserId = contact.UserId,
            ContactId = contact.ContactId,
            Description = "Test follow-up",
            DueDate = DateTime.UtcNow.AddDays(7),
            Priority = "High",
            IsCompleted = false,
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Contacts.Add(contact);
        _context.FollowUps.Add(followUp);
        await _context.SaveChangesAsync();

        var retrieved = await _context.FollowUps.FindAsync(followUp.FollowUpId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Test follow-up"));
        Assert.That(retrieved.Priority, Is.EqualTo("High"));
        Assert.That(retrieved.IsCompleted, Is.False);
    }

    /// <summary>
    /// Tests that cascade delete works for Interactions when Contact is deleted.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedInteractions()
    {
        // Arrange
        var contact = new Contact
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "Alice",
            LastName = "Brown",
            ContactType = ContactType.Client,
            DateMet = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var interaction = new Interaction
        {
            InteractionId = Guid.NewGuid(),
            UserId = contact.UserId,
            ContactId = contact.ContactId,
            InteractionType = "Call",
            InteractionDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Contacts.Add(contact);
        _context.Interactions.Add(interaction);
        await _context.SaveChangesAsync();

        // Act
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();

        var retrievedInteraction = await _context.Interactions.FindAsync(interaction.InteractionId);

        // Assert
        Assert.That(retrievedInteraction, Is.Null);
    }

    /// <summary>
    /// Tests that cascade delete works for FollowUps when Contact is deleted.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedFollowUps()
    {
        // Arrange
        var contact = new Contact
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "Charlie",
            LastName = "Davis",
            ContactType = ContactType.Mentor,
            DateMet = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var followUp = new FollowUp
        {
            FollowUpId = Guid.NewGuid(),
            UserId = contact.UserId,
            ContactId = contact.ContactId,
            Description = "Test follow-up",
            DueDate = DateTime.UtcNow.AddDays(7),
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Contacts.Add(contact);
        _context.FollowUps.Add(followUp);
        await _context.SaveChangesAsync();

        // Act
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();

        var retrievedFollowUp = await _context.FollowUps.FindAsync(followUp.FollowUpId);

        // Assert
        Assert.That(retrievedFollowUp, Is.Null);
    }
}
