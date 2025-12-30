// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Api.Features.Clients;
using FreelanceProjectManager.Core;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace FreelanceProjectManager.Api.Tests.Features.Clients;

/// <summary>
/// Tests for the CreateClientHandler.
/// </summary>
[TestFixture]
public class CreateClientHandlerTests
{
    private Mock<IFreelanceProjectManagerContext> _contextMock;
    private Mock<DbSet<Client>> _clientsDbSetMock;
    private CreateClientHandler _handler;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IFreelanceProjectManagerContext>();
        _clientsDbSetMock = new Mock<DbSet<Client>>();

        _contextMock.Setup(c => c.Clients).Returns(_clientsDbSetMock.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _handler = new CreateClientHandler(_contextMock.Object);
    }

    [Test]
    public async Task Handle_CreatesClient_AndReturnsClientDto()
    {
        // Arrange
        var command = new CreateClientCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Test Client",
            CompanyName = "Test Company",
            Email = "test@example.com",
            Phone = "123-456-7890"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.CompanyName, Is.EqualTo(command.CompanyName));
        Assert.That(result.Email, Is.EqualTo(command.Email));
        Assert.That(result.Phone, Is.EqualTo(command.Phone));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.IsActive, Is.True);

        _clientsDbSetMock.Verify(m => m.Add(It.IsAny<Client>()), Times.Once);
        _contextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
