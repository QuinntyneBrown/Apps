namespace SideHustleIncomeTracker.Core.Tests;

public class BusinessTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesBusiness()
    {
        // Arrange & Act
        var business = new Business
        {
            BusinessId = Guid.NewGuid(),
            Name = "Web Design Studio",
            Description = "Freelance web design services",
            StartDate = new DateTime(2024, 1, 1),
            IsActive = true,
            TaxId = "12-3456789",
            Notes = "LLC registered in CA",
            TotalIncome = 50000m,
            TotalExpenses = 15000m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(business.BusinessId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(business.Name, Is.EqualTo("Web Design Studio"));
            Assert.That(business.Description, Is.EqualTo("Freelance web design services"));
            Assert.That(business.StartDate, Is.EqualTo(new DateTime(2024, 1, 1)));
            Assert.That(business.IsActive, Is.True);
            Assert.That(business.TaxId, Is.EqualTo("12-3456789"));
            Assert.That(business.TotalIncome, Is.EqualTo(50000m));
            Assert.That(business.TotalExpenses, Is.EqualTo(15000m));
        });
    }

    [Test]
    public void CalculateNetProfit_PositiveProfit_ReturnsCorrectValue()
    {
        // Arrange
        var business = new Business
        {
            TotalIncome = 100000m,
            TotalExpenses = 40000m
        };

        // Act
        var netProfit = business.CalculateNetProfit();

        // Assert
        Assert.That(netProfit, Is.EqualTo(60000m));
    }

    [Test]
    public void CalculateNetProfit_NegativeProfit_ReturnsNegativeValue()
    {
        // Arrange
        var business = new Business
        {
            TotalIncome = 30000m,
            TotalExpenses = 50000m
        };

        // Act
        var netProfit = business.CalculateNetProfit();

        // Assert
        Assert.That(netProfit, Is.EqualTo(-20000m));
    }

    [Test]
    public void CalculateNetProfit_ZeroIncome_ReturnsNegativeExpenses()
    {
        // Arrange
        var business = new Business
        {
            TotalIncome = 0m,
            TotalExpenses = 10000m
        };

        // Act
        var netProfit = business.CalculateNetProfit();

        // Assert
        Assert.That(netProfit, Is.EqualTo(-10000m));
    }

    [Test]
    public void CalculateNetProfit_ZeroExpenses_ReturnsTotalIncome()
    {
        // Arrange
        var business = new Business
        {
            TotalIncome = 50000m,
            TotalExpenses = 0m
        };

        // Act
        var netProfit = business.CalculateNetProfit();

        // Assert
        Assert.That(netProfit, Is.EqualTo(50000m));
    }

    [Test]
    public void CalculateNetProfit_IncomeEqualsExpenses_ReturnsZero()
    {
        // Arrange
        var business = new Business
        {
            TotalIncome = 25000m,
            TotalExpenses = 25000m
        };

        // Act
        var netProfit = business.CalculateNetProfit();

        // Assert
        Assert.That(netProfit, Is.EqualTo(0m));
    }

    [Test]
    public void Close_ActiveBusiness_SetsIsActiveToFalse()
    {
        // Arrange
        var business = new Business
        {
            IsActive = true
        };

        // Act
        business.Close();

        // Assert
        Assert.That(business.IsActive, Is.False);
    }

    [Test]
    public void Close_AlreadyClosed_RemainsFalse()
    {
        // Arrange
        var business = new Business
        {
            IsActive = false
        };

        // Act
        business.Close();

        // Assert
        Assert.That(business.IsActive, Is.False);
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var business = new Business();

        // Assert
        Assert.That(business.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void IsActive_DefaultValue_IsTrue()
    {
        // Arrange & Act
        var business = new Business();

        // Assert
        Assert.That(business.IsActive, Is.True);
    }

    [Test]
    public void Description_CanBeNull()
    {
        // Arrange & Act
        var business = new Business
        {
            Description = null
        };

        // Assert
        Assert.That(business.Description, Is.Null);
    }

    [Test]
    public void TaxId_CanBeNull()
    {
        // Arrange & Act
        var business = new Business
        {
            TaxId = null
        };

        // Assert
        Assert.That(business.TaxId, Is.Null);
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var business = new Business
        {
            Notes = null
        };

        // Assert
        Assert.That(business.Notes, Is.Null);
    }

    [Test]
    public void TotalIncome_CanBeSetToZero()
    {
        // Arrange & Act
        var business = new Business
        {
            TotalIncome = 0m
        };

        // Assert
        Assert.That(business.TotalIncome, Is.EqualTo(0m));
    }

    [Test]
    public void TotalExpenses_CanBeSetToZero()
    {
        // Arrange & Act
        var business = new Business
        {
            TotalExpenses = 0m
        };

        // Assert
        Assert.That(business.TotalExpenses, Is.EqualTo(0m));
    }

    [Test]
    public void StartDate_CanBeSetToAnyDate()
    {
        // Arrange
        var specificDate = new DateTime(2020, 6, 15);
        var business = new Business
        {
            StartDate = specificDate
        };

        // Assert
        Assert.That(business.StartDate, Is.EqualTo(specificDate));
    }
}
