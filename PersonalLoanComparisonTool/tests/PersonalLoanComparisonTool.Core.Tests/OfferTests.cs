namespace PersonalLoanComparisonTool.Core.Tests;

public class OfferTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesOffer()
    {
        // Arrange
        var offerId = Guid.NewGuid();
        var loanId = Guid.NewGuid();

        // Act
        var offer = new Offer
        {
            OfferId = offerId,
            LoanId = loanId,
            LenderName = "Bank of America",
            LoanAmount = 10000m,
            InterestRate = 5.5m,
            TermMonths = 36,
            MonthlyPayment = 300m,
            TotalCost = 10800m,
            Fees = 100m,
            Notes = "Special promotional rate"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(offer.OfferId, Is.EqualTo(offerId));
            Assert.That(offer.LoanId, Is.EqualTo(loanId));
            Assert.That(offer.LenderName, Is.EqualTo("Bank of America"));
            Assert.That(offer.LoanAmount, Is.EqualTo(10000m));
            Assert.That(offer.InterestRate, Is.EqualTo(5.5m));
            Assert.That(offer.TermMonths, Is.EqualTo(36));
            Assert.That(offer.MonthlyPayment, Is.EqualTo(300m));
            Assert.That(offer.TotalCost, Is.EqualTo(10800m));
            Assert.That(offer.Fees, Is.EqualTo(100m));
            Assert.That(offer.Notes, Is.EqualTo("Special promotional rate"));
        });
    }

    [Test]
    public void CalculateTotalCost_ValidValues_CalculatesCorrectly()
    {
        // Arrange
        var offer = new Offer
        {
            OfferId = Guid.NewGuid(),
            LoanId = Guid.NewGuid(),
            LenderName = "Test Bank",
            MonthlyPayment = 300m,
            TermMonths = 36,
            Fees = 100m,
            TotalCost = 0m
        };

        // Act
        offer.CalculateTotalCost();

        // Assert
        Assert.That(offer.TotalCost, Is.EqualTo(10900m)); // (300 * 36) + 100
    }

    [Test]
    public void CalculateTotalCost_NoFees_CalculatesCorrectly()
    {
        // Arrange
        var offer = new Offer
        {
            OfferId = Guid.NewGuid(),
            LoanId = Guid.NewGuid(),
            LenderName = "Test Bank",
            MonthlyPayment = 250m,
            TermMonths = 24,
            Fees = 0m,
            TotalCost = 0m
        };

        // Act
        offer.CalculateTotalCost();

        // Assert
        Assert.That(offer.TotalCost, Is.EqualTo(6000m)); // (250 * 24) + 0
    }

    [Test]
    public void CalculateTotalCost_HighFees_CalculatesCorrectly()
    {
        // Arrange
        var offer = new Offer
        {
            OfferId = Guid.NewGuid(),
            LoanId = Guid.NewGuid(),
            LenderName = "Test Bank",
            MonthlyPayment = 500m,
            TermMonths = 12,
            Fees = 500m,
            TotalCost = 0m
        };

        // Act
        offer.CalculateTotalCost();

        // Assert
        Assert.That(offer.TotalCost, Is.EqualTo(6500m)); // (500 * 12) + 500
    }

    [Test]
    public void CalculateAPR_ReturnsInterestRate()
    {
        // Arrange
        var offer = new Offer
        {
            OfferId = Guid.NewGuid(),
            LoanId = Guid.NewGuid(),
            LenderName = "Test Bank",
            InterestRate = 5.75m
        };

        // Act
        var apr = offer.CalculateAPR();

        // Assert
        Assert.That(apr, Is.EqualTo(5.75m));
    }

    [Test]
    public void CalculateAPR_DifferentRates_ReturnsCorrectValue()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var offer1 = new Offer { InterestRate = 3.5m };
            Assert.That(offer1.CalculateAPR(), Is.EqualTo(3.5m));

            var offer2 = new Offer { InterestRate = 10.0m };
            Assert.That(offer2.CalculateAPR(), Is.EqualTo(10.0m));

            var offer3 = new Offer { InterestRate = 0.0m };
            Assert.That(offer3.CalculateAPR(), Is.EqualTo(0.0m));
        });
    }

    [Test]
    public void Offer_WithZeroInterestRate_CanBeCreated()
    {
        // Arrange & Act
        var offer = new Offer
        {
            OfferId = Guid.NewGuid(),
            LoanId = Guid.NewGuid(),
            LenderName = "Test Bank",
            InterestRate = 0.0m
        };

        // Assert
        Assert.That(offer.InterestRate, Is.EqualTo(0.0m));
    }

    [Test]
    public void Offer_DifferentTermLengths_CanBeCreated()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Offer { TermMonths = 12 }, Throws.Nothing);
            Assert.That(() => new Offer { TermMonths = 24 }, Throws.Nothing);
            Assert.That(() => new Offer { TermMonths = 36 }, Throws.Nothing);
            Assert.That(() => new Offer { TermMonths = 60 }, Throws.Nothing);
            Assert.That(() => new Offer { TermMonths = 120 }, Throws.Nothing);
            Assert.That(() => new Offer { TermMonths = 360 }, Throws.Nothing);
        });
    }
}
