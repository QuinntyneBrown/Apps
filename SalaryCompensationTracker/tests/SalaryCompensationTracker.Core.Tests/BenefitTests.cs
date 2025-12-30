namespace SalaryCompensationTracker.Core.Tests;

public class BenefitTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesBenefit()
    {
        // Arrange & Act
        var benefit = new Benefit
        {
            BenefitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompensationId = Guid.NewGuid(),
            Name = "Health Insurance",
            Category = "Health",
            Description = "Comprehensive health coverage",
            EstimatedValue = 12000m,
            EmployerContribution = 10000m,
            EmployeeContribution = 2000m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(benefit.BenefitId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(benefit.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(benefit.CompensationId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(benefit.Name, Is.EqualTo("Health Insurance"));
            Assert.That(benefit.Category, Is.EqualTo("Health"));
            Assert.That(benefit.Description, Is.EqualTo("Comprehensive health coverage"));
            Assert.That(benefit.EstimatedValue, Is.EqualTo(12000m));
            Assert.That(benefit.EmployerContribution, Is.EqualTo(10000m));
            Assert.That(benefit.EmployeeContribution, Is.EqualTo(2000m));
        });
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var benefit = new Benefit();

        // Assert
        Assert.That(benefit.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Category_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var benefit = new Benefit();

        // Assert
        Assert.That(benefit.Category, Is.EqualTo(string.Empty));
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSet()
    {
        // Arrange & Act
        var benefit = new Benefit();

        // Assert
        Assert.That(benefit.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        Assert.That(benefit.CreatedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void CompensationId_CanBeNull()
    {
        // Arrange & Act
        var benefit = new Benefit
        {
            CompensationId = null
        };

        // Assert
        Assert.That(benefit.CompensationId, Is.Null);
    }

    [Test]
    public void Description_CanBeNull()
    {
        // Arrange & Act
        var benefit = new Benefit
        {
            Description = null
        };

        // Assert
        Assert.That(benefit.Description, Is.Null);
    }

    [Test]
    public void EstimatedValue_CanBeNull()
    {
        // Arrange & Act
        var benefit = new Benefit
        {
            EstimatedValue = null
        };

        // Assert
        Assert.That(benefit.EstimatedValue, Is.Null);
    }

    [Test]
    public void EmployerContribution_CanBeNull()
    {
        // Arrange & Act
        var benefit = new Benefit
        {
            EmployerContribution = null
        };

        // Assert
        Assert.That(benefit.EmployerContribution, Is.Null);
    }

    [Test]
    public void EmployeeContribution_CanBeNull()
    {
        // Arrange & Act
        var benefit = new Benefit
        {
            EmployeeContribution = null
        };

        // Assert
        Assert.That(benefit.EmployeeContribution, Is.Null);
    }

    [Test]
    public void UpdatedAt_CanBeNull()
    {
        // Arrange & Act
        var benefit = new Benefit
        {
            UpdatedAt = null
        };

        // Assert
        Assert.That(benefit.UpdatedAt, Is.Null);
    }

    [Test]
    public void Compensation_NavigationProperty_CanBeSet()
    {
        // Arrange
        var benefit = new Benefit();
        var compensation = new Compensation
        {
            CompensationId = Guid.NewGuid(),
            Employer = "Tech Corp"
        };

        // Act
        benefit.Compensation = compensation;

        // Assert
        Assert.That(benefit.Compensation, Is.Not.Null);
        Assert.That(benefit.Compensation.Employer, Is.EqualTo("Tech Corp"));
    }

    [Test]
    public void Category_HealthCategory_CanBeSet()
    {
        // Arrange & Act
        var benefit = new Benefit
        {
            Category = "Health"
        };

        // Assert
        Assert.That(benefit.Category, Is.EqualTo("Health"));
    }

    [Test]
    public void Category_RetirementCategory_CanBeSet()
    {
        // Arrange & Act
        var benefit = new Benefit
        {
            Category = "Retirement"
        };

        // Assert
        Assert.That(benefit.Category, Is.EqualTo("Retirement"));
    }

    [Test]
    public void Category_PTOCategory_CanBeSet()
    {
        // Arrange & Act
        var benefit = new Benefit
        {
            Category = "PTO"
        };

        // Assert
        Assert.That(benefit.Category, Is.EqualTo("PTO"));
    }

    [Test]
    public void EstimatedValue_LargeValue_CanBeSet()
    {
        // Arrange & Act
        var benefit = new Benefit
        {
            EstimatedValue = 50000m
        };

        // Assert
        Assert.That(benefit.EstimatedValue, Is.EqualTo(50000m));
    }

    [Test]
    public void EmployerContribution_GreaterThanEmployeeContribution_IsValid()
    {
        // Arrange & Act
        var benefit = new Benefit
        {
            EmployerContribution = 8000m,
            EmployeeContribution = 2000m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(benefit.EmployerContribution, Is.GreaterThan(benefit.EmployeeContribution));
            Assert.That(benefit.EmployerContribution, Is.EqualTo(8000m));
            Assert.That(benefit.EmployeeContribution, Is.EqualTo(2000m));
        });
    }
}
