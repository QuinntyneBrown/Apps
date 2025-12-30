// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Api.Controllers;
using CharitableGivingTracker.Api.Features.Organizations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CharitableGivingTracker.Api.Tests;

public class OrganizationsControllerTests
{
    private Mock<IMediator> _mockMediator = null!;
    private Mock<ILogger<OrganizationsController>> _mockLogger = null!;
    private OrganizationsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IMediator>();
        _mockLogger = new Mock<ILogger<OrganizationsController>>();
        _controller = new OrganizationsController(_mockMediator.Object, _mockLogger.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkResultWithOrganizations()
    {
        // Arrange
        var organizations = new List<OrganizationDto>
        {
            new OrganizationDto { OrganizationId = Guid.NewGuid(), Name = "Test Charity", Is501c3 = true }
        };
        _mockMediator.Setup(m => m.Send(It.IsAny<GetAllOrganizations.Query>(), default))
            .ReturnsAsync(organizations);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(organizations));
    }

    [Test]
    public async Task GetById_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        var organization = new OrganizationDto { OrganizationId = organizationId, Name = "Test Charity" };
        _mockMediator.Setup(m => m.Send(It.Is<GetOrganizationById.Query>(q => q.OrganizationId == organizationId), default))
            .ReturnsAsync(organization);

        // Act
        var result = await _controller.GetById(organizationId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(organization));
    }

    [Test]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        _mockMediator.Setup(m => m.Send(It.Is<GetOrganizationById.Query>(q => q.OrganizationId == organizationId), default))
            .ReturnsAsync((OrganizationDto?)null);

        // Act
        var result = await _controller.GetById(organizationId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateOrganization.Command
        {
            Name = "New Charity",
            Is501c3 = true
        };
        var createdOrganization = new OrganizationDto { OrganizationId = Guid.NewGuid(), Name = "New Charity" };
        _mockMediator.Setup(m => m.Send(command, default))
            .ReturnsAsync(createdOrganization);

        // Act
        var result = await _controller.Create(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdOrganization));
    }

    [Test]
    public async Task Update_WithMatchingIds_ReturnsOkResult()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        var command = new UpdateOrganization.Command
        {
            OrganizationId = organizationId,
            Name = "Updated Charity",
            Is501c3 = true
        };
        var updatedOrganization = new OrganizationDto { OrganizationId = organizationId, Name = "Updated Charity" };
        _mockMediator.Setup(m => m.Send(command, default))
            .ReturnsAsync(updatedOrganization);

        // Act
        var result = await _controller.Update(organizationId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(updatedOrganization));
    }

    [Test]
    public async Task Update_WithMismatchedIds_ReturnsBadRequest()
    {
        // Arrange
        var routeId = Guid.NewGuid();
        var commandId = Guid.NewGuid();
        var command = new UpdateOrganization.Command
        {
            OrganizationId = commandId,
            Name = "Updated Charity"
        };

        // Act
        var result = await _controller.Update(routeId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        _mockMediator.Setup(m => m.Send(It.Is<DeleteOrganization.Command>(c => c.OrganizationId == organizationId), default))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        _mockMediator.Setup(m => m.Send(It.Is<DeleteOrganization.Command>(c => c.OrganizationId == organizationId), default))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
