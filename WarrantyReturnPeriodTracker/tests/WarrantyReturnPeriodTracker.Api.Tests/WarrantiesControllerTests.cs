using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarrantyReturnPeriodTracker.Api.Controllers;
using WarrantyReturnPeriodTracker.Api.Features.Warranties;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Tests;

[TestFixture]
public class WarrantiesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<WarrantiesController>> _loggerMock;
    private WarrantiesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<WarrantiesController>>();
        _controller = new WarrantiesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetWarranties_ReturnsOkWithWarranties()
    {
        // Arrange
        var expectedWarranties = new List<WarrantyDto>
        {
            new WarrantyDto
            {
                WarrantyId = Guid.NewGuid(),
                PurchaseId = Guid.NewGuid(),
                WarrantyType = WarrantyType.Manufacturer,
                Provider = "Test Provider",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(12),
                DurationMonths = 12,
                Status = WarrantyStatus.Active
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetWarrantiesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedWarranties);

        // Act
        var result = await _controller.GetWarranties(CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(expectedWarranties));
    }

    [Test]
    public async Task CreateWarranty_ValidCommand_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateWarrantyCommand
        {
            PurchaseId = Guid.NewGuid(),
            WarrantyType = WarrantyType.Extended,
            Provider = "Extended Warranty Co",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(2),
            DurationMonths = 24
        };

        var createdWarranty = new WarrantyDto
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = command.PurchaseId,
            WarrantyType = command.WarrantyType,
            Provider = command.Provider,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            DurationMonths = command.DurationMonths,
            Status = WarrantyStatus.Active
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdWarranty);

        // Act
        var result = await _controller.CreateWarranty(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdWarranty));
    }
}
