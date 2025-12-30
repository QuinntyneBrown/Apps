namespace RoadsideAssistanceInfoHub.Core.Tests;

public class PolicyTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesPolicy()
    {
        // Arrange & Act
        var policy = new Policy
        {
            PolicyId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            Provider = "AAA",
            PolicyNumber = "POL-123456",
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 12, 31),
            EmergencyPhone = "1-800-AAA-HELP",
            MaxTowingDistance = 100,
            ServiceCallsPerYear = 4,
            AnnualPremium = 89.99m,
            CoversBatteryService = true,
            CoversFlatTire = true,
            CoversFuelDelivery = true,
            CoversLockout = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(policy.PolicyId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(policy.Provider, Is.EqualTo("AAA"));
            Assert.That(policy.PolicyNumber, Is.EqualTo("POL-123456"));
            Assert.That(policy.EmergencyPhone, Is.EqualTo("1-800-AAA-HELP"));
            Assert.That(policy.MaxTowingDistance, Is.EqualTo(100));
            Assert.That(policy.ServiceCallsPerYear, Is.EqualTo(4));
            Assert.That(policy.CoversBatteryService, Is.True);
        });
    }

    [Test]
    public void IsActive_CurrentDateWithinRange_ReturnsTrue()
    {
        // Arrange
        var policy = new Policy
        {
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        // Act
        var isActive = policy.IsActive(DateTime.UtcNow);

        // Assert
        Assert.That(isActive, Is.True);
    }

    [Test]
    public void IsActive_CurrentDateBeforeStart_ReturnsFalse()
    {
        // Arrange
        var policy = new Policy
        {
            StartDate = DateTime.UtcNow.AddDays(10),
            EndDate = DateTime.UtcNow.AddDays(40)
        };

        // Act
        var isActive = policy.IsActive(DateTime.UtcNow);

        // Assert
        Assert.That(isActive, Is.False);
    }

    [Test]
    public void IsActive_CurrentDateAfterEnd_ReturnsFalse()
    {
        // Arrange
        var policy = new Policy
        {
            StartDate = DateTime.UtcNow.AddDays(-60),
            EndDate = DateTime.UtcNow.AddDays(-10)
        };

        // Act
        var isActive = policy.IsActive(DateTime.UtcNow);

        // Assert
        Assert.That(isActive, Is.False);
    }

    [Test]
    public void IsActive_CurrentDateEqualsStartDate_ReturnsTrue()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var policy = new Policy
        {
            StartDate = startDate,
            EndDate = new DateTime(2024, 12, 31)
        };

        // Act
        var isActive = policy.IsActive(startDate);

        // Assert
        Assert.That(isActive, Is.True);
    }

    [Test]
    public void IsExpiringSoon_WithinThreshold_ReturnsTrue()
    {
        // Arrange
        var policy = new Policy
        {
            StartDate = DateTime.UtcNow.AddDays(-300),
            EndDate = DateTime.UtcNow.AddDays(15)
        };

        // Act
        var isExpiring = policy.IsExpiringSoon(DateTime.UtcNow, 30);

        // Assert
        Assert.That(isExpiring, Is.True);
    }

    [Test]
    public void IsExpiringSoon_BeyondThreshold_ReturnsFalse()
    {
        // Arrange
        var policy = new Policy
        {
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(60)
        };

        // Act
        var isExpiring = policy.IsExpiringSoon(DateTime.UtcNow, 30);

        // Assert
        Assert.That(isExpiring, Is.False);
    }

    [Test]
    public void IsExpiringSoon_AlreadyExpired_ReturnsFalse()
    {
        // Arrange
        var policy = new Policy
        {
            StartDate = DateTime.UtcNow.AddDays(-60),
            EndDate = DateTime.UtcNow.AddDays(-5)
        };

        // Act
        var isExpiring = policy.IsExpiringSoon(DateTime.UtcNow, 30);

        // Assert
        Assert.That(isExpiring, Is.False);
    }

    [Test]
    public void IsExpiringSoon_DefaultThreshold_Uses30Days()
    {
        // Arrange
        var policy = new Policy
        {
            StartDate = DateTime.UtcNow.AddDays(-300),
            EndDate = DateTime.UtcNow.AddDays(25)
        };

        // Act
        var isExpiring = policy.IsExpiringSoon(DateTime.UtcNow);

        // Assert
        Assert.That(isExpiring, Is.True);
    }

    [Test]
    public void AddCoveredServices_ValidServices_AddsToList()
    {
        // Arrange
        var policy = new Policy();
        var services = new[] { "Towing", "Jump Start", "Tire Change" };

        // Act
        policy.AddCoveredServices(services);

        // Assert
        Assert.That(policy.CoveredServices, Has.Count.EqualTo(3));
        Assert.That(policy.CoveredServices, Does.Contain("Towing"));
        Assert.That(policy.CoveredServices, Does.Contain("Jump Start"));
    }

    [Test]
    public void AddCoveredServices_MultipleCalls_AppendsToList()
    {
        // Arrange
        var policy = new Policy();
        policy.AddCoveredServices(new[] { "Towing" });

        // Act
        policy.AddCoveredServices(new[] { "Jump Start", "Fuel Delivery" });

        // Assert
        Assert.That(policy.CoveredServices, Has.Count.EqualTo(3));
    }

    [Test]
    public void RenewPolicy_ValidEndDate_UpdatesDates()
    {
        // Arrange
        var policy = new Policy
        {
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 12, 31)
        };
        var newEndDate = new DateTime(2025, 12, 31);

        // Act
        policy.RenewPolicy(newEndDate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(policy.StartDate, Is.EqualTo(new DateTime(2024, 12, 31)));
            Assert.That(policy.EndDate, Is.EqualTo(newEndDate));
        });
    }

    [Test]
    public void Provider_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var policy = new Policy();

        // Assert
        Assert.That(policy.Provider, Is.EqualTo(string.Empty));
    }

    [Test]
    public void CoveredServices_DefaultValue_IsEmptyList()
    {
        // Arrange & Act
        var policy = new Policy();

        // Assert
        Assert.That(policy.CoveredServices, Is.Not.Null);
        Assert.That(policy.CoveredServices, Is.Empty);
    }

    [Test]
    public void CoverageFlags_DefaultValues_AreFalse()
    {
        // Arrange & Act
        var policy = new Policy();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(policy.CoversBatteryService, Is.False);
            Assert.That(policy.CoversFlatTire, Is.False);
            Assert.That(policy.CoversFuelDelivery, Is.False);
            Assert.That(policy.CoversLockout, Is.False);
        });
    }

    [Test]
    public void Vehicle_NavigationProperty_CanBeSet()
    {
        // Arrange
        var policy = new Policy();
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Toyota",
            Model = "Camry"
        };

        // Act
        policy.Vehicle = vehicle;

        // Assert
        Assert.That(policy.Vehicle, Is.Not.Null);
        Assert.That(policy.Vehicle.Make, Is.EqualTo("Toyota"));
    }
}
