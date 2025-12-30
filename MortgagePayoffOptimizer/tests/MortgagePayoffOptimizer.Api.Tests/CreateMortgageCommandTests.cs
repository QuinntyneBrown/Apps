// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace MortgagePayoffOptimizer.Api.Tests;

/// <summary>
/// Tests for CreateMortgageCommand.
/// </summary>
[TestFixture]
public class CreateMortgageCommandTests
{
    private Mock<IMortgagePayoffOptimizerContext> _mockContext;
    private List<Mortgage> _mortgages;

    [SetUp]
    public void Setup()
    {
        _mortgages = new List<Mortgage>();
        _mockContext = new Mock<IMortgagePayoffOptimizerContext>();

        var mockSet = TestHelpers.CreateMockDbSet(_mortgages);
        _mockContext.Setup(c => c.Mortgages).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
    }

    [Test]
    public async Task Handle_ShouldCreateMortgage()
    {
        // Arrange
        var handler = new CreateMortgageCommandHandler(_mockContext.Object);
        var command = new CreateMortgageCommand
        {
            PropertyAddress = "123 Main St",
            Lender = "Bank of America",
            OriginalLoanAmount = 300000m,
            CurrentBalance = 250000m,
            InterestRate = 3.5m,
            LoanTermYears = 30,
            MonthlyPayment = 1347.13m,
            StartDate = new DateTime(2020, 1, 1),
            MortgageType = MortgageType.Fixed,
            IsActive = true,
            Notes = "Primary residence"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.PropertyAddress, Is.EqualTo(command.PropertyAddress));
        Assert.That(result.Lender, Is.EqualTo(command.Lender));
        Assert.That(result.OriginalLoanAmount, Is.EqualTo(command.OriginalLoanAmount));
        Assert.That(_mortgages, Has.Count.EqualTo(1));
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
