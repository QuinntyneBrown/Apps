namespace RoadsideAssistanceInfoHub.Core.Tests;

public class VehicleTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesVehicle()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Honda",
            Model = "Civic",
            Year = 2022,
            VIN = "1HGBH41JXMN109186",
            LicensePlate = "ABC-123",
            Color = "Blue",
            CurrentMileage = 15000m,
            OwnerName = "John Doe",
            Notes = "Regular maintenance required",
            IsActive = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.VehicleId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(vehicle.Make, Is.EqualTo("Honda"));
            Assert.That(vehicle.Model, Is.EqualTo("Civic"));
            Assert.That(vehicle.Year, Is.EqualTo(2022));
            Assert.That(vehicle.VIN, Is.EqualTo("1HGBH41JXMN109186"));
            Assert.That(vehicle.LicensePlate, Is.EqualTo("ABC-123"));
            Assert.That(vehicle.Color, Is.EqualTo("Blue"));
            Assert.That(vehicle.CurrentMileage, Is.EqualTo(15000m));
            Assert.That(vehicle.OwnerName, Is.EqualTo("John Doe"));
            Assert.That(vehicle.IsActive, Is.True);
        });
    }

    [Test]
    public void Deactivate_ActiveVehicle_SetsIsActiveToFalse()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            IsActive = true
        };

        // Act
        vehicle.Deactivate();

        // Assert
        Assert.That(vehicle.IsActive, Is.False);
    }

    [Test]
    public void Deactivate_AlreadyInactive_RemainsInactive()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            IsActive = false
        };

        // Act
        vehicle.Deactivate();

        // Assert
        Assert.That(vehicle.IsActive, Is.False);
    }

    [Test]
    public void Reactivate_InactiveVehicle_SetsIsActiveToTrue()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            IsActive = false
        };

        // Act
        vehicle.Reactivate();

        // Assert
        Assert.That(vehicle.IsActive, Is.True);
    }

    [Test]
    public void Reactivate_AlreadyActive_RemainsActive()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            IsActive = true
        };

        // Act
        vehicle.Reactivate();

        // Assert
        Assert.That(vehicle.IsActive, Is.True);
    }

    [Test]
    public void UpdateOwner_NewOwnerName_UpdatesOwnerName()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            OwnerName = "John Doe"
        };

        // Act
        vehicle.UpdateOwner("Jane Smith");

        // Assert
        Assert.That(vehicle.OwnerName, Is.EqualTo("Jane Smith"));
    }

    [Test]
    public void UpdateOwner_EmptyString_SetsToEmptyString()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            OwnerName = "John Doe"
        };

        // Act
        vehicle.UpdateOwner(string.Empty);

        // Assert
        Assert.That(vehicle.OwnerName, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Make_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var vehicle = new Vehicle();

        // Assert
        Assert.That(vehicle.Make, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Model_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var vehicle = new Vehicle();

        // Assert
        Assert.That(vehicle.Model, Is.EqualTo(string.Empty));
    }

    [Test]
    public void IsActive_DefaultValue_IsTrue()
    {
        // Arrange & Act
        var vehicle = new Vehicle();

        // Assert
        Assert.That(vehicle.IsActive, Is.True);
    }

    [Test]
    public void InsuranceInfos_DefaultValue_IsEmptyList()
    {
        // Arrange & Act
        var vehicle = new Vehicle();

        // Assert
        Assert.That(vehicle.InsuranceInfos, Is.Not.Null);
        Assert.That(vehicle.InsuranceInfos, Is.Empty);
    }

    [Test]
    public void Policies_DefaultValue_IsEmptyList()
    {
        // Arrange & Act
        var vehicle = new Vehicle();

        // Assert
        Assert.That(vehicle.Policies, Is.Not.Null);
        Assert.That(vehicle.Policies, Is.Empty);
    }

    [Test]
    public void VIN_CanBeNull()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            VIN = null
        };

        // Assert
        Assert.That(vehicle.VIN, Is.Null);
    }

    [Test]
    public void LicensePlate_CanBeNull()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            LicensePlate = null
        };

        // Assert
        Assert.That(vehicle.LicensePlate, Is.Null);
    }

    [Test]
    public void CurrentMileage_CanBeNull()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            CurrentMileage = null
        };

        // Assert
        Assert.That(vehicle.CurrentMileage, Is.Null);
    }

    [Test]
    public void Year_CanBeSetToAnyValue()
    {
        // Arrange & Act
        var vehicle = new Vehicle
        {
            Year = 2023
        };

        // Assert
        Assert.That(vehicle.Year, Is.EqualTo(2023));
    }

    [Test]
    public void InsuranceInfos_CanAddItems()
    {
        // Arrange
        var vehicle = new Vehicle();
        var insurance = new InsuranceInfo
        {
            InsuranceInfoId = Guid.NewGuid(),
            InsuranceCompany = "State Farm"
        };

        // Act
        vehicle.InsuranceInfos.Add(insurance);

        // Assert
        Assert.That(vehicle.InsuranceInfos, Has.Count.EqualTo(1));
        Assert.That(vehicle.InsuranceInfos[0].InsuranceCompany, Is.EqualTo("State Farm"));
    }

    [Test]
    public void Policies_CanAddItems()
    {
        // Arrange
        var vehicle = new Vehicle();
        var policy = new Policy
        {
            PolicyId = Guid.NewGuid(),
            Provider = "AAA"
        };

        // Act
        vehicle.Policies.Add(policy);

        // Assert
        Assert.That(vehicle.Policies, Has.Count.EqualTo(1));
        Assert.That(vehicle.Policies[0].Provider, Is.EqualTo("AAA"));
    }
}
