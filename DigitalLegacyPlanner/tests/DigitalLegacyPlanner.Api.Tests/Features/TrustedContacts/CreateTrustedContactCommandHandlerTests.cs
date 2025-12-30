// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Api.Features.TrustedContacts.Commands;
using DigitalLegacyPlanner.Core;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DigitalLegacyPlanner.Api.Tests.Features.TrustedContacts;

[TestFixture]
public class CreateTrustedContactCommandHandlerTests
{
    private Mock<IDigitalLegacyPlannerContext> _contextMock;
    private CreateTrustedContactCommandHandler _handler;
    private Mock<DbSet<TrustedContact>> _contactsDbSetMock;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IDigitalLegacyPlannerContext>();
        _contactsDbSetMock = new Mock<DbSet<TrustedContact>>();

        _contextMock.Setup(c => c.Contacts).Returns(_contactsDbSetMock.Object);
        _handler = new CreateTrustedContactCommandHandler(_contextMock.Object);
    }

    [Test]
    public async Task Handle_CreatesNewTrustedContact()
    {
        // Arrange
        var command = new CreateTrustedContactCommand
        {
            UserId = Guid.NewGuid(),
            FullName = "John Doe",
            Email = "john@example.com",
            Relationship = "Brother",
            PhoneNumber = "123-456-7890",
            Role = "Executor",
            IsPrimaryContact = true,
            Notes = "Test notes"
        };

        TrustedContact? capturedContact = null;
        _contactsDbSetMock
            .Setup(m => m.Add(It.IsAny<TrustedContact>()))
            .Callback<TrustedContact>(c => capturedContact = c);

        _contextMock
            .Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(command.UserId);
        result.FullName.Should().Be(command.FullName);
        result.Email.Should().Be(command.Email);
        result.Relationship.Should().Be(command.Relationship);
        result.PhoneNumber.Should().Be(command.PhoneNumber);
        result.Role.Should().Be(command.Role);
        result.IsPrimaryContact.Should().Be(command.IsPrimaryContact);
        result.Notes.Should().Be(command.Notes);
        result.IsNotified.Should().BeFalse();

        capturedContact.Should().NotBeNull();
        capturedContact!.TrustedContactId.Should().NotBeEmpty();

        _contactsDbSetMock.Verify(m => m.Add(It.IsAny<TrustedContact>()), Times.Once);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_SetsDefaultValues()
    {
        // Arrange
        var command = new CreateTrustedContactCommand
        {
            UserId = Guid.NewGuid(),
            FullName = "Jane Doe",
            Email = "jane@example.com",
            Relationship = "Sister"
        };

        TrustedContact? capturedContact = null;
        _contactsDbSetMock
            .Setup(m => m.Add(It.IsAny<TrustedContact>()))
            .Callback<TrustedContact>(c => capturedContact = c);

        _contextMock
            .Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsNotified.Should().BeFalse();
        result.IsPrimaryContact.Should().BeFalse();
        result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }
}
