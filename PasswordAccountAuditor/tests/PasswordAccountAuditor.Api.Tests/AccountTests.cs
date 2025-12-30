// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PasswordAccountAuditor.Api.Tests;

namespace PasswordAccountAuditor.Api.Tests;

[TestFixture]
public class AccountTests
{
    [Test]
    public void AccountDto_ToDto_MapsCorrectly()
    {
        // Arrange
        var account = new Core.Account
        {
            AccountId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            AccountName = "Test Account",
            Username = "testuser",
            WebsiteUrl = "https://example.com",
            Category = AccountCategory.Banking,
            SecurityLevel = SecurityLevel.High,
            HasTwoFactorAuth = true,
            LastPasswordChange = DateTime.UtcNow.AddDays(-30),
            LastAccessDate = DateTime.UtcNow.AddDays(-1),
            Notes = "Test notes",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = account.ToDto();

        // Assert
        Assert.That(dto.AccountId, Is.EqualTo(account.AccountId));
        Assert.That(dto.UserId, Is.EqualTo(account.UserId));
        Assert.That(dto.AccountName, Is.EqualTo(account.AccountName));
        Assert.That(dto.Username, Is.EqualTo(account.Username));
        Assert.That(dto.WebsiteUrl, Is.EqualTo(account.WebsiteUrl));
        Assert.That(dto.Category, Is.EqualTo(account.Category));
        Assert.That(dto.SecurityLevel, Is.EqualTo(account.SecurityLevel));
        Assert.That(dto.HasTwoFactorAuth, Is.EqualTo(account.HasTwoFactorAuth));
        Assert.That(dto.IsActive, Is.EqualTo(account.IsActive));
    }

    [Test]
    public async Task CreateAccountCommand_CreatesAccount()
    {
        // Arrange
        var mockContext = TestHelpers.CreateMockContext();
        var handler = new CreateAccountCommandHandler(mockContext.Object);
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountName = "Test Account",
            Username = "testuser",
            Category = AccountCategory.Email,
            HasTwoFactorAuth = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.AccountName, Is.EqualTo(command.AccountName));
        Assert.That(result.Username, Is.EqualTo(command.Username));
        Assert.That(result.HasTwoFactorAuth, Is.True);
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAccountCommand_UpdatesAccount()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var accounts = new List<Core.Account>
        {
            new Core.Account
            {
                AccountId = accountId,
                UserId = Guid.NewGuid(),
                AccountName = "Old Name",
                Username = "olduser",
                Category = AccountCategory.SocialMedia,
                HasTwoFactorAuth = false,
                IsActive = true
            }
        };

        var mockContext = TestHelpers.CreateMockContext(accounts);
        var handler = new UpdateAccountCommandHandler(mockContext.Object);
        var command = new UpdateAccountCommand
        {
            AccountId = accountId,
            AccountName = "New Name",
            Username = "newuser",
            Category = AccountCategory.Banking,
            HasTwoFactorAuth = true,
            IsActive = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.AccountName, Is.EqualTo("New Name"));
        Assert.That(result.Username, Is.EqualTo("newuser"));
        Assert.That(result.HasTwoFactorAuth, Is.True);
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void UpdateAccountCommand_ThrowsException_WhenAccountNotFound()
    {
        // Arrange
        var mockContext = TestHelpers.CreateMockContext();
        var handler = new UpdateAccountCommandHandler(mockContext.Object);
        var command = new UpdateAccountCommand
        {
            AccountId = Guid.NewGuid(),
            AccountName = "Test",
            Username = "test",
            Category = AccountCategory.Email,
            HasTwoFactorAuth = false,
            IsActive = true
        };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task DeleteAccountCommand_DeletesAccount()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var accounts = new List<Core.Account>
        {
            new Core.Account
            {
                AccountId = accountId,
                UserId = Guid.NewGuid(),
                AccountName = "Test",
                Username = "test",
                Category = AccountCategory.Email,
                IsActive = true
            }
        };

        var mockContext = TestHelpers.CreateMockContext(accounts);
        var handler = new DeleteAccountCommandHandler(mockContext.Object);
        var command = new DeleteAccountCommand(accountId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void DeleteAccountCommand_ThrowsException_WhenAccountNotFound()
    {
        // Arrange
        var mockContext = TestHelpers.CreateMockContext();
        var handler = new DeleteAccountCommandHandler(mockContext.Object);
        var command = new DeleteAccountCommand(Guid.NewGuid());

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task GetAccountsQuery_ReturnsAllAccounts()
    {
        // Arrange
        var accounts = new List<Core.Account>
        {
            new Core.Account
            {
                AccountId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                AccountName = "Account 1",
                Username = "user1",
                Category = AccountCategory.Email,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Core.Account
            {
                AccountId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                AccountName = "Account 2",
                Username = "user2",
                Category = AccountCategory.Banking,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        var mockContext = TestHelpers.CreateMockContext(accounts);
        var handler = new GetAccountsQueryHandler(mockContext.Object);
        var query = new GetAccountsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetAccountByIdQuery_ReturnsAccount_WhenExists()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var accounts = new List<Core.Account>
        {
            new Core.Account
            {
                AccountId = accountId,
                UserId = Guid.NewGuid(),
                AccountName = "Test Account",
                Username = "testuser",
                Category = AccountCategory.Email,
                IsActive = true
            }
        };

        var mockContext = TestHelpers.CreateMockContext(accounts);
        var handler = new GetAccountByIdQueryHandler(mockContext.Object);
        var query = new GetAccountByIdQuery(accountId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.AccountId, Is.EqualTo(accountId));
        Assert.That(result.AccountName, Is.EqualTo("Test Account"));
    }

    [Test]
    public async Task GetAccountByIdQuery_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var mockContext = TestHelpers.CreateMockContext();
        var handler = new GetAccountByIdQueryHandler(mockContext.Object);
        var query = new GetAccountByIdQuery(Guid.NewGuid());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }
}
