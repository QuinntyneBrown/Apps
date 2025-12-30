namespace PersonalLoanComparisonTool.Core.Tests;

public class LoanTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesLoan()
    {
        // Arrange
        var loanId = Guid.NewGuid();

        // Act
        var loan = new Loan
        {
            LoanId = loanId,
            Name = "Home Purchase Loan",
            LoanType = LoanType.Home,
            RequestedAmount = 250000m,
            Purpose = "Purchase primary residence",
            CreditScore = 750,
            Notes = "Pre-approved application"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(loan.LoanId, Is.EqualTo(loanId));
            Assert.That(loan.Name, Is.EqualTo("Home Purchase Loan"));
            Assert.That(loan.LoanType, Is.EqualTo(LoanType.Home));
            Assert.That(loan.RequestedAmount, Is.EqualTo(250000m));
            Assert.That(loan.Purpose, Is.EqualTo("Purchase primary residence"));
            Assert.That(loan.CreditScore, Is.EqualTo(750));
            Assert.That(loan.Notes, Is.EqualTo("Pre-approved application"));
            Assert.That(loan.Offers, Is.Not.Null);
        });
    }

    [Test]
    public void GetBestOffer_NoOffers_ReturnsNull()
    {
        // Arrange
        var loan = new Loan
        {
            LoanId = Guid.NewGuid(),
            Name = "Test Loan",
            LoanType = LoanType.Personal,
            RequestedAmount = 10000m,
            Offers = new List<Offer>()
        };

        // Act
        var bestOffer = loan.GetBestOffer();

        // Assert
        Assert.That(bestOffer, Is.Null);
    }

    [Test]
    public void GetBestOffer_SingleOffer_ReturnsThatOffer()
    {
        // Arrange
        var offerId = Guid.NewGuid();
        var loan = new Loan
        {
            LoanId = Guid.NewGuid(),
            Name = "Test Loan",
            LoanType = LoanType.Personal,
            RequestedAmount = 10000m,
            Offers = new List<Offer>
            {
                new Offer
                {
                    OfferId = offerId,
                    LenderName = "Bank A",
                    TotalCost = 11000m
                }
            }
        };

        // Act
        var bestOffer = loan.GetBestOffer();

        // Assert
        Assert.That(bestOffer, Is.Not.Null);
        Assert.That(bestOffer.OfferId, Is.EqualTo(offerId));
    }

    [Test]
    public void GetBestOffer_MultipleOffers_ReturnsLowestTotalCost()
    {
        // Arrange
        var bestOfferId = Guid.NewGuid();
        var loan = new Loan
        {
            LoanId = Guid.NewGuid(),
            Name = "Test Loan",
            LoanType = LoanType.Personal,
            RequestedAmount = 10000m,
            Offers = new List<Offer>
            {
                new Offer
                {
                    OfferId = Guid.NewGuid(),
                    LenderName = "Bank A",
                    TotalCost = 12000m
                },
                new Offer
                {
                    OfferId = bestOfferId,
                    LenderName = "Bank B",
                    TotalCost = 10500m
                },
                new Offer
                {
                    OfferId = Guid.NewGuid(),
                    LenderName = "Bank C",
                    TotalCost = 11500m
                }
            }
        };

        // Act
        var bestOffer = loan.GetBestOffer();

        // Assert
        Assert.That(bestOffer, Is.Not.Null);
        Assert.That(bestOffer.OfferId, Is.EqualTo(bestOfferId));
        Assert.That(bestOffer.TotalCost, Is.EqualTo(10500m));
    }

    [Test]
    public void GetBestOffer_TiedOffers_ReturnsFirstOne()
    {
        // Arrange
        var firstOfferId = Guid.NewGuid();
        var loan = new Loan
        {
            LoanId = Guid.NewGuid(),
            Name = "Test Loan",
            LoanType = LoanType.Personal,
            RequestedAmount = 10000m,
            Offers = new List<Offer>
            {
                new Offer
                {
                    OfferId = firstOfferId,
                    LenderName = "Bank A",
                    TotalCost = 11000m
                },
                new Offer
                {
                    OfferId = Guid.NewGuid(),
                    LenderName = "Bank B",
                    TotalCost = 11000m
                }
            }
        };

        // Act
        var bestOffer = loan.GetBestOffer();

        // Assert
        Assert.That(bestOffer, Is.Not.Null);
        Assert.That(bestOffer.OfferId, Is.EqualTo(firstOfferId));
    }

    [Test]
    public void Loan_AllLoanTypes_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Loan { LoanType = LoanType.Personal }, Throws.Nothing);
            Assert.That(() => new Loan { LoanType = LoanType.Auto }, Throws.Nothing);
            Assert.That(() => new Loan { LoanType = LoanType.Home }, Throws.Nothing);
            Assert.That(() => new Loan { LoanType = LoanType.Student }, Throws.Nothing);
            Assert.That(() => new Loan { LoanType = LoanType.Business }, Throws.Nothing);
        });
    }
}
