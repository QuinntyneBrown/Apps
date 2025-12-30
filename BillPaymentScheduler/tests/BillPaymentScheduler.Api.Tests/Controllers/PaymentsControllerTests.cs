// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Api.Controllers;
using BillPaymentScheduler.Api.Features.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BillPaymentScheduler.Api.Tests.Controllers;

[TestFixture]
public class PaymentsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<PaymentsController>> _loggerMock;
    private PaymentsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<PaymentsController>>();
        _controller = new PaymentsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetPayments_ReturnsOkWithPayments()
    {
        // Arrange
        var payments = new List<PaymentDto>
        {
            new PaymentDto
            {
                PaymentId = Guid.NewGuid(),
                BillId = Guid.NewGuid(),
                Amount = 150.00m,
                PaymentDate = DateTime.Now,
                ConfirmationNumber = "CONF123"
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetPayments.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(payments);

        // Act
        var result = await _controller.GetPayments(CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(payments, okResult.Value);
    }

    [Test]
    public async Task GetPaymentById_WithValidId_ReturnsOkWithPayment()
    {
        // Arrange
        var paymentId = Guid.NewGuid();
        var payment = new PaymentDto
        {
            PaymentId = paymentId,
            BillId = Guid.NewGuid(),
            Amount = 75.50m,
            PaymentDate = DateTime.Now,
            PaymentMethod = "Credit Card"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetPaymentById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(payment);

        // Act
        var result = await _controller.GetPaymentById(paymentId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(payment, okResult.Value);
    }

    [Test]
    public async Task GetPaymentById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var paymentId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetPaymentById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PaymentDto?)null);

        // Act
        var result = await _controller.GetPaymentById(paymentId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result.Result);
    }

    [Test]
    public async Task CreatePayment_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreatePayment.Command
        {
            BillId = Guid.NewGuid(),
            Amount = 100.00m,
            PaymentDate = DateTime.Now,
            PaymentMethod = "Bank Transfer",
            ConfirmationNumber = "CONF456"
        };

        var createdPayment = new PaymentDto
        {
            PaymentId = Guid.NewGuid(),
            BillId = command.BillId,
            Amount = command.Amount,
            PaymentDate = command.PaymentDate,
            PaymentMethod = command.PaymentMethod,
            ConfirmationNumber = command.ConfirmationNumber
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdPayment);

        // Act
        var result = await _controller.CreatePayment(command, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual(createdPayment, createdResult.Value);
    }

    [Test]
    public async Task UpdatePayment_WithValidId_ReturnsOkWithUpdatedPayment()
    {
        // Arrange
        var paymentId = Guid.NewGuid();
        var command = new UpdatePayment.Command
        {
            PaymentId = paymentId,
            BillId = Guid.NewGuid(),
            Amount = 200.00m,
            PaymentDate = DateTime.Now,
            PaymentMethod = "Cash"
        };

        var updatedPayment = new PaymentDto
        {
            PaymentId = paymentId,
            BillId = command.BillId,
            Amount = command.Amount,
            PaymentDate = command.PaymentDate,
            PaymentMethod = command.PaymentMethod
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedPayment);

        // Act
        var result = await _controller.UpdatePayment(paymentId, command, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(updatedPayment, okResult.Value);
    }

    [Test]
    public async Task UpdatePayment_WithMismatchedId_ReturnsBadRequest()
    {
        // Arrange
        var urlId = Guid.NewGuid();
        var command = new UpdatePayment.Command { PaymentId = Guid.NewGuid() };

        // Act
        var result = await _controller.UpdatePayment(urlId, command, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }

    [Test]
    public async Task DeletePayment_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var paymentId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeletePayment.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeletePayment(paymentId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }

    [Test]
    public async Task DeletePayment_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var paymentId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeletePayment.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeletePayment(paymentId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
}
