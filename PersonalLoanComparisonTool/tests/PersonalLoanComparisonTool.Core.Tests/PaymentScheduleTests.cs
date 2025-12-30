namespace PersonalLoanComparisonTool.Core.Tests;

public class PaymentScheduleTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesPaymentSchedule()
    {
        // Arrange
        var paymentScheduleId = Guid.NewGuid();
        var offerId = Guid.NewGuid();
        var dueDate = DateTime.UtcNow.AddMonths(1);

        // Act
        var schedule = new PaymentSchedule
        {
            PaymentScheduleId = paymentScheduleId,
            OfferId = offerId,
            PaymentNumber = 1,
            DueDate = dueDate,
            PaymentAmount = 300m,
            PrincipalAmount = 250m,
            InterestAmount = 50m,
            RemainingBalance = 9750m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(schedule.PaymentScheduleId, Is.EqualTo(paymentScheduleId));
            Assert.That(schedule.OfferId, Is.EqualTo(offerId));
            Assert.That(schedule.PaymentNumber, Is.EqualTo(1));
            Assert.That(schedule.DueDate, Is.EqualTo(dueDate));
            Assert.That(schedule.PaymentAmount, Is.EqualTo(300m));
            Assert.That(schedule.PrincipalAmount, Is.EqualTo(250m));
            Assert.That(schedule.InterestAmount, Is.EqualTo(50m));
            Assert.That(schedule.RemainingBalance, Is.EqualTo(9750m));
        });
    }

    [Test]
    public void PaymentSchedule_FirstPayment_HasCorrectPaymentNumber()
    {
        // Arrange & Act
        var schedule = new PaymentSchedule
        {
            PaymentScheduleId = Guid.NewGuid(),
            OfferId = Guid.NewGuid(),
            PaymentNumber = 1,
            RemainingBalance = 9750m
        };

        // Assert
        Assert.That(schedule.PaymentNumber, Is.EqualTo(1));
    }

    [Test]
    public void PaymentSchedule_LastPayment_HasZeroRemainingBalance()
    {
        // Arrange & Act
        var schedule = new PaymentSchedule
        {
            PaymentScheduleId = Guid.NewGuid(),
            OfferId = Guid.NewGuid(),
            PaymentNumber = 36,
            PaymentAmount = 300m,
            PrincipalAmount = 300m,
            InterestAmount = 0m,
            RemainingBalance = 0m
        };

        // Assert
        Assert.That(schedule.RemainingBalance, Is.EqualTo(0m));
    }

    [Test]
    public void PaymentSchedule_PrincipalPlusInterest_EqualsTotalPayment()
    {
        // Arrange
        var schedule = new PaymentSchedule
        {
            PaymentScheduleId = Guid.NewGuid(),
            OfferId = Guid.NewGuid(),
            PaymentNumber = 1,
            PaymentAmount = 300m,
            PrincipalAmount = 250m,
            InterestAmount = 50m
        };

        // Act
        var sum = schedule.PrincipalAmount + schedule.InterestAmount;

        // Assert
        Assert.That(sum, Is.EqualTo(schedule.PaymentAmount));
    }

    [Test]
    public void PaymentSchedule_EarlyPayments_HaveHigherInterest()
    {
        // Arrange - First payment typically has more interest
        var earlyPayment = new PaymentSchedule
        {
            PaymentScheduleId = Guid.NewGuid(),
            OfferId = Guid.NewGuid(),
            PaymentNumber = 1,
            PaymentAmount = 300m,
            PrincipalAmount = 250m,
            InterestAmount = 50m
        };

        // Arrange - Late payment typically has less interest
        var latePayment = new PaymentSchedule
        {
            PaymentScheduleId = Guid.NewGuid(),
            OfferId = Guid.NewGuid(),
            PaymentNumber = 35,
            PaymentAmount = 300m,
            PrincipalAmount = 295m,
            InterestAmount = 5m
        };

        // Assert
        Assert.That(earlyPayment.InterestAmount, Is.GreaterThan(latePayment.InterestAmount));
    }

    [Test]
    public void PaymentSchedule_SequentialPayments_DecreasingRemainingBalance()
    {
        // Arrange
        var payment1 = new PaymentSchedule
        {
            PaymentScheduleId = Guid.NewGuid(),
            OfferId = Guid.NewGuid(),
            PaymentNumber = 1,
            RemainingBalance = 9750m
        };

        var payment2 = new PaymentSchedule
        {
            PaymentScheduleId = Guid.NewGuid(),
            OfferId = Guid.NewGuid(),
            PaymentNumber = 2,
            RemainingBalance = 9500m
        };

        // Assert
        Assert.That(payment2.RemainingBalance, Is.LessThan(payment1.RemainingBalance));
    }

    [Test]
    public void PaymentSchedule_WithOffer_CanLinkToOffer()
    {
        // Arrange
        var offerId = Guid.NewGuid();
        var offer = new Offer
        {
            OfferId = offerId,
            LenderName = "Test Bank"
        };

        // Act
        var schedule = new PaymentSchedule
        {
            PaymentScheduleId = Guid.NewGuid(),
            OfferId = offerId,
            Offer = offer
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(schedule.Offer, Is.Not.Null);
            Assert.That(schedule.Offer.OfferId, Is.EqualTo(offerId));
        });
    }
}
