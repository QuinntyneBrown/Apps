// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core.Tests;

public class ContractorTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesContractor()
    {
        // Arrange
        var contractorId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "ABC Plumbing";
        var specialty = "Plumbing";
        var phoneNumber = "555-1234";
        var email = "contact@abcplumbing.com";
        var website = "www.abcplumbing.com";
        var address = "123 Main St";
        var licenseNumber = "PL-12345";
        var isInsured = true;
        var rating = 5;
        var notes = "Excellent service";
        var isActive = true;

        // Act
        var contractor = new Contractor
        {
            ContractorId = contractorId,
            UserId = userId,
            Name = name,
            Specialty = specialty,
            PhoneNumber = phoneNumber,
            Email = email,
            Website = website,
            Address = address,
            LicenseNumber = licenseNumber,
            IsInsured = isInsured,
            Rating = rating,
            Notes = notes,
            IsActive = isActive
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contractor.ContractorId, Is.EqualTo(contractorId));
            Assert.That(contractor.UserId, Is.EqualTo(userId));
            Assert.That(contractor.Name, Is.EqualTo(name));
            Assert.That(contractor.Specialty, Is.EqualTo(specialty));
            Assert.That(contractor.PhoneNumber, Is.EqualTo(phoneNumber));
            Assert.That(contractor.Email, Is.EqualTo(email));
            Assert.That(contractor.Website, Is.EqualTo(website));
            Assert.That(contractor.Address, Is.EqualTo(address));
            Assert.That(contractor.LicenseNumber, Is.EqualTo(licenseNumber));
            Assert.That(contractor.IsInsured, Is.True);
            Assert.That(contractor.Rating, Is.EqualTo(rating));
            Assert.That(contractor.Notes, Is.EqualTo(notes));
            Assert.That(contractor.IsActive, Is.True);
        });
    }

    [Test]
    public void Contractor_DefaultValues_AreSetCorrectly()
    {
        // Act
        var contractor = new Contractor();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contractor.Name, Is.EqualTo(string.Empty));
            Assert.That(contractor.Specialty, Is.Null);
            Assert.That(contractor.PhoneNumber, Is.Null);
            Assert.That(contractor.Email, Is.Null);
            Assert.That(contractor.Website, Is.Null);
            Assert.That(contractor.Address, Is.Null);
            Assert.That(contractor.LicenseNumber, Is.Null);
            Assert.That(contractor.IsInsured, Is.False);
            Assert.That(contractor.Rating, Is.Null);
            Assert.That(contractor.Notes, Is.Null);
            Assert.That(contractor.IsActive, Is.True);
            Assert.That(contractor.MaintenanceTasks, Is.Not.Null);
            Assert.That(contractor.ServiceLogs, Is.Not.Null);
        });
    }

    [Test]
    public void Contractor_Collections_InitializeAsEmpty()
    {
        // Act
        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Contractor"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contractor.MaintenanceTasks, Is.Empty);
            Assert.That(contractor.ServiceLogs, Is.Empty);
        });
    }

    [Test]
    public void Contractor_IsActive_DefaultsToTrue()
    {
        // Act
        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Active Contractor"
        };

        // Assert
        Assert.That(contractor.IsActive, Is.True);
    }

    [Test]
    public void Contractor_WithRating_IsValid()
    {
        // Arrange
        var ratings = new[] { 1, 2, 3, 4, 5 };

        // Act & Assert
        foreach (var rating in ratings)
        {
            var contractor = new Contractor
            {
                ContractorId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Contractor",
                Rating = rating
            };

            Assert.That(contractor.Rating, Is.EqualTo(rating));
        }
    }

    [Test]
    public void Contractor_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Contractor"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(contractor.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void Contractor_WithoutOptionalFields_IsValid()
    {
        // Arrange & Act
        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Simple Contractor"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contractor.Specialty, Is.Null);
            Assert.That(contractor.PhoneNumber, Is.Null);
            Assert.That(contractor.Email, Is.Null);
            Assert.That(contractor.Website, Is.Null);
            Assert.That(contractor.Address, Is.Null);
            Assert.That(contractor.LicenseNumber, Is.Null);
            Assert.That(contractor.Rating, Is.Null);
            Assert.That(contractor.Notes, Is.Null);
        });
    }

    [Test]
    public void Contractor_AllProperties_CanBeSet()
    {
        // Arrange
        var contractorId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Full Service HVAC";
        var specialty = "HVAC";
        var phoneNumber = "555-9999";
        var email = "info@fullservice.com";
        var website = "www.fullservice.com";
        var address = "456 Oak Ave";
        var licenseNumber = "HVAC-67890";
        var isInsured = true;
        var rating = 4;
        var notes = "Reliable contractor";
        var isActive = true;
        var createdAt = DateTime.UtcNow.AddDays(-90);

        // Act
        var contractor = new Contractor
        {
            ContractorId = contractorId,
            UserId = userId,
            Name = name,
            Specialty = specialty,
            PhoneNumber = phoneNumber,
            Email = email,
            Website = website,
            Address = address,
            LicenseNumber = licenseNumber,
            IsInsured = isInsured,
            Rating = rating,
            Notes = notes,
            IsActive = isActive,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contractor.ContractorId, Is.EqualTo(contractorId));
            Assert.That(contractor.UserId, Is.EqualTo(userId));
            Assert.That(contractor.Name, Is.EqualTo(name));
            Assert.That(contractor.Specialty, Is.EqualTo(specialty));
            Assert.That(contractor.PhoneNumber, Is.EqualTo(phoneNumber));
            Assert.That(contractor.Email, Is.EqualTo(email));
            Assert.That(contractor.Website, Is.EqualTo(website));
            Assert.That(contractor.Address, Is.EqualTo(address));
            Assert.That(contractor.LicenseNumber, Is.EqualTo(licenseNumber));
            Assert.That(contractor.IsInsured, Is.EqualTo(isInsured));
            Assert.That(contractor.Rating, Is.EqualTo(rating));
            Assert.That(contractor.Notes, Is.EqualTo(notes));
            Assert.That(contractor.IsActive, Is.EqualTo(isActive));
            Assert.That(contractor.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
