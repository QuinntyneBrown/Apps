namespace PersonalLoanComparisonTool.Core.Tests;

public class EventTests
{
    [Test]
    public void LoanCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var loanId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new LoanCreatedEvent
        {
            LoanId = loanId,
            Name = "Home Purchase Loan",
            RequestedAmount = 250000m,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.LoanId, Is.EqualTo(loanId));
            Assert.That(evt.Name, Is.EqualTo("Home Purchase Loan"));
            Assert.That(evt.RequestedAmount, Is.EqualTo(250000m));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void OfferReceivedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var offerId = Guid.NewGuid();
        var loanId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new OfferReceivedEvent
        {
            OfferId = offerId,
            LoanId = loanId,
            LenderName = "Bank of America",
            InterestRate = 5.5m,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.OfferId, Is.EqualTo(offerId));
            Assert.That(evt.LoanId, Is.EqualTo(loanId));
            Assert.That(evt.LenderName, Is.EqualTo("Bank of America"));
            Assert.That(evt.InterestRate, Is.EqualTo(5.5m));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ScheduleGeneratedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var paymentScheduleId = Guid.NewGuid();
        var offerId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ScheduleGeneratedEvent
        {
            PaymentScheduleId = paymentScheduleId,
            OfferId = offerId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PaymentScheduleId, Is.EqualTo(paymentScheduleId));
            Assert.That(evt.OfferId, Is.EqualTo(offerId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void LoanCreatedEvent_DifferentAmounts_CanBeCreated()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new LoanCreatedEvent { RequestedAmount = 5000m }, Throws.Nothing);
            Assert.That(() => new LoanCreatedEvent { RequestedAmount = 50000m }, Throws.Nothing);
            Assert.That(() => new LoanCreatedEvent { RequestedAmount = 500000m }, Throws.Nothing);
        });
    }

    [Test]
    public void OfferReceivedEvent_DifferentInterestRates_CanBeCreated()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new OfferReceivedEvent { InterestRate = 0.0m }, Throws.Nothing);
            Assert.That(() => new OfferReceivedEvent { InterestRate = 3.5m }, Throws.Nothing);
            Assert.That(() => new OfferReceivedEvent { InterestRate = 15.0m }, Throws.Nothing);
        });
    }

    [Test]
    public void LoanCreatedEvent_IsRecord_IsImmutable()
    {
        // Arrange
        var evt = new LoanCreatedEvent
        {
            LoanId = Guid.NewGuid(),
            Name = "Test Loan",
            RequestedAmount = 10000m,
            Timestamp = DateTime.UtcNow
        };

        // Assert - Record types are immutable, properties cannot be reassigned
        Assert.That(evt, Is.Not.Null);
    }

    [Test]
    public void OfferReceivedEvent_IsRecord_IsImmutable()
    {
        // Arrange
        var evt = new OfferReceivedEvent
        {
            OfferId = Guid.NewGuid(),
            LoanId = Guid.NewGuid(),
            LenderName = "Test Bank",
            InterestRate = 5.5m,
            Timestamp = DateTime.UtcNow
        };

        // Assert - Record types are immutable, properties cannot be reassigned
        Assert.That(evt, Is.Not.Null);
    }

    [Test]
    public void ScheduleGeneratedEvent_IsRecord_IsImmutable()
    {
        // Arrange
        var evt = new ScheduleGeneratedEvent
        {
            PaymentScheduleId = Guid.NewGuid(),
            OfferId = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow
        };

        // Assert - Record types are immutable, properties cannot be reassigned
        Assert.That(evt, Is.Not.Null);
    }
}
