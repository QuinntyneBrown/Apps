using RoadsideAssistanceInfoHub.Api.Features.Vehicles;
using RoadsideAssistanceInfoHub.Api.Features.InsuranceInfos;
using RoadsideAssistanceInfoHub.Api.Features.EmergencyContacts;
using RoadsideAssistanceInfoHub.Api.Features.Policies;

namespace RoadsideAssistanceInfoHub.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void VehicleDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var vehicle = new Core.Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Toyota",
            Model = "Camry",
            Year = 2022,
            VIN = "1HGBH41JXMN109186",
            LicensePlate = "ABC123",
            Color = "Silver",
            CurrentMileage = 15000,
            OwnerName = "John Doe",
            Notes = "Test notes",
            IsActive = true,
        };

        // Act
        var dto = vehicle.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.VehicleId, Is.EqualTo(vehicle.VehicleId));
            Assert.That(dto.Make, Is.EqualTo(vehicle.Make));
            Assert.That(dto.Model, Is.EqualTo(vehicle.Model));
            Assert.That(dto.Year, Is.EqualTo(vehicle.Year));
            Assert.That(dto.VIN, Is.EqualTo(vehicle.VIN));
            Assert.That(dto.LicensePlate, Is.EqualTo(vehicle.LicensePlate));
            Assert.That(dto.Color, Is.EqualTo(vehicle.Color));
            Assert.That(dto.CurrentMileage, Is.EqualTo(vehicle.CurrentMileage));
            Assert.That(dto.OwnerName, Is.EqualTo(vehicle.OwnerName));
            Assert.That(dto.Notes, Is.EqualTo(vehicle.Notes));
            Assert.That(dto.IsActive, Is.EqualTo(vehicle.IsActive));
        });
    }

    [Test]
    public void InsuranceInfoDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var insuranceInfo = new Core.InsuranceInfo
        {
            InsuranceInfoId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            InsuranceCompany = "State Farm",
            PolicyNumber = "POL123456",
            PolicyHolder = "Jane Smith",
            PolicyStartDate = DateTime.UtcNow.AddMonths(-6),
            PolicyEndDate = DateTime.UtcNow.AddMonths(6),
            AgentName = "Bob Agent",
            AgentPhone = "555-1234",
            CompanyPhone = "555-5678",
            ClaimsPhone = "555-9999",
            CoverageType = "Full Coverage",
            Deductible = 500,
            Premium = 1200,
            IncludesRoadsideAssistance = true,
            Notes = "Premium policy",
        };

        // Act
        var dto = insuranceInfo.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.InsuranceInfoId, Is.EqualTo(insuranceInfo.InsuranceInfoId));
            Assert.That(dto.VehicleId, Is.EqualTo(insuranceInfo.VehicleId));
            Assert.That(dto.InsuranceCompany, Is.EqualTo(insuranceInfo.InsuranceCompany));
            Assert.That(dto.PolicyNumber, Is.EqualTo(insuranceInfo.PolicyNumber));
            Assert.That(dto.PolicyHolder, Is.EqualTo(insuranceInfo.PolicyHolder));
            Assert.That(dto.PolicyStartDate, Is.EqualTo(insuranceInfo.PolicyStartDate));
            Assert.That(dto.PolicyEndDate, Is.EqualTo(insuranceInfo.PolicyEndDate));
            Assert.That(dto.AgentName, Is.EqualTo(insuranceInfo.AgentName));
            Assert.That(dto.AgentPhone, Is.EqualTo(insuranceInfo.AgentPhone));
            Assert.That(dto.CompanyPhone, Is.EqualTo(insuranceInfo.CompanyPhone));
            Assert.That(dto.ClaimsPhone, Is.EqualTo(insuranceInfo.ClaimsPhone));
            Assert.That(dto.CoverageType, Is.EqualTo(insuranceInfo.CoverageType));
            Assert.That(dto.Deductible, Is.EqualTo(insuranceInfo.Deductible));
            Assert.That(dto.Premium, Is.EqualTo(insuranceInfo.Premium));
            Assert.That(dto.IncludesRoadsideAssistance, Is.EqualTo(insuranceInfo.IncludesRoadsideAssistance));
            Assert.That(dto.Notes, Is.EqualTo(insuranceInfo.Notes));
        });
    }

    [Test]
    public void EmergencyContactDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var contact = new Core.EmergencyContact
        {
            EmergencyContactId = Guid.NewGuid(),
            Name = "Emergency Tow Service",
            Relationship = "Service Provider",
            PhoneNumber = "555-TOWING",
            AlternatePhone = "555-TOW2",
            Email = "tow@example.com",
            Address = "123 Main St",
            IsPrimaryContact = true,
            ContactType = "Tow Service",
            ServiceArea = "Metro Area",
            Notes = "24/7 service",
            IsActive = true,
        };

        // Act
        var dto = contact.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.EmergencyContactId, Is.EqualTo(contact.EmergencyContactId));
            Assert.That(dto.Name, Is.EqualTo(contact.Name));
            Assert.That(dto.Relationship, Is.EqualTo(contact.Relationship));
            Assert.That(dto.PhoneNumber, Is.EqualTo(contact.PhoneNumber));
            Assert.That(dto.AlternatePhone, Is.EqualTo(contact.AlternatePhone));
            Assert.That(dto.Email, Is.EqualTo(contact.Email));
            Assert.That(dto.Address, Is.EqualTo(contact.Address));
            Assert.That(dto.IsPrimaryContact, Is.EqualTo(contact.IsPrimaryContact));
            Assert.That(dto.ContactType, Is.EqualTo(contact.ContactType));
            Assert.That(dto.ServiceArea, Is.EqualTo(contact.ServiceArea));
            Assert.That(dto.Notes, Is.EqualTo(contact.Notes));
            Assert.That(dto.IsActive, Is.EqualTo(contact.IsActive));
        });
    }

    [Test]
    public void PolicyDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var policy = new Core.Policy
        {
            PolicyId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            Provider = "AAA",
            PolicyNumber = "AAA-123456",
            StartDate = DateTime.UtcNow.AddMonths(-3),
            EndDate = DateTime.UtcNow.AddMonths(9),
            EmergencyPhone = "1-800-AAA-HELP",
            MaxTowingDistance = 100,
            ServiceCallsPerYear = 4,
            CoveredServices = new List<string> { "Towing", "Battery", "Fuel Delivery" },
            AnnualPremium = 89.99m,
            CoversBatteryService = true,
            CoversFlatTire = true,
            CoversFuelDelivery = true,
            CoversLockout = true,
            Notes = "Premium plus coverage",
        };

        // Act
        var dto = policy.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.PolicyId, Is.EqualTo(policy.PolicyId));
            Assert.That(dto.VehicleId, Is.EqualTo(policy.VehicleId));
            Assert.That(dto.Provider, Is.EqualTo(policy.Provider));
            Assert.That(dto.PolicyNumber, Is.EqualTo(policy.PolicyNumber));
            Assert.That(dto.StartDate, Is.EqualTo(policy.StartDate));
            Assert.That(dto.EndDate, Is.EqualTo(policy.EndDate));
            Assert.That(dto.EmergencyPhone, Is.EqualTo(policy.EmergencyPhone));
            Assert.That(dto.MaxTowingDistance, Is.EqualTo(policy.MaxTowingDistance));
            Assert.That(dto.ServiceCallsPerYear, Is.EqualTo(policy.ServiceCallsPerYear));
            Assert.That(dto.CoveredServices, Is.EqualTo(policy.CoveredServices));
            Assert.That(dto.AnnualPremium, Is.EqualTo(policy.AnnualPremium));
            Assert.That(dto.CoversBatteryService, Is.EqualTo(policy.CoversBatteryService));
            Assert.That(dto.CoversFlatTire, Is.EqualTo(policy.CoversFlatTire));
            Assert.That(dto.CoversFuelDelivery, Is.EqualTo(policy.CoversFuelDelivery));
            Assert.That(dto.CoversLockout, Is.EqualTo(policy.CoversLockout));
            Assert.That(dto.Notes, Is.EqualTo(policy.Notes));
        });
    }
}
