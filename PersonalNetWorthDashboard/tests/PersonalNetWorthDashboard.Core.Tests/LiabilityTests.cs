namespace PersonalNetWorthDashboard.Core.Tests;

public class LiabilityTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesLiability()
    {
        // Arrange
        var liabilityId = Guid.NewGuid();
        var dueDate = DateTime.UtcNow.AddMonths(1);

        // Act
        var liability = new Liability
        {
            LiabilityId = liabilityId,
            Name = "Home Mortgage",
            LiabilityType = LiabilityType.Mortgage,
            CurrentBalance = 250000m,
            OriginalAmount = 300000m,
            InterestRate = 3.5m,
            MonthlyPayment = 1500m,
            Creditor = "First National Bank",
            AccountNumber = "LOAN-12345",
            DueDate = dueDate,
            Notes = "30-year fixed rate",
            IsActive = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(liability.LiabilityId, Is.EqualTo(liabilityId));
            Assert.That(liability.Name, Is.EqualTo("Home Mortgage"));
            Assert.That(liability.LiabilityType, Is.EqualTo(LiabilityType.Mortgage));
            Assert.That(liability.CurrentBalance, Is.EqualTo(250000m));
            Assert.That(liability.OriginalAmount, Is.EqualTo(300000m));
            Assert.That(liability.InterestRate, Is.EqualTo(3.5m));
            Assert.That(liability.MonthlyPayment, Is.EqualTo(1500m));
            Assert.That(liability.Creditor, Is.EqualTo("First National Bank"));
            Assert.That(liability.AccountNumber, Is.EqualTo("LOAN-12345"));
            Assert.That(liability.DueDate, Is.EqualTo(dueDate));
            Assert.That(liability.Notes, Is.EqualTo("30-year fixed rate"));
            Assert.That(liability.IsActive, Is.True);
            Assert.That(liability.LastUpdated, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void UpdateBalance_ValidPositiveBalance_UpdatesBalanceAndTimestamp()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Test Liability",
            LiabilityType = LiabilityType.CreditCard,
            CurrentBalance = 5000m
        };

        // Act
        liability.UpdateBalance(4000m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(liability.CurrentBalance, Is.EqualTo(4000m));
            Assert.That(liability.LastUpdated, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void UpdateBalance_ZeroBalance_UpdatesCorrectly()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Test Liability",
            LiabilityType = LiabilityType.CreditCard,
            CurrentBalance = 5000m
        };

        // Act
        liability.UpdateBalance(0m);

        // Assert
        Assert.That(liability.CurrentBalance, Is.EqualTo(0m));
    }

    [Test]
    public void UpdateBalance_NegativeBalance_ThrowsArgumentException()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Test Liability",
            LiabilityType = LiabilityType.CreditCard,
            CurrentBalance = 5000m
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => liability.UpdateBalance(-100m));
    }

    [Test]
    public void RecordPayment_ValidPayment_ReducesBalance()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Credit Card",
            LiabilityType = LiabilityType.CreditCard,
            CurrentBalance = 5000m
        };

        // Act
        liability.RecordPayment(1000m);

        // Assert
        Assert.That(liability.CurrentBalance, Is.EqualTo(4000m));
    }

    [Test]
    public void RecordPayment_PaymentLargerThanBalance_SetsBalanceToZero()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Credit Card",
            LiabilityType = LiabilityType.CreditCard,
            CurrentBalance = 1000m
        };

        // Act
        liability.RecordPayment(1500m);

        // Assert
        Assert.That(liability.CurrentBalance, Is.EqualTo(0m));
    }

    [Test]
    public void RecordPayment_NegativePayment_ThrowsArgumentException()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Credit Card",
            LiabilityType = LiabilityType.CreditCard,
            CurrentBalance = 5000m
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => liability.RecordPayment(-100m));
    }

    [Test]
    public void RecordPayment_ZeroPayment_ThrowsArgumentException()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Credit Card",
            LiabilityType = LiabilityType.CreditCard,
            CurrentBalance = 5000m
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => liability.RecordPayment(0m));
    }

    [Test]
    public void RecordPayment_UpdatesLastUpdated()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Credit Card",
            LiabilityType = LiabilityType.CreditCard,
            CurrentBalance = 5000m
        };

        // Act
        liability.RecordPayment(100m);

        // Assert
        Assert.That(liability.LastUpdated, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void MarkAsPaidOff_SetsBalanceToZeroAndDeactivates()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Auto Loan",
            LiabilityType = LiabilityType.AutoLoan,
            CurrentBalance = 5000m,
            IsActive = true
        };

        // Act
        liability.MarkAsPaidOff();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(liability.CurrentBalance, Is.EqualTo(0m));
            Assert.That(liability.IsActive, Is.False);
            Assert.That(liability.LastUpdated, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Liability_DefaultIsActive_IsTrue()
    {
        // Arrange & Act
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Test Liability",
            LiabilityType = LiabilityType.CreditCard
        };

        // Assert
        Assert.That(liability.IsActive, Is.True);
    }

    [Test]
    public void Liability_AllLiabilityTypes_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Liability { LiabilityType = LiabilityType.Mortgage }, Throws.Nothing);
            Assert.That(() => new Liability { LiabilityType = LiabilityType.AutoLoan }, Throws.Nothing);
            Assert.That(() => new Liability { LiabilityType = LiabilityType.StudentLoan }, Throws.Nothing);
            Assert.That(() => new Liability { LiabilityType = LiabilityType.CreditCard }, Throws.Nothing);
            Assert.That(() => new Liability { LiabilityType = LiabilityType.PersonalLoan }, Throws.Nothing);
            Assert.That(() => new Liability { LiabilityType = LiabilityType.MedicalDebt }, Throws.Nothing);
            Assert.That(() => new Liability { LiabilityType = LiabilityType.BusinessLoan }, Throws.Nothing);
            Assert.That(() => new Liability { LiabilityType = LiabilityType.Other }, Throws.Nothing);
        });
    }
}
