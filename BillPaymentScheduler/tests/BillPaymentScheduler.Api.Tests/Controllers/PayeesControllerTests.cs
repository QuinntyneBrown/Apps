// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Api.Controllers;
using BillPaymentScheduler.Api.Features.Payees;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BillPaymentScheduler.Api.Tests.Controllers;

[TestFixture]
public class PayeesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<PayeesController>> _loggerMock;
    private PayeesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<PayeesController>>();
        _controller = new PayeesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetPayees_ReturnsOkWithPayees()
    {
        // Arrange
        var payees = new List<PayeeDto>
        {
            new PayeeDto
            {
                PayeeId = Guid.NewGuid(),
                Name = "Electric Company",
                AccountNumber = "123456",
                Website = "https://electric.com"
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetPayees.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(payees);

        // Act
        var result = await _controller.GetPayees(CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(payees, okResult.Value);
    }

    [Test]
    public async Task GetPayeeById_WithValidId_ReturnsOkWithPayee()
    {
        // Arrange
        var payeeId = Guid.NewGuid();
        var payee = new PayeeDto
        {
            PayeeId = payeeId,
            Name = "Water Company",
            AccountNumber = "789012",
            PhoneNumber = "555-1234"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetPayeeById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(payee);

        // Act
        var result = await _controller.GetPayeeById(payeeId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(payee, okResult.Value);
    }

    [Test]
    public async Task GetPayeeById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var payeeId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetPayeeById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PayeeDto?)null);

        // Act
        var result = await _controller.GetPayeeById(payeeId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result.Result);
    }

    [Test]
    public async Task CreatePayee_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreatePayee.Command
        {
            Name = "Gas Company",
            AccountNumber = "345678",
            Website = "https://gas.com"
        };

        var createdPayee = new PayeeDto
        {
            PayeeId = Guid.NewGuid(),
            Name = command.Name,
            AccountNumber = command.AccountNumber,
            Website = command.Website
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdPayee);

        // Act
        var result = await _controller.CreatePayee(command, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual(createdPayee, createdResult.Value);
    }

    [Test]
    public async Task UpdatePayee_WithValidId_ReturnsOkWithUpdatedPayee()
    {
        // Arrange
        var payeeId = Guid.NewGuid();
        var command = new UpdatePayee.Command
        {
            PayeeId = payeeId,
            Name = "Updated Company",
            AccountNumber = "999999",
            Website = "https://updated.com"
        };

        var updatedPayee = new PayeeDto
        {
            PayeeId = payeeId,
            Name = command.Name,
            AccountNumber = command.AccountNumber,
            Website = command.Website
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedPayee);

        // Act
        var result = await _controller.UpdatePayee(payeeId, command, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(updatedPayee, okResult.Value);
    }

    [Test]
    public async Task UpdatePayee_WithMismatchedId_ReturnsBadRequest()
    {
        // Arrange
        var urlId = Guid.NewGuid();
        var command = new UpdatePayee.Command { PayeeId = Guid.NewGuid() };

        // Act
        var result = await _controller.UpdatePayee(urlId, command, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }

    [Test]
    public async Task DeletePayee_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var payeeId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeletePayee.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeletePayee(payeeId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }

    [Test]
    public async Task DeletePayee_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var payeeId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeletePayee.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeletePayee(payeeId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
}
