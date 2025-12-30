// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Api.Controllers;
using FriendGroupEventCoordinator.Api.Features.Members;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FriendGroupEventCoordinator.Api.Tests.Controllers;

[TestFixture]
public class MembersControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<MembersController>> _loggerMock;
    private MembersController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<MembersController>>();
        _controller = new MembersController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetMember_WithValidId_ReturnsOkResultWithMember()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var memberDto = new MemberDto { MemberId = memberId, Name = "Test Member" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetMemberQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(memberDto);

        // Act
        var result = await _controller.GetMember(memberId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(memberDto));
    }

    [Test]
    public async Task GetMember_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetMemberQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((MemberDto?)null);

        // Act
        var result = await _controller.GetMember(memberId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task GetMembersByGroup_ReturnsOkResultWithMembers()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var members = new List<MemberDto>
        {
            new MemberDto { MemberId = Guid.NewGuid(), Name = "Member 1", GroupId = groupId },
            new MemberDto { MemberId = Guid.NewGuid(), Name = "Member 2", GroupId = groupId }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetMembersByGroupQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(members);

        // Act
        var result = await _controller.GetMembersByGroup(groupId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(members));
    }

    [Test]
    public async Task CreateMember_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var createDto = new CreateMemberDto { Name = "New Member", GroupId = Guid.NewGuid(), UserId = Guid.NewGuid() };
        var createdMember = new MemberDto { MemberId = Guid.NewGuid(), Name = "New Member" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateMemberCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdMember);

        // Act
        var result = await _controller.CreateMember(createDto, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult.Value, Is.EqualTo(createdMember));
    }

    [Test]
    public async Task UpdateMember_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var updateDto = new UpdateMemberDto { Name = "Updated Member" };
        var updatedMember = new MemberDto { MemberId = memberId, Name = "Updated Member" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateMemberCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedMember);

        // Act
        var result = await _controller.UpdateMember(memberId, updateDto, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task RemoveMember_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var removedMember = new MemberDto { MemberId = memberId, IsActive = false };
        _mediatorMock.Setup(m => m.Send(It.IsAny<RemoveMemberCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(removedMember);

        // Act
        var result = await _controller.RemoveMember(memberId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task PromoteToAdmin_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var promotedMember = new MemberDto { MemberId = memberId, IsAdmin = true };
        _mediatorMock.Setup(m => m.Send(It.IsAny<PromoteToAdminCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(promotedMember);

        // Act
        var result = await _controller.PromoteToAdmin(memberId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task DeleteMember_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteMemberCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteMember(memberId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteMember_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteMemberCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteMember(memberId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
