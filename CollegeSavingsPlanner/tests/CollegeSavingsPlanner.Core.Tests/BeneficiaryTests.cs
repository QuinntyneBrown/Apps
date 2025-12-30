// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core.Tests;

public class BeneficiaryTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var beneficiary = new Beneficiary();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(beneficiary.FirstName, Is.EqualTo(string.Empty));
            Assert.That(beneficiary.LastName, Is.EqualTo(string.Empty));
            Assert.That(beneficiary.Relationship, Is.Null);
            Assert.That(beneficiary.ExpectedCollegeStartYear, Is.Null);
            Assert.That(beneficiary.IsPrimary, Is.False);
            Assert.That(beneficiary.Plan, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var beneficiaryId = Guid.NewGuid();
        var planId = Guid.NewGuid();
        var dateOfBirth = new DateTime(2010, 5, 15);

        // Act
        var beneficiary = new Beneficiary
        {
            BeneficiaryId = beneficiaryId,
            PlanId = planId,
            FirstName = "Sarah",
            LastName = "Johnson",
            DateOfBirth = dateOfBirth,
            Relationship = "Daughter",
            ExpectedCollegeStartYear = 2028,
            IsPrimary = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(beneficiary.BeneficiaryId, Is.EqualTo(beneficiaryId));
            Assert.That(beneficiary.PlanId, Is.EqualTo(planId));
            Assert.That(beneficiary.FirstName, Is.EqualTo("Sarah"));
            Assert.That(beneficiary.LastName, Is.EqualTo("Johnson"));
            Assert.That(beneficiary.DateOfBirth, Is.EqualTo(dateOfBirth));
            Assert.That(beneficiary.Relationship, Is.EqualTo("Daughter"));
            Assert.That(beneficiary.ExpectedCollegeStartYear, Is.EqualTo(2028));
            Assert.That(beneficiary.IsPrimary, Is.True);
        });
    }

    [Test]
    public void CalculateAge_ValidBirthDate_ReturnsCorrectAge()
    {
        // Arrange
        var today = DateTime.Today;
        var birthDate = today.AddYears(-15);
        var beneficiary = new Beneficiary { DateOfBirth = birthDate };

        // Act
        var age = beneficiary.CalculateAge();

        // Assert
        Assert.That(age, Is.EqualTo(15));
    }

    [Test]
    public void CalculateAge_BirthdayNotYetThisYear_ReturnsCorrectAge()
    {
        // Arrange
        var today = DateTime.Today;
        var birthDate = today.AddYears(-15).AddDays(1); // Birthday tomorrow
        var beneficiary = new Beneficiary { DateOfBirth = birthDate };

        // Act
        var age = beneficiary.CalculateAge();

        // Assert
        Assert.That(age, Is.EqualTo(14));
    }

    [Test]
    public void CalculateAge_BirthdayToday_ReturnsCorrectAge()
    {
        // Arrange
        var today = DateTime.Today;
        var birthDate = today.AddYears(-20);
        var beneficiary = new Beneficiary { DateOfBirth = birthDate };

        // Act
        var age = beneficiary.CalculateAge();

        // Assert
        Assert.That(age, Is.EqualTo(20));
    }

    [Test]
    public void CalculateAge_InfantAge_ReturnsZero()
    {
        // Arrange
        var today = DateTime.Today;
        var birthDate = today.AddMonths(-6);
        var beneficiary = new Beneficiary { DateOfBirth = birthDate };

        // Act
        var age = beneficiary.CalculateAge();

        // Assert
        Assert.That(age, Is.EqualTo(0));
    }

    [Test]
    public void CalculateYearsUntilCollege_ValidYear_ReturnsYearsRemaining()
    {
        // Arrange
        var currentYear = DateTime.Now.Year;
        var beneficiary = new Beneficiary
        {
            ExpectedCollegeStartYear = currentYear + 5
        };

        // Act
        var years = beneficiary.CalculateYearsUntilCollege();

        // Assert
        Assert.That(years, Is.EqualTo(5));
    }

    [Test]
    public void CalculateYearsUntilCollege_CurrentYear_ReturnsZero()
    {
        // Arrange
        var currentYear = DateTime.Now.Year;
        var beneficiary = new Beneficiary
        {
            ExpectedCollegeStartYear = currentYear
        };

        // Act
        var years = beneficiary.CalculateYearsUntilCollege();

        // Assert
        Assert.That(years, Is.EqualTo(0));
    }

    [Test]
    public void CalculateYearsUntilCollege_PastYear_ReturnsNegative()
    {
        // Arrange
        var currentYear = DateTime.Now.Year;
        var beneficiary = new Beneficiary
        {
            ExpectedCollegeStartYear = currentYear - 2
        };

        // Act
        var years = beneficiary.CalculateYearsUntilCollege();

        // Assert
        Assert.That(years, Is.EqualTo(-2));
    }

    [Test]
    public void CalculateYearsUntilCollege_NoExpectedYear_ReturnsNull()
    {
        // Arrange
        var beneficiary = new Beneficiary
        {
            ExpectedCollegeStartYear = null
        };

        // Act
        var years = beneficiary.CalculateYearsUntilCollege();

        // Assert
        Assert.That(years, Is.Null);
    }

    [Test]
    public void Plan_CanBeAssigned()
    {
        // Arrange
        var plan = new Plan { PlanId = Guid.NewGuid(), Name = "529 Plan" };
        var beneficiary = new Beneficiary();

        // Act
        beneficiary.Plan = plan;

        // Assert
        Assert.That(beneficiary.Plan, Is.EqualTo(plan));
    }
}
