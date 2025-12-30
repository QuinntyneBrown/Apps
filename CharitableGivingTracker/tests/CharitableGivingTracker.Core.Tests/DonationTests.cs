// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CharitableGivingTracker.Core.Tests;

public class DonationTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange
        var donationDate = new DateTime(2024, 3, 15);

        // Act
        var donation = new Donation
        {
            DonationId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid(),
            Amount = 100.00m,
            DonationDate = donationDate,
            DonationType = DonationType.Cash,
            IsTaxDeductible = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(donation.DonationId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(donation.OrganizationId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(donation.Amount, Is.EqualTo(100.00m));
            Assert.That(donation.DonationDate, Is.EqualTo(donationDate));
            Assert.That(donation.DonationType, Is.EqualTo(DonationType.Cash));
            Assert.That(donation.IsTaxDeductible, Is.True);
        });
    }

    [Test]
    public void Donation_WithReceiptNumber_StoresReceiptCorrectly()
    {
        // Arrange & Act
        var donation = new Donation
        {
            ReceiptNumber = "REC-2024-001"
        };

        // Assert
        Assert.That(donation.ReceiptNumber, Is.EqualTo("REC-2024-001"));
    }

    [Test]
    public void Donation_WithNullReceiptNumber_AllowsNull()
    {
        // Arrange & Act
        var donation = new Donation
        {
            ReceiptNumber = null
        };

        // Assert
        Assert.That(donation.ReceiptNumber, Is.Null);
    }

    [Test]
    public void Donation_WithNotes_StoresNotesCorrectly()
    {
        // Arrange & Act
        var donation = new Donation
        {
            Notes = "Annual donation for 2024"
        };

        // Assert
        Assert.That(donation.Notes, Is.EqualTo("Annual donation for 2024"));
    }

    [Test]
    public void Donation_WithNullNotes_AllowsNull()
    {
        // Arrange & Act
        var donation = new Donation
        {
            Notes = null
        };

        // Assert
        Assert.That(donation.Notes, Is.Null);
    }

    [Test]
    public void Donation_AllDonationTypesCanBeAssigned()
    {
        // Arrange & Act & Assert
        var types = Enum.GetValues<DonationType>();
        foreach (var type in types)
        {
            var donation = new Donation { DonationType = type };
            Assert.That(donation.DonationType, Is.EqualTo(type));
        }
    }

    [Test]
    public void Donation_NotTaxDeductible_IsTaxDeductibleIsFalse()
    {
        // Arrange & Act
        var donation = new Donation
        {
            IsTaxDeductible = false
        };

        // Assert
        Assert.That(donation.IsTaxDeductible, Is.False);
    }

    [Test]
    public void Donation_DefaultIsTaxDeductible_IsTrue()
    {
        // Arrange & Act
        var donation = new Donation();

        // Assert
        Assert.That(donation.IsTaxDeductible, Is.True);
    }

    [Test]
    public void Donation_WithOrganization_AssociatesOrganizationCorrectly()
    {
        // Arrange
        var organization = new Organization
        {
            OrganizationId = Guid.NewGuid(),
            Name = "Red Cross"
        };

        // Act
        var donation = new Donation
        {
            OrganizationId = organization.OrganizationId,
            Organization = organization
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(donation.Organization, Is.Not.Null);
            Assert.That(donation.Organization.OrganizationId, Is.EqualTo(organization.OrganizationId));
            Assert.That(donation.Organization.Name, Is.EqualTo("Red Cross"));
        });
    }

    [Test]
    public void Donation_WithNullOrganization_AllowsNull()
    {
        // Arrange & Act
        var donation = new Donation
        {
            OrganizationId = Guid.NewGuid(),
            Organization = null
        };

        // Assert
        Assert.That(donation.Organization, Is.Null);
    }

    [Test]
    public void Donation_WithLargeAmount_StoresAmountCorrectly()
    {
        // Arrange & Act
        var donation = new Donation
        {
            Amount = 10000.00m
        };

        // Assert
        Assert.That(donation.Amount, Is.EqualTo(10000.00m));
    }

    [Test]
    public void Donation_WithSmallAmount_StoresAmountCorrectly()
    {
        // Arrange & Act
        var donation = new Donation
        {
            Amount = 5.00m
        };

        // Assert
        Assert.That(donation.Amount, Is.EqualTo(5.00m));
    }
}
