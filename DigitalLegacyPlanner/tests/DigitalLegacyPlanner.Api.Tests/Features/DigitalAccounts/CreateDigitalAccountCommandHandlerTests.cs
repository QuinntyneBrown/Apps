// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Api.Features.DigitalAccounts.Commands;
using DigitalLegacyPlanner.Core;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DigitalLegacyPlanner.Api.Tests.Features.DigitalAccounts;

[TestFixture]
public class CreateDigitalAccountCommandHandlerTests
{
    private Mock<IDigitalLegacyPlannerContext> _contextMock;
    private CreateDigitalAccountCommandHandler _handler;
    private Mock<DbSet<DigitalAccount>> _accountsDbSetMock;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IDigitalLegacyPlannerContext>();
        _accountsDbSetMock = new Mock<DbSet<DigitalAccount>>();

        _contextMock.Setup(c => c.Accounts).Returns(_accountsDbSetMock.Object);
        _handler = new CreateDigitalAccountCommandHandler(_contextMock.Object);
    }

    [Test]
    public async Task Handle_CreatesNewDigitalAccount()
    {
        // Arrange
        var command = new CreateDigitalAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountType = AccountType.Email,
            AccountName = "Gmail",
            Username = "user@gmail.com",
            PasswordHint = "Favorite pet name",
            Url = "https://gmail.com",
            DesiredAction = "Delete",
            Notes = "Personal email account"
        };

        DigitalAccount? capturedAccount = null;
        _accountsDbSetMock
            .Setup(m => m.Add(It.IsAny<DigitalAccount>()))
            .Callback<DigitalAccount>(a => capturedAccount = a);

        _contextMock
            .Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(command.UserId);
        result.AccountType.Should().Be(command.AccountType);
        result.AccountName.Should().Be(command.AccountName);
        result.Username.Should().Be(command.Username);
        result.PasswordHint.Should().Be(command.PasswordHint);
        result.Url.Should().Be(command.Url);
        result.DesiredAction.Should().Be(command.DesiredAction);
        result.Notes.Should().Be(command.Notes);

        capturedAccount.Should().NotBeNull();
        capturedAccount!.DigitalAccountId.Should().NotBeEmpty();
        capturedAccount.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        capturedAccount.LastUpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));

        _accountsDbSetMock.Verify(m => m.Add(It.IsAny<DigitalAccount>()), Times.Once);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
