namespace SideHustleIncomeTracker.Core.Tests;

public class TaxEstimateTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTaxEstimate()
    {
        // Arrange & Act
        var taxEstimate = new TaxEstimate
        {
            TaxEstimateId = Guid.NewGuid(),
            BusinessId = Guid.NewGuid(),
            TaxYear = 2024,
            Quarter = 1,
            NetProfit = 25000m,
            SelfEmploymentTax = 3532.50m,
            IncomeTax = 3000m,
            TotalEstimatedTax = 6532.50m,
            IsPaid = false,
            PaymentDate = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taxEstimate.TaxEstimateId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(taxEstimate.BusinessId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(taxEstimate.TaxYear, Is.EqualTo(2024));
            Assert.That(taxEstimate.Quarter, Is.EqualTo(1));
            Assert.That(taxEstimate.NetProfit, Is.EqualTo(25000m));
            Assert.That(taxEstimate.SelfEmploymentTax, Is.EqualTo(3532.50m));
            Assert.That(taxEstimate.IncomeTax, Is.EqualTo(3000m));
            Assert.That(taxEstimate.TotalEstimatedTax, Is.EqualTo(6532.50m));
            Assert.That(taxEstimate.IsPaid, Is.False);
        });
    }

    [Test]
    public void CalculateTotalTax_ValidAmounts_CalculatesCorrectly()
    {
        // Arrange
        var taxEstimate = new TaxEstimate
        {
            SelfEmploymentTax = 2500m,
            IncomeTax = 3500m
        };

        // Act
        taxEstimate.CalculateTotalTax();

        // Assert
        Assert.That(taxEstimate.TotalEstimatedTax, Is.EqualTo(6000m));
    }

    [Test]
    public void CalculateTotalTax_ZeroSelfEmploymentTax_CalculatesOnlyIncomeTax()
    {
        // Arrange
        var taxEstimate = new TaxEstimate
        {
            SelfEmploymentTax = 0m,
            IncomeTax = 4000m
        };

        // Act
        taxEstimate.CalculateTotalTax();

        // Assert
        Assert.That(taxEstimate.TotalEstimatedTax, Is.EqualTo(4000m));
    }

    [Test]
    public void CalculateTotalTax_ZeroIncomeTax_CalculatesOnlySelfEmploymentTax()
    {
        // Arrange
        var taxEstimate = new TaxEstimate
        {
            SelfEmploymentTax = 5000m,
            IncomeTax = 0m
        };

        // Act
        taxEstimate.CalculateTotalTax();

        // Assert
        Assert.That(taxEstimate.TotalEstimatedTax, Is.EqualTo(5000m));
    }

    [Test]
    public void CalculateTotalTax_BothZero_ReturnsZero()
    {
        // Arrange
        var taxEstimate = new TaxEstimate
        {
            SelfEmploymentTax = 0m,
            IncomeTax = 0m
        };

        // Act
        taxEstimate.CalculateTotalTax();

        // Assert
        Assert.That(taxEstimate.TotalEstimatedTax, Is.EqualTo(0m));
    }

    [Test]
    public void RecordPayment_UnpaidEstimate_SetsIsPaidToTrue()
    {
        // Arrange
        var taxEstimate = new TaxEstimate
        {
            IsPaid = false,
            PaymentDate = null
        };

        // Act
        taxEstimate.RecordPayment();

        // Assert
        Assert.That(taxEstimate.IsPaid, Is.True);
    }

    [Test]
    public void RecordPayment_UnpaidEstimate_SetsPaymentDate()
    {
        // Arrange
        var taxEstimate = new TaxEstimate
        {
            IsPaid = false,
            PaymentDate = null
        };
        var beforePayment = DateTime.UtcNow;

        // Act
        taxEstimate.RecordPayment();

        // Assert
        Assert.That(taxEstimate.PaymentDate, Is.Not.Null);
        Assert.That(taxEstimate.PaymentDate, Is.GreaterThanOrEqualTo(beforePayment));
        Assert.That(taxEstimate.PaymentDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void RecordPayment_AlreadyPaid_UpdatesPaymentDate()
    {
        // Arrange
        var taxEstimate = new TaxEstimate
        {
            IsPaid = true,
            PaymentDate = DateTime.UtcNow.AddDays(-10)
        };
        var originalPaymentDate = taxEstimate.PaymentDate;

        // Act
        System.Threading.Thread.Sleep(10); // Small delay to ensure time difference
        taxEstimate.RecordPayment();

        // Assert
        Assert.That(taxEstimate.IsPaid, Is.True);
        Assert.That(taxEstimate.PaymentDate, Is.GreaterThan(originalPaymentDate));
    }

    [Test]
    public void IsPaid_DefaultValue_IsFalse()
    {
        // Arrange & Act
        var taxEstimate = new TaxEstimate();

        // Assert
        Assert.That(taxEstimate.IsPaid, Is.False);
    }

    [Test]
    public void PaymentDate_DefaultValue_IsNull()
    {
        // Arrange & Act
        var taxEstimate = new TaxEstimate();

        // Assert
        Assert.That(taxEstimate.PaymentDate, Is.Null);
    }

    [Test]
    public void Quarter_CanBeSetToValidValues()
    {
        // Arrange & Act
        var q1 = new TaxEstimate { Quarter = 1 };
        var q2 = new TaxEstimate { Quarter = 2 };
        var q3 = new TaxEstimate { Quarter = 3 };
        var q4 = new TaxEstimate { Quarter = 4 };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(q1.Quarter, Is.EqualTo(1));
            Assert.That(q2.Quarter, Is.EqualTo(2));
            Assert.That(q3.Quarter, Is.EqualTo(3));
            Assert.That(q4.Quarter, Is.EqualTo(4));
        });
    }

    [Test]
    public void TaxYear_CanBeSetToAnyYear()
    {
        // Arrange & Act
        var taxEstimate = new TaxEstimate
        {
            TaxYear = 2025
        };

        // Assert
        Assert.That(taxEstimate.TaxYear, Is.EqualTo(2025));
    }

    [Test]
    public void Business_NavigationProperty_CanBeSet()
    {
        // Arrange
        var taxEstimate = new TaxEstimate();
        var business = new Business
        {
            BusinessId = Guid.NewGuid(),
            Name = "My Business"
        };

        // Act
        taxEstimate.Business = business;

        // Assert
        Assert.That(taxEstimate.Business, Is.Not.Null);
        Assert.That(taxEstimate.Business.Name, Is.EqualTo("My Business"));
    }

    [Test]
    public void NetProfit_CanBeNegative()
    {
        // Arrange & Act
        var taxEstimate = new TaxEstimate
        {
            NetProfit = -5000m
        };

        // Assert
        Assert.That(taxEstimate.NetProfit, Is.EqualTo(-5000m));
    }

    [Test]
    public void CalculateTotalTax_LargeAmounts_CalculatesCorrectly()
    {
        // Arrange
        var taxEstimate = new TaxEstimate
        {
            SelfEmploymentTax = 15000m,
            IncomeTax = 25000m
        };

        // Act
        taxEstimate.CalculateTotalTax();

        // Assert
        Assert.That(taxEstimate.TotalEstimatedTax, Is.EqualTo(40000m));
    }

    [Test]
    public void PaymentDate_CanBeSetExplicitly()
    {
        // Arrange
        var specificDate = new DateTime(2024, 4, 15);
        var taxEstimate = new TaxEstimate
        {
            PaymentDate = specificDate
        };

        // Assert
        Assert.That(taxEstimate.PaymentDate, Is.EqualTo(specificDate));
    }
}
