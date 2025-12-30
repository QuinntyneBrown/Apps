// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DigitalLegacyPlanner.Infrastructure.Tests;

/// <summary>
/// Unit tests for the DigitalLegacyPlannerContext.
/// </summary>
[TestFixture]
public class DigitalLegacyPlannerContextTests
{
    private DigitalLegacyPlannerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DigitalLegacyPlannerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DigitalLegacyPlannerContext(options);
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
    /// Tests that DigitalAccounts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task DigitalAccounts_CanAddAndRetrieve()
    {
        // Arrange
        var account = new DigitalAccount
        {
            DigitalAccountId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            AccountType = AccountType.Email,
            AccountName = "Primary Gmail",
            Username = "user@gmail.com",
            PasswordHint = "Stored in password manager",
            Url = "https://gmail.com",
            DesiredAction = "Transfer to trusted contact",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Accounts.FindAsync(account.DigitalAccountId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.AccountName, Is.EqualTo("Primary Gmail"));
        Assert.That(retrieved.AccountType, Is.EqualTo(AccountType.Email));
    }

    /// <summary>
    /// Tests that LegacyInstructions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task LegacyInstructions_CanAddAndRetrieve()
    {
        // Arrange
        var instruction = new LegacyInstruction
        {
            LegacyInstructionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Social Media Account Handling",
            Content = "Please memorialize my Facebook account.",
            Category = "Digital Accounts",
            Priority = 2,
            AssignedTo = "John Doe",
            ExecutionTiming = "Within 2 weeks",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Instructions.Add(instruction);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Instructions.FindAsync(instruction.LegacyInstructionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Social Media Account Handling"));
        Assert.That(retrieved.Priority, Is.EqualTo(2));
    }

    /// <summary>
    /// Tests that TrustedContacts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task TrustedContacts_CanAddAndRetrieve()
    {
        // Arrange
        var contact = new TrustedContact
        {
            TrustedContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FullName = "Jane Smith",
            Relationship = "Spouse",
            Email = "jane.smith@email.com",
            PhoneNumber = "+1-555-0101",
            Role = "Primary Executor",
            IsPrimaryContact = true,
            IsNotified = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Contacts.FindAsync(contact.TrustedContactId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.FullName, Is.EqualTo("Jane Smith"));
        Assert.That(retrieved.IsPrimaryContact, Is.True);
    }

    /// <summary>
    /// Tests that LegacyDocuments can be added and retrieved.
    /// </summary>
    [Test]
    public async Task LegacyDocuments_CanAddAndRetrieve()
    {
        // Arrange
        var document = new LegacyDocument
        {
            LegacyDocumentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Last Will and Testament",
            DocumentType = "Legal - Will",
            FilePath = "/documents/will_2024.pdf",
            Description = "Most recent version of will",
            PhysicalLocation = "Safe deposit box at First National Bank",
            AccessGrantedTo = "Jane Smith",
            IsEncrypted = true,
            LastReviewedAt = DateTime.UtcNow.AddMonths(-3),
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Documents.Add(document);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Documents.FindAsync(document.LegacyDocumentId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Last Will and Testament"));
        Assert.That(retrieved.IsEncrypted, Is.True);
    }

    /// <summary>
    /// Tests that multiple accounts of different types can be stored.
    /// </summary>
    [Test]
    public async Task Accounts_CanStoreDifferentAccountTypes()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var emailAccount = new DigitalAccount
        {
            DigitalAccountId = Guid.NewGuid(),
            UserId = userId,
            AccountType = AccountType.Email,
            AccountName = "Email",
            Username = "user@email.com",
            CreatedAt = DateTime.UtcNow,
        };

        var socialAccount = new DigitalAccount
        {
            DigitalAccountId = Guid.NewGuid(),
            UserId = userId,
            AccountType = AccountType.SocialMedia,
            AccountName = "Facebook",
            Username = "user.profile",
            CreatedAt = DateTime.UtcNow,
        };

        var financialAccount = new DigitalAccount
        {
            DigitalAccountId = Guid.NewGuid(),
            UserId = userId,
            AccountType = AccountType.Financial,
            AccountName = "Bank Account",
            Username = "customer123",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Accounts.AddRange(emailAccount, socialAccount, financialAccount);
        await _context.SaveChangesAsync();

        var accounts = await _context.Accounts
            .Where(a => a.UserId == userId)
            .ToListAsync();

        // Assert
        Assert.That(accounts, Has.Count.EqualTo(3));
        Assert.That(accounts.Any(a => a.AccountType == AccountType.Email), Is.True);
        Assert.That(accounts.Any(a => a.AccountType == AccountType.SocialMedia), Is.True);
        Assert.That(accounts.Any(a => a.AccountType == AccountType.Financial), Is.True);
    }

    /// <summary>
    /// Tests that primary contact can be identified.
    /// </summary>
    [Test]
    public async Task Contacts_CanIdentifyPrimaryContact()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var primaryContact = new TrustedContact
        {
            TrustedContactId = Guid.NewGuid(),
            UserId = userId,
            FullName = "Primary Contact",
            Relationship = "Spouse",
            Email = "primary@email.com",
            IsPrimaryContact = true,
            IsNotified = false,
            CreatedAt = DateTime.UtcNow,
        };

        var secondaryContact = new TrustedContact
        {
            TrustedContactId = Guid.NewGuid(),
            UserId = userId,
            FullName = "Secondary Contact",
            Relationship = "Son",
            Email = "secondary@email.com",
            IsPrimaryContact = false,
            IsNotified = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Contacts.AddRange(primaryContact, secondaryContact);
        await _context.SaveChangesAsync();

        // Act
        var primary = await _context.Contacts
            .Where(c => c.UserId == userId && c.IsPrimaryContact)
            .FirstOrDefaultAsync();

        // Assert
        Assert.That(primary, Is.Not.Null);
        Assert.That(primary!.FullName, Is.EqualTo("Primary Contact"));
    }

    /// <summary>
    /// Tests that instructions can be sorted by priority.
    /// </summary>
    [Test]
    public async Task Instructions_CanBeSortedByPriority()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var lowPriority = new LegacyInstruction
        {
            LegacyInstructionId = Guid.NewGuid(),
            UserId = userId,
            Title = "Low Priority Task",
            Content = "This can wait",
            Priority = 3,
            CreatedAt = DateTime.UtcNow,
        };

        var highPriority = new LegacyInstruction
        {
            LegacyInstructionId = Guid.NewGuid(),
            UserId = userId,
            Title = "High Priority Task",
            Content = "Do this first",
            Priority = 1,
            CreatedAt = DateTime.UtcNow,
        };

        var mediumPriority = new LegacyInstruction
        {
            LegacyInstructionId = Guid.NewGuid(),
            UserId = userId,
            Title = "Medium Priority Task",
            Content = "Do this second",
            Priority = 2,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Instructions.AddRange(lowPriority, highPriority, mediumPriority);
        await _context.SaveChangesAsync();

        // Act
        var sortedInstructions = await _context.Instructions
            .Where(i => i.UserId == userId)
            .OrderBy(i => i.Priority)
            .ToListAsync();

        // Assert
        Assert.That(sortedInstructions, Has.Count.EqualTo(3));
        Assert.That(sortedInstructions[0].Priority, Is.EqualTo(1));
        Assert.That(sortedInstructions[1].Priority, Is.EqualTo(2));
        Assert.That(sortedInstructions[2].Priority, Is.EqualTo(3));
    }

    /// <summary>
    /// Tests that documents can be queried by encryption status.
    /// </summary>
    [Test]
    public async Task Documents_CanBeQueriedByEncryptionStatus()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var encryptedDoc = new LegacyDocument
        {
            LegacyDocumentId = Guid.NewGuid(),
            UserId = userId,
            Title = "Encrypted Document",
            DocumentType = "Legal",
            IsEncrypted = true,
            CreatedAt = DateTime.UtcNow,
        };

        var unencryptedDoc = new LegacyDocument
        {
            LegacyDocumentId = Guid.NewGuid(),
            UserId = userId,
            Title = "Unencrypted Document",
            DocumentType = "General",
            IsEncrypted = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Documents.AddRange(encryptedDoc, unencryptedDoc);
        await _context.SaveChangesAsync();

        // Act
        var encryptedDocuments = await _context.Documents
            .Where(d => d.UserId == userId && d.IsEncrypted)
            .ToListAsync();

        // Assert
        Assert.That(encryptedDocuments, Has.Count.EqualTo(1));
        Assert.That(encryptedDocuments[0].Title, Is.EqualTo("Encrypted Document"));
    }
}
