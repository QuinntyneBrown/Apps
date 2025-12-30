// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLoanComparisonTool.Api.Tests;

namespace PersonalLoanComparisonTool.Api.Tests;

[TestFixture]
public class OfferFeatureTests
{
    [Test]
    public void OfferDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var offer = new Offer
        {
            OfferId = Guid.NewGuid(),
            LoanId = Guid.NewGuid(),
            LenderName = "Test Bank",
            LoanAmount = 10000m,
            InterestRate = 5.5m,
            TermMonths = 36,
            MonthlyPayment = 300m,
            TotalCost = 10800m,
            Fees = 100m,
            Notes = "Test notes"
        };

        // Act
        var dto = offer.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.OfferId, Is.EqualTo(offer.OfferId));
            Assert.That(dto.LoanId, Is.EqualTo(offer.LoanId));
            Assert.That(dto.LenderName, Is.EqualTo(offer.LenderName));
            Assert.That(dto.LoanAmount, Is.EqualTo(offer.LoanAmount));
            Assert.That(dto.InterestRate, Is.EqualTo(offer.InterestRate));
            Assert.That(dto.TermMonths, Is.EqualTo(offer.TermMonths));
            Assert.That(dto.MonthlyPayment, Is.EqualTo(offer.MonthlyPayment));
            Assert.That(dto.TotalCost, Is.EqualTo(offer.TotalCost));
            Assert.That(dto.Fees, Is.EqualTo(offer.Fees));
            Assert.That(dto.Notes, Is.EqualTo(offer.Notes));
        });
    }

    [Test]
    public async Task CreateOfferCommand_CreatesOffer()
    {
        // Arrange
        var offers = new List<Offer>();
        var mockDbSet = TestHelpers.CreateAsyncMockDbSet(offers);
        var mockContext = new Mock<IPersonalLoanComparisonToolContext>();
        mockContext.Setup(c => c.Offers).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateOfferCommandHandler(mockContext.Object);
        var command = new CreateOfferCommand(
            Guid.NewGuid(),
            "Test Bank",
            10000m,
            5.5m,
            36,
            300m,
            100m,
            "Test notes"
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.LoanId, Is.EqualTo(command.LoanId));
            Assert.That(result.LenderName, Is.EqualTo(command.LenderName));
            Assert.That(result.LoanAmount, Is.EqualTo(command.LoanAmount));
            Assert.That(result.InterestRate, Is.EqualTo(command.InterestRate));
            Assert.That(result.TermMonths, Is.EqualTo(command.TermMonths));
            Assert.That(result.MonthlyPayment, Is.EqualTo(command.MonthlyPayment));
            Assert.That(result.Fees, Is.EqualTo(command.Fees));
            Assert.That(result.Notes, Is.EqualTo(command.Notes));
            Assert.That(offers.Count, Is.EqualTo(1));
        });

        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetOffersQuery_ReturnsAllOffers()
    {
        // Arrange
        var offers = new List<Offer>
        {
            new Offer
            {
                OfferId = Guid.NewGuid(),
                LoanId = Guid.NewGuid(),
                LenderName = "Bank 1",
                LoanAmount = 10000m,
                InterestRate = 5.5m,
                TermMonths = 36,
                MonthlyPayment = 300m,
                TotalCost = 10800m,
                Fees = 100m
            },
            new Offer
            {
                OfferId = Guid.NewGuid(),
                LoanId = Guid.NewGuid(),
                LenderName = "Bank 2",
                LoanAmount = 15000m,
                InterestRate = 6.0m,
                TermMonths = 48,
                MonthlyPayment = 350m,
                TotalCost = 16800m,
                Fees = 150m
            }
        };

        var mockDbSet = TestHelpers.CreateAsyncMockDbSet(offers);
        var mockContext = new Mock<IPersonalLoanComparisonToolContext>();
        mockContext.Setup(c => c.Offers).Returns(mockDbSet.Object);

        var handler = new GetOffersQueryHandler(mockContext.Object);
        var query = new GetOffersQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task DeleteOfferCommand_DeletesOffer()
    {
        // Arrange
        var offerId = Guid.NewGuid();
        var offers = new List<Offer>
        {
            new Offer
            {
                OfferId = offerId,
                LoanId = Guid.NewGuid(),
                LenderName = "Test Bank",
                LoanAmount = 10000m,
                InterestRate = 5.5m,
                TermMonths = 36,
                MonthlyPayment = 300m,
                TotalCost = 10800m,
                Fees = 100m
            }
        };

        var mockDbSet = TestHelpers.CreateAsyncMockDbSet(offers);
        var mockContext = new Mock<IPersonalLoanComparisonToolContext>();
        mockContext.Setup(c => c.Offers).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new DeleteOfferCommandHandler(mockContext.Object);
        var command = new DeleteOfferCommand(offerId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(offers.Count, Is.EqualTo(0));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
