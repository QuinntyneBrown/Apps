using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarrantyReturnPeriodTracker.Api.Controllers;
using WarrantyReturnPeriodTracker.Api.Features.ReturnWindows;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Tests;

[TestFixture]
public class ReturnWindowsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<ReturnWindowsController>> _loggerMock;
    private ReturnWindowsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<ReturnWindowsController>>();
        _controller = new ReturnWindowsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetReturnWindows_ReturnsOkWithReturnWindows()
    {
        // Arrange
        var expectedReturnWindows = new List<ReturnWindowDto>
        {
            new ReturnWindowDto
            {
                ReturnWindowId = Guid.NewGuid(),
                PurchaseId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                DurationDays = 30,
                Status = ReturnWindowStatus.Open,
                PolicyDetails = "30-day return policy"
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetReturnWindowsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedReturnWindows);

        // Act
        var result = await _controller.GetReturnWindows(CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(expectedReturnWindows));
    }

    [Test]
    public async Task CreateReturnWindow_ValidCommand_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateReturnWindowCommand
        {
            PurchaseId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30),
            DurationDays = 30,
            PolicyDetails = "Standard 30-day return policy",
            RestockingFeePercent = 15m
        };

        var createdReturnWindow = new ReturnWindowDto
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = command.PurchaseId,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            DurationDays = command.DurationDays,
            Status = ReturnWindowStatus.Open,
            PolicyDetails = command.PolicyDetails,
            RestockingFeePercent = command.RestockingFeePercent
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdReturnWindow);

        // Act
        var result = await _controller.CreateReturnWindow(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdReturnWindow));
    }

    [Test]
    public async Task DeleteReturnWindow_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var returnWindowId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteReturnWindowCommand>(c => c.ReturnWindowId == returnWindowId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeleteReturnWindow(returnWindowId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
}
