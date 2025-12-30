using CharitableGivingTracker.Api.Controllers;
using CharitableGivingTracker.Api.Features.Donations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CharitableGivingTracker.Api.Tests;

public class DonationsControllerTests
{
    private Mock<IMediator> _mockMediator = null!;
    private Mock<ILogger<DonationsController>> _mockLogger = null!;
    private DonationsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IMediator>();
        _mockLogger = new Mock<ILogger<DonationsController>>();
        _controller = new DonationsController(_mockMediator.Object, _mockLogger.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkResultWithDonations()
    {
        // Arrange
        var donations = new List<DonationDto>
        {
            new DonationDto { DonationId = Guid.NewGuid(), Amount = 100, OrganizationName = "Test Org" }
        };
        _mockMediator.Setup(m => m.Send(It.IsAny<GetAllDonations.Query>(), default))
            .ReturnsAsync(donations);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(donations));
    }

    [Test]
    public async Task GetById_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var donationId = Guid.NewGuid();
        var donation = new DonationDto { DonationId = donationId, Amount = 100 };
        _mockMediator.Setup(m => m.Send(It.Is<GetDonationById.Query>(q => q.DonationId == donationId), default))
            .ReturnsAsync(donation);

        // Act
        var result = await _controller.GetById(donationId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(donation));
    }

    [Test]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var donationId = Guid.NewGuid();
        _mockMediator.Setup(m => m.Send(It.Is<GetDonationById.Query>(q => q.DonationId == donationId), default))
            .ReturnsAsync((DonationDto?)null);

        // Act
        var result = await _controller.GetById(donationId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateDonation.Command
        {
            OrganizationId = Guid.NewGuid(),
            Amount = 100,
            DonationDate = DateTime.UtcNow
        };
        var createdDonation = new DonationDto { DonationId = Guid.NewGuid(), Amount = 100 };
        _mockMediator.Setup(m => m.Send(command, default))
            .ReturnsAsync(createdDonation);

        // Act
        var result = await _controller.Create(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdDonation));
    }

    [Test]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var donationId = Guid.NewGuid();
        _mockMediator.Setup(m => m.Send(It.Is<DeleteDonation.Command>(c => c.DonationId == donationId), default))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(donationId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var donationId = Guid.NewGuid();
        _mockMediator.Setup(m => m.Send(It.Is<DeleteDonation.Command>(c => c.DonationId == donationId), default))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(donationId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
