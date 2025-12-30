// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core.Tests;

public class EventTests
{
    [Test]
    public void PlanCreatedEvent_Properties_CanBeSet()
    {
        // Arrange
        var planId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PlanCreatedEvent
        {
            PlanId = planId,
            Name = "My 529 Plan",
            State = "California",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PlanId, Is.EqualTo(planId));
            Assert.That(evt.Name, Is.EqualTo("My 529 Plan"));
            Assert.That(evt.State, Is.EqualTo("California"));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void BeneficiaryAddedEvent_Properties_CanBeSet()
    {
        // Arrange
        var beneficiaryId = Guid.NewGuid();
        var planId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new BeneficiaryAddedEvent
        {
            BeneficiaryId = beneficiaryId,
            PlanId = planId,
            FirstName = "Sarah",
            LastName = "Johnson",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BeneficiaryId, Is.EqualTo(beneficiaryId));
            Assert.That(evt.PlanId, Is.EqualTo(planId));
            Assert.That(evt.FirstName, Is.EqualTo("Sarah"));
            Assert.That(evt.LastName, Is.EqualTo("Johnson"));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ContributionMadeEvent_Properties_CanBeSet()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        var planId = Guid.NewGuid();
        var contributionDate = DateTime.UtcNow.AddDays(-5);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ContributionMadeEvent
        {
            ContributionId = contributionId,
            PlanId = planId,
            Amount = 500m,
            ContributionDate = contributionDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ContributionId, Is.EqualTo(contributionId));
            Assert.That(evt.PlanId, Is.EqualTo(planId));
            Assert.That(evt.Amount, Is.EqualTo(500m));
            Assert.That(evt.ContributionDate, Is.EqualTo(contributionDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ProjectionCreatedEvent_Properties_CanBeSet()
    {
        // Arrange
        var projectionId = Guid.NewGuid();
        var planId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ProjectionCreatedEvent
        {
            ProjectionId = projectionId,
            PlanId = planId,
            TargetGoal = 100000m,
            YearsUntilCollege = 10,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ProjectionId, Is.EqualTo(projectionId));
            Assert.That(evt.PlanId, Is.EqualTo(planId));
            Assert.That(evt.TargetGoal, Is.EqualTo(100000m));
            Assert.That(evt.YearsUntilCollege, Is.EqualTo(10));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
