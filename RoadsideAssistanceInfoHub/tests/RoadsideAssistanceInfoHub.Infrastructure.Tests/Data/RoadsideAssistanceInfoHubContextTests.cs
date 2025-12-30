// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RoadsideAssistanceInfoHub.Infrastructure.Tests;

/// <summary>
/// Unit tests for the RoadsideAssistanceInfoHubContext.
/// </summary>
[TestFixture]
public class RoadsideAssistanceInfoHubContextTests
{
    private RoadsideAssistanceInfoHubContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RoadsideAssistanceInfoHubContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new RoadsideAssistanceInfoHubContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Vehicles can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Vehicles_CanAddAndRetrieve()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Toyota",
            Model = "Camry",
            Year = 2020,
            VIN = "1HGBH41JXMN109186",
            LicensePlate = "ABC123",
            Color = "Silver",
            IsActive = true,
        };

        // Act
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Vehicles.FindAsync(vehicle.VehicleId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Make, Is.EqualTo("Toyota"));
        Assert.That(retrieved.Model, Is.EqualTo("Camry"));
        Assert.That(retrieved.Year, Is.EqualTo(2020));
    }

    /// <summary>
    /// Tests that InsuranceInfos can be added and retrieved.
    /// </summary>
    [Test]
    public async Task InsuranceInfos_CanAddAndRetrieve()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Honda",
            Model = "Civic",
            Year = 2021,
            IsActive = true,
        };

        var insuranceInfo = new InsuranceInfo
        {
            InsuranceInfoId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            InsuranceCompany = "State Farm",
            PolicyNumber = "SF123456",
            PolicyHolder = "John Doe",
            PolicyStartDate = DateTime.UtcNow,
            PolicyEndDate = DateTime.UtcNow.AddYears(1),
            IncludesRoadsideAssistance = true,
        };

        // Act
        _context.Vehicles.Add(vehicle);
        _context.InsuranceInfos.Add(insuranceInfo);
        await _context.SaveChangesAsync();

        var retrieved = await _context.InsuranceInfos.FindAsync(insuranceInfo.InsuranceInfoId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.InsuranceCompany, Is.EqualTo("State Farm"));
        Assert.That(retrieved.PolicyNumber, Is.EqualTo("SF123456"));
        Assert.That(retrieved.IncludesRoadsideAssistance, Is.True);
    }

    /// <summary>
    /// Tests that EmergencyContacts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task EmergencyContacts_CanAddAndRetrieve()
    {
        // Arrange
        var contact = new EmergencyContact
        {
            EmergencyContactId = Guid.NewGuid(),
            Name = "AAA Roadside",
            PhoneNumber = "(800) 222-4357",
            ContactType = "Tow Service",
            IsPrimaryContact = true,
            IsActive = true,
        };

        // Act
        _context.EmergencyContacts.Add(contact);
        await _context.SaveChangesAsync();

        var retrieved = await _context.EmergencyContacts.FindAsync(contact.EmergencyContactId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("AAA Roadside"));
        Assert.That(retrieved.PhoneNumber, Is.EqualTo("(800) 222-4357"));
        Assert.That(retrieved.IsPrimaryContact, Is.True);
    }

    /// <summary>
    /// Tests that Policies can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Policies_CanAddAndRetrieve()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Ford",
            Model = "F-150",
            Year = 2022,
            IsActive = true,
        };

        var policy = new Policy
        {
            PolicyId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            Provider = "AAA",
            PolicyNumber = "AAA123456",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(1),
            EmergencyPhone = "(800) 222-4357",
            CoversBatteryService = true,
            CoversFlatTire = true,
        };

        // Act
        _context.Vehicles.Add(vehicle);
        _context.Policies.Add(policy);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Policies.FindAsync(policy.PolicyId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Provider, Is.EqualTo("AAA"));
        Assert.That(retrieved.PolicyNumber, Is.EqualTo("AAA123456"));
        Assert.That(retrieved.CoversBatteryService, Is.True);
    }

    /// <summary>
    /// Tests that cascade delete works for insurance info when vehicle is deleted.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesInsuranceInfo()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = "Chevrolet",
            Model = "Silverado",
            Year = 2020,
            IsActive = true,
        };

        var insuranceInfo = new InsuranceInfo
        {
            InsuranceInfoId = Guid.NewGuid(),
            VehicleId = vehicle.VehicleId,
            InsuranceCompany = "Geico",
            PolicyNumber = "GC123456",
            PolicyHolder = "Jane Doe",
            PolicyStartDate = DateTime.UtcNow,
            PolicyEndDate = DateTime.UtcNow.AddYears(1),
        };

        _context.Vehicles.Add(vehicle);
        _context.InsuranceInfos.Add(insuranceInfo);
        await _context.SaveChangesAsync();

        // Act
        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        var retrievedInsurance = await _context.InsuranceInfos.FindAsync(insuranceInfo.InsuranceInfoId);

        // Assert
        Assert.That(retrievedInsurance, Is.Null);
    }
}
