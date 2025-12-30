namespace RoadsideAssistanceInfoHub.Core.Tests;

public class InsuranceInfoTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesInsuranceInfo()
    {
        // Arrange & Act
        var insurance = new InsuranceInfo
        {
            InsuranceInfoId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            InsuranceCompany = "State Farm",
            PolicyNumber = "SF-123456",
            PolicyHolder = "John Doe",
            PolicyStartDate = new DateTime(2024, 1, 1),
            PolicyEndDate = new DateTime(2024, 12, 31),
            AgentName = "Jane Agent",
            AgentPhone = "555-1234",
            CompanyPhone = "1-800-STATE-FARM",
            ClaimsPhone = "1-800-CLAIMS",
            CoverageType = "Full Coverage",
            Deductible = 500m,
            Premium = 1200m,
            IncludesRoadsideAssistance = true,
            Notes = "Comprehensive coverage"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(insurance.InsuranceInfoId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(insurance.InsuranceCompany, Is.EqualTo("State Farm"));
            Assert.That(insurance.PolicyNumber, Is.EqualTo("SF-123456"));
            Assert.That(insurance.PolicyHolder, Is.EqualTo("John Doe"));
            Assert.That(insurance.Deductible, Is.EqualTo(500m));
            Assert.That(insurance.Premium, Is.EqualTo(1200m));
            Assert.That(insurance.IncludesRoadsideAssistance, Is.True);
        });
    }

    [Test]
    public void IsActive_CurrentDateWithinRange_ReturnsTrue()
    {
        // Arrange
        var insurance = new InsuranceInfo
        {
            PolicyStartDate = DateTime.UtcNow.AddDays(-30),
            PolicyEndDate = DateTime.UtcNow.AddDays(30)
        };

        // Act
        var isActive = insurance.IsActive(DateTime.UtcNow);

        // Assert
        Assert.That(isActive, Is.True);
    }

    [Test]
    public void IsActive_CurrentDateBeforeStart_ReturnsFalse()
    {
        // Arrange
        var insurance = new InsuranceInfo
        {
            PolicyStartDate = DateTime.UtcNow.AddDays(10),
            PolicyEndDate = DateTime.UtcNow.AddDays(40)
        };

        // Act
        var isActive = insurance.IsActive(DateTime.UtcNow);

        // Assert
        Assert.That(isActive, Is.False);
    }

    [Test]
    public void IsActive_CurrentDateAfterEnd_ReturnsFalse()
    {
        // Arrange
        var insurance = new InsuranceInfo
        {
            PolicyStartDate = DateTime.UtcNow.AddDays(-60),
            PolicyEndDate = DateTime.UtcNow.AddDays(-10)
        };

        // Act
        var isActive = insurance.IsActive(DateTime.UtcNow);

        // Assert
        Assert.That(isActive, Is.False);
    }

    [Test]
    public void IsActive_CurrentDateEqualsStartDate_ReturnsTrue()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var insurance = new InsuranceInfo
        {
            PolicyStartDate = startDate,
            PolicyEndDate = new DateTime(2024, 12, 31)
        };

        // Act
        var isActive = insurance.IsActive(startDate);

        // Assert
        Assert.That(isActive, Is.True);
    }

    [Test]
    public void IsExpiringSoon_WithinThreshold_ReturnsTrue()
    {
        // Arrange
        var insurance = new InsuranceInfo
        {
            PolicyStartDate = DateTime.UtcNow.AddDays(-300),
            PolicyEndDate = DateTime.UtcNow.AddDays(20)
        };

        // Act
        var isExpiring = insurance.IsExpiringSoon(DateTime.UtcNow, 30);

        // Assert
        Assert.That(isExpiring, Is.True);
    }

    [Test]
    public void IsExpiringSoon_BeyondThreshold_ReturnsFalse()
    {
        // Arrange
        var insurance = new InsuranceInfo
        {
            PolicyStartDate = DateTime.UtcNow,
            PolicyEndDate = DateTime.UtcNow.AddDays(60)
        };

        // Act
        var isExpiring = insurance.IsExpiringSoon(DateTime.UtcNow, 30);

        // Assert
        Assert.That(isExpiring, Is.False);
    }

    [Test]
    public void IsExpiringSoon_AlreadyExpired_ReturnsFalse()
    {
        // Arrange
        var insurance = new InsuranceInfo
        {
            PolicyStartDate = DateTime.UtcNow.AddDays(-60),
            PolicyEndDate = DateTime.UtcNow.AddDays(-5)
        };

        // Act
        var isExpiring = insurance.IsExpiringSoon(DateTime.UtcNow, 30);

        // Assert
        Assert.That(isExpiring, Is.False);
    }

    [Test]
    public void IsExpiringSoon_DefaultThreshold_Uses30Days()
    {
        // Arrange
        var insurance = new InsuranceInfo
        {
            PolicyStartDate = DateTime.UtcNow.AddDays(-300),
            PolicyEndDate = DateTime.UtcNow.AddDays(25)
        };

        // Act
        var isExpiring = insurance.IsExpiringSoon(DateTime.UtcNow);

        // Assert
        Assert.That(isExpiring, Is.True);
    }

    [Test]
    public void RenewPolicy_ValidEndDate_UpdatesDates()
    {
        // Arrange
        var insurance = new InsuranceInfo
        {
            PolicyStartDate = new DateTime(2024, 1, 1),
            PolicyEndDate = new DateTime(2024, 12, 31)
        };
        var newEndDate = new DateTime(2025, 12, 31);

        // Act
        insurance.RenewPolicy(newEndDate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(insurance.PolicyStartDate, Is.EqualTo(new DateTime(2024, 12, 31)));
            Assert.That(insurance.PolicyEndDate, Is.EqualTo(newEndDate));
        });
    }

    [Test]
    public void InsuranceCompany_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var insurance = new InsuranceInfo();

        // Assert
        Assert.That(insurance.InsuranceCompany, Is.EqualTo(string.Empty));
    }

    [Test]
    public void PolicyNumber_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var insurance = new InsuranceInfo();

        // Assert
        Assert.That(insurance.PolicyNumber, Is.EqualTo(string.Empty));
    }

    [Test]
    public void PolicyHolder_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var insurance = new InsuranceInfo();

        // Assert
        Assert.That(insurance.PolicyHolder, Is.EqualTo(string.Empty));
    }

    [Test]
    public void IncludesRoadsideAssistance_DefaultValue_IsFalse()
    {
        // Arrange & Act
        var insurance = new InsuranceInfo();

        // Assert
        Assert.That(insurance.IncludesRoadsideAssistance, Is.False);
    }

    [Test]
    public void Deductible_CanBeNull()
    {
        // Arrange & Act
        var insurance = new InsuranceInfo
        {
            Deductible = null
        };

        // Assert
        Assert.That(insurance.Deductible, Is.Null);
    }

    [Test]
    public void Premium_CanBeNull()
    {
        // Arrange & Act
        var insurance = new InsuranceInfo
        {
            Premium = null
        };

        // Assert
        Assert.That(insurance.Premium, Is.Null);
    }

    [Test]
    public void Vehicle_NavigationProperty_CanBeSet()
    {
        // Arrange
        var insurance = new InsuranceInfo();
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Ford",
            Model = "F-150"
        };

        // Act
        insurance.Vehicle = vehicle;

        // Assert
        Assert.That(insurance.Vehicle, Is.Not.Null);
        Assert.That(insurance.Vehicle.Make, Is.EqualTo("Ford"));
    }
}
