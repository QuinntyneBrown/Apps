// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLoanComparisonTool.Api.Tests;

namespace PersonalLoanComparisonTool.Api.Tests;

[TestFixture]
public class LoanFeatureTests
{
    [Test]
    public void LoanDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var loan = new Loan
        {
            LoanId = Guid.NewGuid(),
            Name = "Test Loan",
            LoanType = LoanType.Personal,
            RequestedAmount = 10000m,
            Purpose = "Home Improvement",
            CreditScore = 750,
            Notes = "Test notes"
        };

        // Act
        var dto = loan.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.LoanId, Is.EqualTo(loan.LoanId));
            Assert.That(dto.Name, Is.EqualTo(loan.Name));
            Assert.That(dto.LoanType, Is.EqualTo(loan.LoanType));
            Assert.That(dto.RequestedAmount, Is.EqualTo(loan.RequestedAmount));
            Assert.That(dto.Purpose, Is.EqualTo(loan.Purpose));
            Assert.That(dto.CreditScore, Is.EqualTo(loan.CreditScore));
            Assert.That(dto.Notes, Is.EqualTo(loan.Notes));
        });
    }

    [Test]
    public async Task CreateLoanCommand_CreatesLoan()
    {
        // Arrange
        var loans = new List<Loan>();
        var mockDbSet = TestHelpers.CreateAsyncMockDbSet(loans);
        var mockContext = new Mock<IPersonalLoanComparisonToolContext>();
        mockContext.Setup(c => c.Loans).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateLoanCommandHandler(mockContext.Object);
        var command = new CreateLoanCommand(
            "Test Loan",
            LoanType.Personal,
            10000m,
            "Home Improvement",
            750,
            "Test notes"
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo(command.Name));
            Assert.That(result.LoanType, Is.EqualTo(command.LoanType));
            Assert.That(result.RequestedAmount, Is.EqualTo(command.RequestedAmount));
            Assert.That(result.Purpose, Is.EqualTo(command.Purpose));
            Assert.That(result.CreditScore, Is.EqualTo(command.CreditScore));
            Assert.That(result.Notes, Is.EqualTo(command.Notes));
            Assert.That(loans.Count, Is.EqualTo(1));
        });

        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetLoansQuery_ReturnsAllLoans()
    {
        // Arrange
        var loans = new List<Loan>
        {
            new Loan
            {
                LoanId = Guid.NewGuid(),
                Name = "Loan 1",
                LoanType = LoanType.Personal,
                RequestedAmount = 10000m,
                Purpose = "Purpose 1",
                CreditScore = 750
            },
            new Loan
            {
                LoanId = Guid.NewGuid(),
                Name = "Loan 2",
                LoanType = LoanType.Auto,
                RequestedAmount = 20000m,
                Purpose = "Purpose 2",
                CreditScore = 700
            }
        };

        var mockDbSet = TestHelpers.CreateAsyncMockDbSet(loans);
        var mockContext = new Mock<IPersonalLoanComparisonToolContext>();
        mockContext.Setup(c => c.Loans).Returns(mockDbSet.Object);

        var handler = new GetLoansQueryHandler(mockContext.Object);
        var query = new GetLoansQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task DeleteLoanCommand_DeletesLoan()
    {
        // Arrange
        var loanId = Guid.NewGuid();
        var loans = new List<Loan>
        {
            new Loan
            {
                LoanId = loanId,
                Name = "Test Loan",
                LoanType = LoanType.Personal,
                RequestedAmount = 10000m,
                Purpose = "Test",
                CreditScore = 750
            }
        };

        var mockDbSet = TestHelpers.CreateAsyncMockDbSet(loans);
        var mockContext = new Mock<IPersonalLoanComparisonToolContext>();
        mockContext.Setup(c => c.Loans).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new DeleteLoanCommandHandler(mockContext.Object);
        var command = new DeleteLoanCommand(loanId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(loans.Count, Is.EqualTo(0));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
