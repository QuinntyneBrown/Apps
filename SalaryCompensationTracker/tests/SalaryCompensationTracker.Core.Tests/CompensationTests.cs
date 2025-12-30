namespace SalaryCompensationTracker.Core.Tests;

public class CompensationTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesCompensation()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            CompensationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompensationType = CompensationType.FullTime,
            Employer = "Tech Corp",
            JobTitle = "Senior Software Engineer",
            BaseSalary = 120000m,
            Currency = "USD",
            Bonus = 15000m,
            StockValue = 25000m,
            OtherCompensation = 5000m,
            TotalCompensation = 165000m,
            EffectiveDate = new DateTime(2024, 1, 1),
            Notes = "Annual review"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(compensation.CompensationId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(compensation.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(compensation.CompensationType, Is.EqualTo(CompensationType.FullTime));
            Assert.That(compensation.Employer, Is.EqualTo("Tech Corp"));
            Assert.That(compensation.JobTitle, Is.EqualTo("Senior Software Engineer"));
            Assert.That(compensation.BaseSalary, Is.EqualTo(120000m));
            Assert.That(compensation.Currency, Is.EqualTo("USD"));
            Assert.That(compensation.Bonus, Is.EqualTo(15000m));
            Assert.That(compensation.StockValue, Is.EqualTo(25000m));
        });
    }

    [Test]
    public void CalculateTotalCompensation_AllComponents_CalculatesCorrectly()
    {
        // Arrange
        var compensation = new Compensation
        {
            BaseSalary = 100000m,
            Bonus = 10000m,
            StockValue = 20000m,
            OtherCompensation = 5000m
        };

        // Act
        compensation.CalculateTotalCompensation();

        // Assert
        Assert.That(compensation.TotalCompensation, Is.EqualTo(135000m));
    }

    [Test]
    public void CalculateTotalCompensation_NoBonusOrStock_CalculatesBaseSalaryOnly()
    {
        // Arrange
        var compensation = new Compensation
        {
            BaseSalary = 80000m,
            Bonus = null,
            StockValue = null,
            OtherCompensation = null
        };

        // Act
        compensation.CalculateTotalCompensation();

        // Assert
        Assert.That(compensation.TotalCompensation, Is.EqualTo(80000m));
    }

    [Test]
    public void CalculateTotalCompensation_OnlyBaseSalaryAndBonus_CalculatesCorrectly()
    {
        // Arrange
        var compensation = new Compensation
        {
            BaseSalary = 90000m,
            Bonus = 5000m,
            StockValue = null,
            OtherCompensation = null
        };

        // Act
        compensation.CalculateTotalCompensation();

        // Assert
        Assert.That(compensation.TotalCompensation, Is.EqualTo(95000m));
    }

    [Test]
    public void CalculateTotalCompensation_UpdatesUpdatedAtTimestamp()
    {
        // Arrange
        var compensation = new Compensation
        {
            BaseSalary = 100000m,
            UpdatedAt = null
        };

        // Act
        compensation.CalculateTotalCompensation();

        // Assert
        Assert.That(compensation.UpdatedAt, Is.Not.Null);
        Assert.That(compensation.UpdatedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void Employer_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var compensation = new Compensation();

        // Assert
        Assert.That(compensation.Employer, Is.EqualTo(string.Empty));
    }

    [Test]
    public void JobTitle_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var compensation = new Compensation();

        // Assert
        Assert.That(compensation.JobTitle, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Currency_DefaultValue_IsUSD()
    {
        // Arrange & Act
        var compensation = new Compensation();

        // Assert
        Assert.That(compensation.Currency, Is.EqualTo("USD"));
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSet()
    {
        // Arrange & Act
        var compensation = new Compensation();

        // Assert
        Assert.That(compensation.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        Assert.That(compensation.CreatedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void Bonus_CanBeNull()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            Bonus = null
        };

        // Assert
        Assert.That(compensation.Bonus, Is.Null);
    }

    [Test]
    public void StockValue_CanBeNull()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            StockValue = null
        };

        // Assert
        Assert.That(compensation.StockValue, Is.Null);
    }

    [Test]
    public void OtherCompensation_CanBeNull()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            OtherCompensation = null
        };

        // Assert
        Assert.That(compensation.OtherCompensation, Is.Null);
    }

    [Test]
    public void EndDate_CanBeNull()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            EndDate = null
        };

        // Assert
        Assert.That(compensation.EndDate, Is.Null);
    }

    [Test]
    public void UpdatedAt_CanBeNull()
    {
        // Arrange & Act
        var compensation = new Compensation
        {
            UpdatedAt = null
        };

        // Assert
        Assert.That(compensation.UpdatedAt, Is.Null);
    }

    [Test]
    public void Benefits_DefaultValue_IsEmptyCollection()
    {
        // Arrange & Act
        var compensation = new Compensation();

        // Assert
        Assert.That(compensation.Benefits, Is.Not.Null);
        Assert.That(compensation.Benefits, Is.Empty);
    }

    [Test]
    public void Benefits_CanAddItems()
    {
        // Arrange
        var compensation = new Compensation();
        var benefit = new Benefit
        {
            BenefitId = Guid.NewGuid(),
            Name = "Health Insurance"
        };

        // Act
        compensation.Benefits.Add(benefit);

        // Assert
        Assert.That(compensation.Benefits, Has.Count.EqualTo(1));
        Assert.That(compensation.Benefits.First().Name, Is.EqualTo("Health Insurance"));
    }

    [Test]
    public void CalculateTotalCompensation_ZeroBaseSalary_CalculatesOtherComponents()
    {
        // Arrange
        var compensation = new Compensation
        {
            BaseSalary = 0m,
            Bonus = 5000m,
            StockValue = 10000m,
            OtherCompensation = 2000m
        };

        // Act
        compensation.CalculateTotalCompensation();

        // Assert
        Assert.That(compensation.TotalCompensation, Is.EqualTo(17000m));
    }
}
