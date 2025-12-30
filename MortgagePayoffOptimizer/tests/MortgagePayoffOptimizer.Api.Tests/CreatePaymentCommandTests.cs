// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Api.Tests;

/// <summary>
/// Tests for CreatePaymentCommand.
/// </summary>
[TestFixture]
public class CreatePaymentCommandTests
{
    private Mock<IMortgagePayoffOptimizerContext> _mockContext;
    private List<Payment> _payments;

    [SetUp]
    public void Setup()
    {
        _payments = new List<Payment>();
        _mockContext = new Mock<IMortgagePayoffOptimizerContext>();

        var mockSet = TestHelpers.CreateMockDbSet(_payments);
        _mockContext.Setup(c => c.Payments).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
    }

    [Test]
    public async Task Handle_ShouldCreatePayment()
    {
        // Arrange
        var handler = new CreatePaymentCommandHandler(_mockContext.Object);
        var command = new CreatePaymentCommand
        {
            MortgageId = Guid.NewGuid(),
            PaymentDate = new DateTime(2024, 1, 15),
            Amount = 1347.13m,
            PrincipalAmount = 618.47m,
            InterestAmount = 728.66m,
            ExtraPrincipal = 100m,
            Notes = "Regular monthly payment"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.MortgageId, Is.EqualTo(command.MortgageId));
        Assert.That(result.Amount, Is.EqualTo(command.Amount));
        Assert.That(result.PrincipalAmount, Is.EqualTo(command.PrincipalAmount));
        Assert.That(_payments, Has.Count.EqualTo(1));
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
