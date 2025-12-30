// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Api.Controllers;
using FriendGroupEventCoordinator.Api.Features.Groups;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FriendGroupEventCoordinator.Api.Tests.Controllers;

[TestFixture]
public class GroupsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<GroupsController>> _loggerMock;
    private GroupsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<GroupsController>>();
        _controller = new GroupsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetGroups_ReturnsOkResultWithGroups()
    {
        // Arrange
        var groups = new List<GroupDto>
        {
            new GroupDto { GroupId = Guid.NewGuid(), Name = "Test Group 1" },
            new GroupDto { GroupId = Guid.NewGuid(), Name = "Test Group 2" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGroupsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(groups);

        // Act
        var result = await _controller.GetGroups(CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(groups));
    }

    [Test]
    public async Task GetGroup_WithValidId_ReturnsOkResultWithGroup()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var groupDto = new GroupDto { GroupId = groupId, Name = "Test Group" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGroupQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(groupDto);

        // Act
        var result = await _controller.GetGroup(groupId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(groupDto));
    }

    [Test]
    public async Task GetGroup_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGroupQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GroupDto?)null);

        // Act
        var result = await _controller.GetGroup(groupId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateGroup_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var createDto = new CreateGroupDto { Name = "New Group", CreatedByUserId = Guid.NewGuid() };
        var createdGroup = new GroupDto { GroupId = Guid.NewGuid(), Name = "New Group" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateGroupCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdGroup);

        // Act
        var result = await _controller.CreateGroup(createDto, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult.Value, Is.EqualTo(createdGroup));
    }

    [Test]
    public async Task UpdateGroup_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var updateDto = new UpdateGroupDto { Name = "Updated Group" };
        var updatedGroup = new GroupDto { GroupId = groupId, Name = "Updated Group" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateGroupCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedGroup);

        // Act
        var result = await _controller.UpdateGroup(groupId, updateDto, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task DeactivateGroup_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var deactivatedGroup = new GroupDto { GroupId = groupId, IsActive = false };
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeactivateGroupCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(deactivatedGroup);

        // Act
        var result = await _controller.DeactivateGroup(groupId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task DeleteGroup_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteGroupCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteGroup(groupId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteGroup_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteGroupCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteGroup(groupId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
