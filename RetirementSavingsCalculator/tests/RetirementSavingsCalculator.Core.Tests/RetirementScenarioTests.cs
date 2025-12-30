namespace RetirementSavingsCalculator.Core.Tests;

public class RetirementScenarioTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesRetirementScenario()
    {
        // Arrange & Act
        var scenario = new RetirementScenario
        {
            RetirementScenarioId = Guid.NewGuid(),
            Name = "Conservative Plan",
            CurrentAge = 30,
            RetirementAge = 65,
            LifeExpectancyAge = 85,
            CurrentSavings = 50000m,
            AnnualContribution = 10000m,
            ExpectedReturnRate = 7m,
            InflationRate = 3m,
            ProjectedAnnualIncome = 30000m,
            ProjectedAnnualExpenses = 50000m,
            Notes = "My retirement plan"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(scenario.RetirementScenarioId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(scenario.Name, Is.EqualTo("Conservative Plan"));
            Assert.That(scenario.CurrentAge, Is.EqualTo(30));
            Assert.That(scenario.RetirementAge, Is.EqualTo(65));
            Assert.That(scenario.LifeExpectancyAge, Is.EqualTo(85));
            Assert.That(scenario.CurrentSavings, Is.EqualTo(50000m));
            Assert.That(scenario.AnnualContribution, Is.EqualTo(10000m));
            Assert.That(scenario.ExpectedReturnRate, Is.EqualTo(7m));
            Assert.That(scenario.InflationRate, Is.EqualTo(3m));
            Assert.That(scenario.ProjectedAnnualIncome, Is.EqualTo(30000m));
            Assert.That(scenario.ProjectedAnnualExpenses, Is.EqualTo(50000m));
        });
    }

    [Test]
    public void CalculateProjectedSavings_WithYearsToRetirement_CalculatesCorrectly()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            CurrentAge = 30,
            RetirementAge = 35, // 5 years to retirement
            CurrentSavings = 10000m,
            AnnualContribution = 5000m,
            ExpectedReturnRate = 6m // 6% annual return
        };

        // Act
        var projectedSavings = scenario.CalculateProjectedSavings();

        // Assert
        Assert.That(projectedSavings, Is.GreaterThan(10000m)); // Should be greater than initial
        Assert.That(projectedSavings, Is.GreaterThan(35000m)); // Should include contributions and growth
    }

    [Test]
    public void CalculateProjectedSavings_AlreadyRetired_ReturnsCurrentSavings()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            CurrentAge = 70,
            RetirementAge = 65,
            CurrentSavings = 500000m,
            AnnualContribution = 0m,
            ExpectedReturnRate = 5m
        };

        // Act
        var projectedSavings = scenario.CalculateProjectedSavings();

        // Assert
        Assert.That(projectedSavings, Is.EqualTo(500000m));
    }

    [Test]
    public void CalculateProjectedSavings_NoYearsToRetirement_ReturnsCurrentSavings()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            CurrentAge = 65,
            RetirementAge = 65,
            CurrentSavings = 250000m,
            AnnualContribution = 5000m,
            ExpectedReturnRate = 7m
        };

        // Act
        var projectedSavings = scenario.CalculateProjectedSavings();

        // Assert
        Assert.That(projectedSavings, Is.EqualTo(250000m));
    }

    [Test]
    public void CalculateAnnualWithdrawal_ExpensesGreaterThanIncome_ReturnsPositiveValue()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            ProjectedAnnualIncome = 20000m,
            ProjectedAnnualExpenses = 60000m
        };

        // Act
        var withdrawal = scenario.CalculateAnnualWithdrawal();

        // Assert
        Assert.That(withdrawal, Is.EqualTo(40000m));
    }

    [Test]
    public void CalculateAnnualWithdrawal_IncomeCoverExpenses_ReturnsNegativeValue()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            ProjectedAnnualIncome = 80000m,
            ProjectedAnnualExpenses = 50000m
        };

        // Act
        var withdrawal = scenario.CalculateAnnualWithdrawal();

        // Assert
        Assert.That(withdrawal, Is.EqualTo(-30000m));
    }

    [Test]
    public void CalculateAnnualWithdrawal_IncomeEqualsExpenses_ReturnsZero()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            ProjectedAnnualIncome = 50000m,
            ProjectedAnnualExpenses = 50000m
        };

        // Act
        var withdrawal = scenario.CalculateAnnualWithdrawal();

        // Assert
        Assert.That(withdrawal, Is.EqualTo(0m));
    }

    [Test]
    public void UpdateParameters_ValidValues_UpdatesCorrectly()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            RetirementAge = 65,
            AnnualContribution = 5000m,
            ExpectedReturnRate = 6m,
            LastUpdated = DateTime.UtcNow.AddDays(-10)
        };
        var initialLastUpdated = scenario.LastUpdated;

        // Act
        scenario.UpdateParameters(67, 7000m, 7.5m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(scenario.RetirementAge, Is.EqualTo(67));
            Assert.That(scenario.AnnualContribution, Is.EqualTo(7000m));
            Assert.That(scenario.ExpectedReturnRate, Is.EqualTo(7.5m));
            Assert.That(scenario.LastUpdated, Is.GreaterThan(initialLastUpdated));
        });
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var scenario = new RetirementScenario();

        // Assert
        Assert.That(scenario.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSet()
    {
        // Arrange & Act
        var scenario = new RetirementScenario();

        // Assert
        Assert.That(scenario.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        Assert.That(scenario.CreatedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void LastUpdated_DefaultValue_IsSet()
    {
        // Arrange & Act
        var scenario = new RetirementScenario();

        // Assert
        Assert.That(scenario.LastUpdated, Is.Not.EqualTo(default(DateTime)));
        Assert.That(scenario.LastUpdated, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void CalculateProjectedSavings_ZeroReturnRate_CalculatesOnlyContributions()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            CurrentAge = 30,
            RetirementAge = 31, // 1 year
            CurrentSavings = 10000m,
            AnnualContribution = 12000m, // $1000/month
            ExpectedReturnRate = 0m
        };

        // Act
        var projectedSavings = scenario.CalculateProjectedSavings();

        // Assert
        // With 0% return, should be close to initial + contributions
        Assert.That(projectedSavings, Is.GreaterThanOrEqualTo(21000m));
        Assert.That(projectedSavings, Is.LessThan(23000m));
    }

    [Test]
    public void CalculateProjectedSavings_HighReturnRate_AccumulatesSignificantly()
    {
        // Arrange
        var scenario = new RetirementScenario
        {
            CurrentAge = 25,
            RetirementAge = 30, // 5 years
            CurrentSavings = 10000m,
            AnnualContribution = 10000m,
            ExpectedReturnRate = 12m // High return rate
        };

        // Act
        var projectedSavings = scenario.CalculateProjectedSavings();

        // Assert
        Assert.That(projectedSavings, Is.GreaterThan(60000m)); // Should be significantly more due to compounding
    }

    [Test]
    public void UpdateParameters_UpdatesLastUpdatedTimestamp()
    {
        // Arrange
        var scenario = new RetirementScenario();
        var beforeUpdate = DateTime.UtcNow;

        // Act
        System.Threading.Thread.Sleep(10); // Small delay to ensure time difference
        scenario.UpdateParameters(65, 5000m, 6m);

        // Assert
        Assert.That(scenario.LastUpdated, Is.GreaterThanOrEqualTo(beforeUpdate));
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var scenario = new RetirementScenario
        {
            Notes = null
        };

        // Assert
        Assert.That(scenario.Notes, Is.Null);
    }
}
