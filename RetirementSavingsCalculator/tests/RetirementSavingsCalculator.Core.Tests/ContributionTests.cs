namespace RetirementSavingsCalculator.Core.Tests;

public class ContributionTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesContribution()
    {
        // Arrange & Act
        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            RetirementScenarioId = Guid.NewGuid(),
            Amount = 5000m,
            ContributionDate = DateTime.UtcNow,
            AccountName = "401(k)",
            IsEmployerMatch = false,
            Notes = "Regular contribution"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contribution.ContributionId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(contribution.RetirementScenarioId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(contribution.Amount, Is.EqualTo(5000m));
            Assert.That(contribution.AccountName, Is.EqualTo("401(k)"));
            Assert.That(contribution.IsEmployerMatch, Is.False);
            Assert.That(contribution.Notes, Is.EqualTo("Regular contribution"));
        });
    }

    [Test]
    public void ValidateAmount_PositiveAmount_DoesNotThrow()
    {
        // Arrange
        var contribution = new Contribution
        {
            Amount = 1000m
        };

        // Act & Assert
        Assert.DoesNotThrow(() => contribution.ValidateAmount());
    }

    [Test]
    public void ValidateAmount_ZeroAmount_ThrowsArgumentException()
    {
        // Arrange
        var contribution = new Contribution
        {
            Amount = 0m
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => contribution.ValidateAmount());
        Assert.That(ex.Message, Does.Contain("Contribution amount must be positive"));
        Assert.That(ex.ParamName, Is.EqualTo("Amount"));
    }

    [Test]
    public void ValidateAmount_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        var contribution = new Contribution
        {
            Amount = -100m
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => contribution.ValidateAmount());
        Assert.That(ex.Message, Does.Contain("Contribution amount must be positive"));
    }

    [Test]
    public void AccountName_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var contribution = new Contribution();

        // Assert
        Assert.That(contribution.AccountName, Is.EqualTo(string.Empty));
    }

    [Test]
    public void IsEmployerMatch_DefaultValue_IsFalse()
    {
        // Arrange & Act
        var contribution = new Contribution();

        // Assert
        Assert.That(contribution.IsEmployerMatch, Is.False);
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var contribution = new Contribution
        {
            Notes = null
        };

        // Assert
        Assert.That(contribution.Notes, Is.Null);
    }

    [Test]
    public void IsEmployerMatch_SetToTrue_StoresCorrectly()
    {
        // Arrange & Act
        var contribution = new Contribution
        {
            IsEmployerMatch = true
        };

        // Assert
        Assert.That(contribution.IsEmployerMatch, Is.True);
    }

    [Test]
    public void ContributionDate_SetToSpecificDate_StoresCorrectly()
    {
        // Arrange
        var specificDate = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc);
        var contribution = new Contribution
        {
            ContributionDate = specificDate
        };

        // Act & Assert
        Assert.That(contribution.ContributionDate, Is.EqualTo(specificDate));
    }

    [Test]
    public void RetirementScenario_NavigationProperty_CanBeSet()
    {
        // Arrange
        var contribution = new Contribution();
        var scenario = new RetirementScenario
        {
            RetirementScenarioId = Guid.NewGuid(),
            Name = "Test Scenario"
        };

        // Act
        contribution.RetirementScenario = scenario;

        // Assert
        Assert.That(contribution.RetirementScenario, Is.Not.Null);
        Assert.That(contribution.RetirementScenario.Name, Is.EqualTo("Test Scenario"));
    }

    [Test]
    public void Amount_LargeValue_StoresCorrectly()
    {
        // Arrange & Act
        var contribution = new Contribution
        {
            Amount = 999999.99m
        };

        // Assert
        Assert.That(contribution.Amount, Is.EqualTo(999999.99m));
    }

    [Test]
    public void Amount_SmallPositiveValue_PassesValidation()
    {
        // Arrange
        var contribution = new Contribution
        {
            Amount = 0.01m
        };

        // Act & Assert
        Assert.DoesNotThrow(() => contribution.ValidateAmount());
    }
}
