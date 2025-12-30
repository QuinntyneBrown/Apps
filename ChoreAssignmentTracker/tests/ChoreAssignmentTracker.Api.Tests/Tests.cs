using ChoreAssignmentTracker.Api.Controllers;
using ChoreAssignmentTracker.Api.Features.FamilyMembers;
using ChoreAssignmentTracker.Api.Features.Chores;
using ChoreAssignmentTracker.Api.Features.Rewards;
using ChoreAssignmentTracker.Api.Features.Assignments;
using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChoreAssignmentTracker.Api.Tests;

/// <summary>
/// Tests for FamilyMembersController.
/// </summary>
[TestFixture]
public class FamilyMembersControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<FamilyMembersController>> _loggerMock;
    private FamilyMembersController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<FamilyMembersController>>();
        _controller = new FamilyMembersController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetFamilyMembers_ReturnsOkResult()
    {
        // Arrange
        var familyMembers = new List<FamilyMemberDto>
        {
            new FamilyMemberDto { FamilyMemberId = Guid.NewGuid(), Name = "John" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetFamilyMembers>(), default))
            .ReturnsAsync(familyMembers);

        // Act
        var result = await _controller.GetFamilyMembers(null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(familyMembers));
    }

    [Test]
    public async Task GetFamilyMember_WhenExists_ReturnsOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var familyMember = new FamilyMemberDto { FamilyMemberId = id, Name = "John" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetFamilyMemberById>(), default))
            .ReturnsAsync(familyMember);

        // Act
        var result = await _controller.GetFamilyMember(id);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task GetFamilyMember_WhenNotExists_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetFamilyMemberById>(), default))
            .ReturnsAsync((FamilyMemberDto?)null);

        // Act
        var result = await _controller.GetFamilyMember(Guid.NewGuid());

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateFamilyMember_ReturnsCreatedResult()
    {
        // Arrange
        var dto = new CreateFamilyMemberDto { Name = "John", UserId = Guid.NewGuid() };
        var created = new FamilyMemberDto { FamilyMemberId = Guid.NewGuid(), Name = "John" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateFamilyMember>(), default))
            .ReturnsAsync(created);

        // Act
        var result = await _controller.CreateFamilyMember(dto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
    }

    [Test]
    public async Task UpdateFamilyMember_WhenExists_ReturnsOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto = new UpdateFamilyMemberDto { Name = "John Updated" };
        var updated = new FamilyMemberDto { FamilyMemberId = id, Name = "John Updated" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateFamilyMember>(), default))
            .ReturnsAsync(updated);

        // Act
        var result = await _controller.UpdateFamilyMember(id, dto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task DeleteFamilyMember_WhenExists_ReturnsNoContent()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteFamilyMember>(), default))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteFamilyMember(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
}

/// <summary>
/// Tests for ChoresController.
/// </summary>
[TestFixture]
public class ChoresControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<ChoresController>> _loggerMock;
    private ChoresController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<ChoresController>>();
        _controller = new ChoresController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetChores_ReturnsOkResult()
    {
        // Arrange
        var chores = new List<ChoreDto>
        {
            new ChoreDto { ChoreId = Guid.NewGuid(), Name = "Clean dishes" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetChores>(), default))
            .ReturnsAsync(chores);

        // Act
        var result = await _controller.GetChores(null, null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task CreateChore_ReturnsCreatedResult()
    {
        // Arrange
        var dto = new CreateChoreDto { Name = "Clean dishes", UserId = Guid.NewGuid(), Points = 10 };
        var created = new ChoreDto { ChoreId = Guid.NewGuid(), Name = "Clean dishes" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateChore>(), default))
            .ReturnsAsync(created);

        // Act
        var result = await _controller.CreateChore(dto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
    }
}

/// <summary>
/// Tests for RewardsController.
/// </summary>
[TestFixture]
public class RewardsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<RewardsController>> _loggerMock;
    private RewardsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<RewardsController>>();
        _controller = new RewardsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetRewards_ReturnsOkResult()
    {
        // Arrange
        var rewards = new List<RewardDto>
        {
            new RewardDto { RewardId = Guid.NewGuid(), Name = "Ice cream" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRewards>(), default))
            .ReturnsAsync(rewards);

        // Act
        var result = await _controller.GetRewards(null, null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task RedeemReward_WhenSuccessful_ReturnsOkResult()
    {
        // Arrange
        var rewardId = Guid.NewGuid();
        var dto = new RedeemRewardDto { FamilyMemberId = Guid.NewGuid() };
        var redeemed = new RewardDto { RewardId = rewardId, Name = "Ice cream", IsAvailable = false };
        _mediatorMock.Setup(m => m.Send(It.IsAny<RedeemReward>(), default))
            .ReturnsAsync(redeemed);

        // Act
        var result = await _controller.RedeemReward(rewardId, dto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task RedeemReward_WhenFailed_ReturnsBadRequest()
    {
        // Arrange
        var rewardId = Guid.NewGuid();
        var dto = new RedeemRewardDto { FamilyMemberId = Guid.NewGuid() };
        _mediatorMock.Setup(m => m.Send(It.IsAny<RedeemReward>(), default))
            .ReturnsAsync((RewardDto?)null);

        // Act
        var result = await _controller.RedeemReward(rewardId, dto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }
}

/// <summary>
/// Tests for AssignmentsController.
/// </summary>
[TestFixture]
public class AssignmentsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<AssignmentsController>> _loggerMock;
    private AssignmentsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<AssignmentsController>>();
        _controller = new AssignmentsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAssignments_ReturnsOkResult()
    {
        // Arrange
        var assignments = new List<AssignmentDto>
        {
            new AssignmentDto { AssignmentId = Guid.NewGuid(), ChoreName = "Clean dishes" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAssignments>(), default))
            .ReturnsAsync(assignments);

        // Act
        var result = await _controller.GetAssignments(null, null, null, null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task CreateAssignment_WhenSuccessful_ReturnsCreatedResult()
    {
        // Arrange
        var dto = new CreateAssignmentDto
        {
            ChoreId = Guid.NewGuid(),
            FamilyMemberId = Guid.NewGuid(),
            AssignedDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(1)
        };
        var created = new AssignmentDto { AssignmentId = Guid.NewGuid() };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateAssignment>(), default))
            .ReturnsAsync(created);

        // Act
        var result = await _controller.CreateAssignment(dto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
    }

    [Test]
    public async Task CompleteAssignment_WhenExists_ReturnsOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto = new CompleteAssignmentDto { Notes = "Done!" };
        var completed = new AssignmentDto { AssignmentId = id, IsCompleted = true };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CompleteAssignment>(), default))
            .ReturnsAsync(completed);

        // Act
        var result = await _controller.CompleteAssignment(id, dto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task VerifyAssignment_WhenExists_ReturnsOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto = new VerifyAssignmentDto { Points = 10 };
        var verified = new AssignmentDto { AssignmentId = id, IsVerified = true, PointsEarned = 10 };
        _mediatorMock.Setup(m => m.Send(It.IsAny<VerifyAssignment>(), default))
            .ReturnsAsync(verified);

        // Act
        var result = await _controller.VerifyAssignment(id, dto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }
}
