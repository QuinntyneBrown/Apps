// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Api.Tests;

/// <summary>
/// Tests for GetMortgagesQuery.
/// </summary>
[TestFixture]
public class GetMortgagesQueryTests
{
    private Mock<IMortgagePayoffOptimizerContext> _mockContext;
    private List<Mortgage> _mortgages;

    [SetUp]
    public void Setup()
    {
        _mortgages = new List<Mortgage>
        {
            new Mortgage
            {
                MortgageId = Guid.NewGuid(),
                PropertyAddress = "123 Main St",
                Lender = "Bank of America",
                OriginalLoanAmount = 300000m,
                CurrentBalance = 250000m,
                InterestRate = 3.5m,
                LoanTermYears = 30,
                MonthlyPayment = 1347.13m,
                StartDate = new DateTime(2020, 1, 1),
                MortgageType = MortgageType.Fixed,
                IsActive = true
            },
            new Mortgage
            {
                MortgageId = Guid.NewGuid(),
                PropertyAddress = "456 Oak Ave",
                Lender = "Wells Fargo",
                OriginalLoanAmount = 400000m,
                CurrentBalance = 350000m,
                InterestRate = 3.75m,
                LoanTermYears = 30,
                MonthlyPayment = 1853.36m,
                StartDate = new DateTime(2021, 6, 1),
                MortgageType = MortgageType.Fixed,
                IsActive = true
            }
        };

        _mockContext = new Mock<IMortgagePayoffOptimizerContext>();
        var mockSet = TestHelpers.CreateMockDbSet(_mortgages);
        _mockContext.Setup(c => c.Mortgages).Returns(mockSet.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnAllMortgages()
    {
        // Arrange
        var handler = new GetMortgagesQueryHandler(_mockContext.Object);
        var query = new GetMortgagesQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
    }
}
