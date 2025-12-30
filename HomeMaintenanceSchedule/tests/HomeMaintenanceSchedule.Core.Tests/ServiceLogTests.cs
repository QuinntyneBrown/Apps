// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core.Tests;

public class ServiceLogTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesServiceLog()
    {
        // Arrange
        var serviceLogId = Guid.NewGuid();
        var maintenanceTaskId = Guid.NewGuid();
        var serviceDate = DateTime.UtcNow;
        var description = "Replaced air filter and cleaned coils";
        var contractorId = Guid.NewGuid();
        var cost = 125.50m;
        var notes = "System running smoothly";
        var partsUsed = "Air filter, cleaning solution";
        var laborHours = 2.5m;
        var warrantyExpiresAt = DateTime.UtcNow.AddYears(1);

        // Act
        var serviceLog = new ServiceLog
        {
            ServiceLogId = serviceLogId,
            MaintenanceTaskId = maintenanceTaskId,
            ServiceDate = serviceDate,
            Description = description,
            ContractorId = contractorId,
            Cost = cost,
            Notes = notes,
            PartsUsed = partsUsed,
            LaborHours = laborHours,
            WarrantyExpiresAt = warrantyExpiresAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(serviceLog.ServiceLogId, Is.EqualTo(serviceLogId));
            Assert.That(serviceLog.MaintenanceTaskId, Is.EqualTo(maintenanceTaskId));
            Assert.That(serviceLog.ServiceDate, Is.EqualTo(serviceDate));
            Assert.That(serviceLog.Description, Is.EqualTo(description));
            Assert.That(serviceLog.ContractorId, Is.EqualTo(contractorId));
            Assert.That(serviceLog.Cost, Is.EqualTo(cost));
            Assert.That(serviceLog.Notes, Is.EqualTo(notes));
            Assert.That(serviceLog.PartsUsed, Is.EqualTo(partsUsed));
            Assert.That(serviceLog.LaborHours, Is.EqualTo(laborHours));
            Assert.That(serviceLog.WarrantyExpiresAt, Is.EqualTo(warrantyExpiresAt));
        });
    }

    [Test]
    public void ServiceLog_DefaultValues_AreSetCorrectly()
    {
        // Act
        var serviceLog = new ServiceLog();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(serviceLog.ServiceDate, Is.EqualTo(default(DateTime)));
            Assert.That(serviceLog.Description, Is.EqualTo(string.Empty));
            Assert.That(serviceLog.ContractorId, Is.Null);
            Assert.That(serviceLog.Contractor, Is.Null);
            Assert.That(serviceLog.Cost, Is.Null);
            Assert.That(serviceLog.Notes, Is.Null);
            Assert.That(serviceLog.PartsUsed, Is.Null);
            Assert.That(serviceLog.LaborHours, Is.Null);
            Assert.That(serviceLog.WarrantyExpiresAt, Is.Null);
            Assert.That(serviceLog.MaintenanceTask, Is.Null);
            Assert.That(serviceLog.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ServiceLog_WithoutOptionalFields_IsValid()
    {
        // Arrange & Act
        var serviceLog = new ServiceLog
        {
            ServiceLogId = Guid.NewGuid(),
            MaintenanceTaskId = Guid.NewGuid(),
            ServiceDate = DateTime.UtcNow,
            Description = "Basic service"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(serviceLog.ContractorId, Is.Null);
            Assert.That(serviceLog.Cost, Is.Null);
            Assert.That(serviceLog.Notes, Is.Null);
            Assert.That(serviceLog.PartsUsed, Is.Null);
            Assert.That(serviceLog.LaborHours, Is.Null);
            Assert.That(serviceLog.WarrantyExpiresAt, Is.Null);
        });
    }

    [Test]
    public void ServiceLog_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var serviceLog = new ServiceLog
        {
            ServiceLogId = Guid.NewGuid(),
            MaintenanceTaskId = Guid.NewGuid(),
            ServiceDate = DateTime.UtcNow,
            Description = "Test service"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(serviceLog.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void ServiceLog_WithCost_IsValid()
    {
        // Arrange
        var cost = 250.75m;

        // Act
        var serviceLog = new ServiceLog
        {
            ServiceLogId = Guid.NewGuid(),
            MaintenanceTaskId = Guid.NewGuid(),
            ServiceDate = DateTime.UtcNow,
            Description = "Service with cost",
            Cost = cost
        };

        // Assert
        Assert.That(serviceLog.Cost, Is.EqualTo(cost));
    }

    [Test]
    public void ServiceLog_WithLaborHours_IsValid()
    {
        // Arrange
        var laborHours = 4.5m;

        // Act
        var serviceLog = new ServiceLog
        {
            ServiceLogId = Guid.NewGuid(),
            MaintenanceTaskId = Guid.NewGuid(),
            ServiceDate = DateTime.UtcNow,
            Description = "Service with labor",
            LaborHours = laborHours
        };

        // Assert
        Assert.That(serviceLog.LaborHours, Is.EqualTo(laborHours));
    }

    [Test]
    public void ServiceLog_WithWarranty_IsValid()
    {
        // Arrange
        var warrantyDate = DateTime.UtcNow.AddMonths(6);

        // Act
        var serviceLog = new ServiceLog
        {
            ServiceLogId = Guid.NewGuid(),
            MaintenanceTaskId = Guid.NewGuid(),
            ServiceDate = DateTime.UtcNow,
            Description = "Service with warranty",
            WarrantyExpiresAt = warrantyDate
        };

        // Assert
        Assert.That(serviceLog.WarrantyExpiresAt, Is.EqualTo(warrantyDate));
    }

    [Test]
    public void ServiceLog_AllProperties_CanBeSet()
    {
        // Arrange
        var serviceLogId = Guid.NewGuid();
        var maintenanceTaskId = Guid.NewGuid();
        var serviceDate = new DateTime(2024, 3, 15);
        var description = "Complete service log";
        var contractorId = Guid.NewGuid();
        var cost = 350m;
        var notes = "Detailed notes";
        var partsUsed = "Multiple parts";
        var laborHours = 5m;
        var warrantyExpiresAt = new DateTime(2025, 3, 15);
        var createdAt = DateTime.UtcNow.AddDays(-10);

        // Act
        var serviceLog = new ServiceLog
        {
            ServiceLogId = serviceLogId,
            MaintenanceTaskId = maintenanceTaskId,
            ServiceDate = serviceDate,
            Description = description,
            ContractorId = contractorId,
            Cost = cost,
            Notes = notes,
            PartsUsed = partsUsed,
            LaborHours = laborHours,
            WarrantyExpiresAt = warrantyExpiresAt,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(serviceLog.ServiceLogId, Is.EqualTo(serviceLogId));
            Assert.That(serviceLog.MaintenanceTaskId, Is.EqualTo(maintenanceTaskId));
            Assert.That(serviceLog.ServiceDate, Is.EqualTo(serviceDate));
            Assert.That(serviceLog.Description, Is.EqualTo(description));
            Assert.That(serviceLog.ContractorId, Is.EqualTo(contractorId));
            Assert.That(serviceLog.Cost, Is.EqualTo(cost));
            Assert.That(serviceLog.Notes, Is.EqualTo(notes));
            Assert.That(serviceLog.PartsUsed, Is.EqualTo(partsUsed));
            Assert.That(serviceLog.LaborHours, Is.EqualTo(laborHours));
            Assert.That(serviceLog.WarrantyExpiresAt, Is.EqualTo(warrantyExpiresAt));
            Assert.That(serviceLog.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
