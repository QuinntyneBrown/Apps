// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CharitableGivingTracker.Core.Tests;

public class OrganizationTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var organization = new Organization
        {
            OrganizationId = Guid.NewGuid(),
            Name = "American Red Cross",
            EIN = "53-0196605",
            Is501c3 = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(organization.OrganizationId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(organization.Name, Is.EqualTo("American Red Cross"));
            Assert.That(organization.EIN, Is.EqualTo("53-0196605"));
            Assert.That(organization.Is501c3, Is.True);
            Assert.That(organization.Donations, Is.Not.Null);
        });
    }

    [Test]
    public void Organization_WithAddress_StoresAddressCorrectly()
    {
        // Arrange & Act
        var organization = new Organization
        {
            Address = "123 Charity Lane, Nonprofit City, ST 12345"
        };

        // Assert
        Assert.That(organization.Address, Is.EqualTo("123 Charity Lane, Nonprofit City, ST 12345"));
    }

    [Test]
    public void Organization_WithNullAddress_AllowsNull()
    {
        // Arrange & Act
        var organization = new Organization
        {
            Address = null
        };

        // Assert
        Assert.That(organization.Address, Is.Null);
    }

    [Test]
    public void Organization_WithWebsite_StoresWebsiteCorrectly()
    {
        // Arrange & Act
        var organization = new Organization
        {
            Website = "https://www.redcross.org"
        };

        // Assert
        Assert.That(organization.Website, Is.EqualTo("https://www.redcross.org"));
    }

    [Test]
    public void Organization_WithNullWebsite_AllowsNull()
    {
        // Arrange & Act
        var organization = new Organization
        {
            Website = null
        };

        // Assert
        Assert.That(organization.Website, Is.Null);
    }

    [Test]
    public void Organization_WithNullEIN_AllowsNull()
    {
        // Arrange & Act
        var organization = new Organization
        {
            EIN = null
        };

        // Assert
        Assert.That(organization.EIN, Is.Null);
    }

    [Test]
    public void Organization_WithNotes_StoresNotesCorrectly()
    {
        // Arrange & Act
        var organization = new Organization
        {
            Notes = "Primary charity for annual giving"
        };

        // Assert
        Assert.That(organization.Notes, Is.EqualTo("Primary charity for annual giving"));
    }

    [Test]
    public void Organization_NotIs501c3_Is501c3IsFalse()
    {
        // Arrange & Act
        var organization = new Organization
        {
            Is501c3 = false
        };

        // Assert
        Assert.That(organization.Is501c3, Is.False);
    }

    [Test]
    public void Organization_DefaultIs501c3_IsTrue()
    {
        // Arrange & Act
        var organization = new Organization();

        // Assert
        Assert.That(organization.Is501c3, Is.True);
    }

    [Test]
    public void CalculateTotalDonations_WithNoDonations_ReturnsZero()
    {
        // Arrange
        var organization = new Organization();

        // Act
        var total = organization.CalculateTotalDonations();

        // Assert
        Assert.That(total, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateTotalDonations_WithSingleDonation_ReturnsAmount()
    {
        // Arrange
        var organization = new Organization();
        organization.Donations.Add(new Donation { Amount = 100m });

        // Act
        var total = organization.CalculateTotalDonations();

        // Assert
        Assert.That(total, Is.EqualTo(100m));
    }

    [Test]
    public void CalculateTotalDonations_WithMultipleDonations_ReturnsSum()
    {
        // Arrange
        var organization = new Organization();
        organization.Donations.Add(new Donation { Amount = 100m });
        organization.Donations.Add(new Donation { Amount = 250m });
        organization.Donations.Add(new Donation { Amount = 50m });

        // Act
        var total = organization.CalculateTotalDonations();

        // Assert
        Assert.That(total, Is.EqualTo(400m));
    }

    [Test]
    public void CalculateTotalDonations_WithLargeAmounts_CalculatesCorrectly()
    {
        // Arrange
        var organization = new Organization();
        organization.Donations.Add(new Donation { Amount = 5000m });
        organization.Donations.Add(new Donation { Amount = 10000m });

        // Act
        var total = organization.CalculateTotalDonations();

        // Assert
        Assert.That(total, Is.EqualTo(15000m));
    }

    [Test]
    public void Organization_CanAddMultipleDonations()
    {
        // Arrange
        var organization = new Organization();
        var donation1 = new Donation { DonationId = Guid.NewGuid(), Amount = 100m };
        var donation2 = new Donation { DonationId = Guid.NewGuid(), Amount = 200m };
        var donation3 = new Donation { DonationId = Guid.NewGuid(), Amount = 300m };

        // Act
        organization.Donations.Add(donation1);
        organization.Donations.Add(donation2);
        organization.Donations.Add(donation3);

        // Assert
        Assert.That(organization.Donations, Has.Count.EqualTo(3));
    }
}
