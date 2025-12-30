// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core.Tests;

public class LeaseTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesLease()
    {
        // Arrange & Act
        var lease = new Lease();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(lease.LeaseId, Is.EqualTo(Guid.Empty));
            Assert.That(lease.PropertyId, Is.EqualTo(Guid.Empty));
            Assert.That(lease.TenantName, Is.EqualTo(string.Empty));
            Assert.That(lease.MonthlyRent, Is.EqualTo(0m));
            Assert.That(lease.StartDate, Is.EqualTo(default(DateTime)));
            Assert.That(lease.EndDate, Is.EqualTo(default(DateTime)));
            Assert.That(lease.SecurityDeposit, Is.EqualTo(0m));
            Assert.That(lease.IsActive, Is.True);
            Assert.That(lease.Notes, Is.Null);
            Assert.That(lease.Property, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesLease()
    {
        // Arrange
        var leaseId = Guid.NewGuid();
        var propertyId = Guid.NewGuid();
        var tenantName = "John Doe";
        var monthlyRent = 1500m;
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 12, 31);
        var securityDeposit = 3000m;

        // Act
        var lease = new Lease
        {
            LeaseId = leaseId,
            PropertyId = propertyId,
            TenantName = tenantName,
            MonthlyRent = monthlyRent,
            StartDate = startDate,
            EndDate = endDate,
            SecurityDeposit = securityDeposit
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(lease.LeaseId, Is.EqualTo(leaseId));
            Assert.That(lease.PropertyId, Is.EqualTo(propertyId));
            Assert.That(lease.TenantName, Is.EqualTo(tenantName));
            Assert.That(lease.MonthlyRent, Is.EqualTo(monthlyRent));
            Assert.That(lease.StartDate, Is.EqualTo(startDate));
            Assert.That(lease.EndDate, Is.EqualTo(endDate));
            Assert.That(lease.SecurityDeposit, Is.EqualTo(securityDeposit));
            Assert.That(lease.IsActive, Is.True);
        });
    }

    [Test]
    public void Terminate_ActiveLease_SetsIsActiveToFalse()
    {
        // Arrange
        var lease = new Lease
        {
            TenantName = "Jane Smith",
            MonthlyRent = 1200m,
            IsActive = true
        };

        // Act
        lease.Terminate();

        // Assert
        Assert.That(lease.IsActive, Is.False);
    }

    [Test]
    public void Terminate_AlreadyInactiveLease_RemainsInactive()
    {
        // Arrange
        var lease = new Lease
        {
            TenantName = "Bob Johnson",
            MonthlyRent = 1800m,
            IsActive = false
        };

        // Act
        lease.Terminate();

        // Assert
        Assert.That(lease.IsActive, Is.False);
    }

    [Test]
    public void Lease_WithNotes_SetsCorrectly()
    {
        // Arrange
        var notes = "Tenant has been reliable and timely with payments";

        // Act
        var lease = new Lease
        {
            TenantName = "Alice Brown",
            MonthlyRent = 2000m,
            Notes = notes
        };

        // Assert
        Assert.That(lease.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void Lease_HighMonthlyRent_StoresCorrectly()
    {
        // Arrange & Act
        var lease = new Lease
        {
            TenantName = "Corporate Tenant",
            MonthlyRent = 5000m
        };

        // Assert
        Assert.That(lease.MonthlyRent, Is.EqualTo(5000m));
    }

    [Test]
    public void Lease_LowSecurityDeposit_StoresCorrectly()
    {
        // Arrange & Act
        var lease = new Lease
        {
            TenantName = "Student Tenant",
            MonthlyRent = 800m,
            SecurityDeposit = 800m
        };

        // Assert
        Assert.That(lease.SecurityDeposit, Is.EqualTo(800m));
    }

    [Test]
    public void Lease_LongTermLease_StoresCorrectly()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2027, 12, 31);

        // Act
        var lease = new Lease
        {
            TenantName = "Long Term Tenant",
            MonthlyRent = 1500m,
            StartDate = startDate,
            EndDate = endDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(lease.StartDate, Is.EqualTo(startDate));
            Assert.That(lease.EndDate, Is.EqualTo(endDate));
            Assert.That((lease.EndDate - lease.StartDate).Days, Is.GreaterThan(1000));
        });
    }

    [Test]
    public void Lease_ShortTermLease_StoresCorrectly()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 6, 30);

        // Act
        var lease = new Lease
        {
            TenantName = "Short Term Tenant",
            MonthlyRent = 2000m,
            StartDate = startDate,
            EndDate = endDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(lease.StartDate, Is.EqualTo(startDate));
            Assert.That(lease.EndDate, Is.EqualTo(endDate));
        });
    }

    [Test]
    public void Lease_WithAllProperties_SetsAllCorrectly()
    {
        // Arrange & Act
        var lease = new Lease
        {
            LeaseId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            TenantName = "Complete Tenant",
            MonthlyRent = 1750m,
            StartDate = new DateTime(2024, 3, 1),
            EndDate = new DateTime(2025, 2, 28),
            SecurityDeposit = 3500m,
            IsActive = true,
            Notes = "All details provided"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(lease.TenantName, Is.EqualTo("Complete Tenant"));
            Assert.That(lease.MonthlyRent, Is.EqualTo(1750m));
            Assert.That(lease.SecurityDeposit, Is.EqualTo(3500m));
            Assert.That(lease.IsActive, Is.True);
            Assert.That(lease.Notes, Is.EqualTo("All details provided"));
        });
    }
}
