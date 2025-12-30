// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Core.Tests;

public class ContractorTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesContractor()
    {
        // Arrange
        var contractorId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var name = "John's Construction";
        var trade = "General Contractor";
        var phoneNumber = "555-1234";
        var email = "john@construction.com";
        var rating = 5;

        // Act
        var contractor = new Contractor
        {
            ContractorId = contractorId,
            ProjectId = projectId,
            Name = name,
            Trade = trade,
            PhoneNumber = phoneNumber,
            Email = email,
            Rating = rating
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contractor.ContractorId, Is.EqualTo(contractorId));
            Assert.That(contractor.ProjectId, Is.EqualTo(projectId));
            Assert.That(contractor.Name, Is.EqualTo(name));
            Assert.That(contractor.Trade, Is.EqualTo(trade));
            Assert.That(contractor.PhoneNumber, Is.EqualTo(phoneNumber));
            Assert.That(contractor.Email, Is.EqualTo(email));
            Assert.That(contractor.Rating, Is.EqualTo(rating));
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
            Assert.That(contractor.ProjectId, Is.Null);
            Assert.That(contractor.Trade, Is.Null);
            Assert.That(contractor.PhoneNumber, Is.Null);
            Assert.That(contractor.Email, Is.Null);
            Assert.That(contractor.Rating, Is.Null);
            Assert.That(contractor.Project, Is.Null);
            Assert.That(contractor.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Contractor_WithoutOptionalFields_IsValid()
    {
        // Arrange & Act
        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            Name = "Simple Contractor"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contractor.ProjectId, Is.Null);
            Assert.That(contractor.Trade, Is.Null);
            Assert.That(contractor.PhoneNumber, Is.Null);
            Assert.That(contractor.Email, Is.Null);
            Assert.That(contractor.Rating, Is.Null);
        });
    }

    [Test]
    public void Contractor_WithRating1_IsValid()
    {
        // Arrange & Act
        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            Name = "Contractor",
            Rating = 1
        };

        // Assert
        Assert.That(contractor.Rating, Is.EqualTo(1));
    }

    [Test]
    public void Contractor_WithRating5_IsValid()
    {
        // Arrange & Act
        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            Name = "Excellent Contractor",
            Rating = 5
        };

        // Assert
        Assert.That(contractor.Rating, Is.EqualTo(5));
    }

    [Test]
    public void Contractor_WithVariousTrades_IsValid()
    {
        // Arrange
        var trades = new[] { "Plumber", "Electrician", "Carpenter", "Roofer", "Painter" };

        // Act & Assert
        foreach (var trade in trades)
        {
            var contractor = new Contractor
            {
                ContractorId = Guid.NewGuid(),
                Name = $"{trade} Contractor",
                Trade = trade
            };

            Assert.That(contractor.Trade, Is.EqualTo(trade));
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
            Name = "Test Contractor"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(contractor.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void Contractor_WithEmail_IsValid()
    {
        // Arrange
        var email = "contractor@example.com";

        // Act
        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            Name = "Contractor",
            Email = email
        };

        // Assert
        Assert.That(contractor.Email, Is.EqualTo(email));
    }

    [Test]
    public void Contractor_WithPhoneNumber_IsValid()
    {
        // Arrange
        var phoneNumber = "555-9876";

        // Act
        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            Name = "Contractor",
            PhoneNumber = phoneNumber
        };

        // Assert
        Assert.That(contractor.PhoneNumber, Is.EqualTo(phoneNumber));
    }

    [Test]
    public void Contractor_CanUpdateRating()
    {
        // Arrange
        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            Name = "Contractor",
            Rating = 3
        };
        var newRating = 5;

        // Act
        contractor.Rating = newRating;

        // Assert
        Assert.That(contractor.Rating, Is.EqualTo(newRating));
    }

    [Test]
    public void Contractor_AllProperties_CanBeSet()
    {
        // Arrange
        var contractorId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var name = "Complete Contractor";
        var trade = "Master Builder";
        var phoneNumber = "555-5555";
        var email = "complete@contractor.com";
        var rating = 4;
        var createdAt = DateTime.UtcNow.AddDays(-20);

        // Act
        var contractor = new Contractor
        {
            ContractorId = contractorId,
            ProjectId = projectId,
            Name = name,
            Trade = trade,
            PhoneNumber = phoneNumber,
            Email = email,
            Rating = rating,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contractor.ContractorId, Is.EqualTo(contractorId));
            Assert.That(contractor.ProjectId, Is.EqualTo(projectId));
            Assert.That(contractor.Name, Is.EqualTo(name));
            Assert.That(contractor.Trade, Is.EqualTo(trade));
            Assert.That(contractor.PhoneNumber, Is.EqualTo(phoneNumber));
            Assert.That(contractor.Email, Is.EqualTo(email));
            Assert.That(contractor.Rating, Is.EqualTo(rating));
            Assert.That(contractor.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
