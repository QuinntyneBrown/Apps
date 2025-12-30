// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core.Tests;

public class FamilyMemberTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var familyMember = new FamilyMember();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(familyMember.Name, Is.EqualTo(string.Empty));
            Assert.That(familyMember.TotalPoints, Is.EqualTo(0));
            Assert.That(familyMember.AvailablePoints, Is.EqualTo(0));
            Assert.That(familyMember.IsActive, Is.True);
            Assert.That(familyMember.Assignments, Is.Not.Null);
            Assert.That(familyMember.RedeemedRewards, Is.Not.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var familyMemberId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var familyMember = new FamilyMember
        {
            FamilyMemberId = familyMemberId,
            UserId = userId,
            Name = "John Doe",
            Age = 10,
            Avatar = "avatar1",
            TotalPoints = 100,
            AvailablePoints = 50,
            IsActive = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(familyMember.FamilyMemberId, Is.EqualTo(familyMemberId));
            Assert.That(familyMember.UserId, Is.EqualTo(userId));
            Assert.That(familyMember.Name, Is.EqualTo("John Doe"));
            Assert.That(familyMember.Age, Is.EqualTo(10));
            Assert.That(familyMember.Avatar, Is.EqualTo("avatar1"));
            Assert.That(familyMember.TotalPoints, Is.EqualTo(100));
            Assert.That(familyMember.AvailablePoints, Is.EqualTo(50));
            Assert.That(familyMember.IsActive, Is.True);
        });
    }

    [Test]
    public void AwardPoints_ValidPoints_IncreasesTotalAndAvailablePoints()
    {
        // Arrange
        var familyMember = new FamilyMember
        {
            TotalPoints = 50,
            AvailablePoints = 30
        };

        // Act
        familyMember.AwardPoints(20);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(familyMember.TotalPoints, Is.EqualTo(70));
            Assert.That(familyMember.AvailablePoints, Is.EqualTo(50));
        });
    }

    [Test]
    public void AwardPoints_ZeroPoints_DoesNotChangePoints()
    {
        // Arrange
        var familyMember = new FamilyMember
        {
            TotalPoints = 50,
            AvailablePoints = 30
        };

        // Act
        familyMember.AwardPoints(0);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(familyMember.TotalPoints, Is.EqualTo(50));
            Assert.That(familyMember.AvailablePoints, Is.EqualTo(30));
        });
    }

    [Test]
    public void AwardPoints_NegativePoints_DecreasesPoints()
    {
        // Arrange
        var familyMember = new FamilyMember
        {
            TotalPoints = 50,
            AvailablePoints = 30
        };

        // Act
        familyMember.AwardPoints(-10);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(familyMember.TotalPoints, Is.EqualTo(40));
            Assert.That(familyMember.AvailablePoints, Is.EqualTo(20));
        });
    }

    [Test]
    public void RedeemPoints_SufficientPoints_ReturnsTrue()
    {
        // Arrange
        var familyMember = new FamilyMember
        {
            AvailablePoints = 100
        };

        // Act
        var result = familyMember.RedeemPoints(50);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(familyMember.AvailablePoints, Is.EqualTo(50));
        });
    }

    [Test]
    public void RedeemPoints_ExactPoints_ReturnsTrue()
    {
        // Arrange
        var familyMember = new FamilyMember
        {
            AvailablePoints = 50
        };

        // Act
        var result = familyMember.RedeemPoints(50);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(familyMember.AvailablePoints, Is.EqualTo(0));
        });
    }

    [Test]
    public void RedeemPoints_InsufficientPoints_ReturnsFalse()
    {
        // Arrange
        var familyMember = new FamilyMember
        {
            AvailablePoints = 30
        };

        // Act
        var result = familyMember.RedeemPoints(50);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(familyMember.AvailablePoints, Is.EqualTo(30));
        });
    }

    [Test]
    public void GetCompletionRate_NoAssignments_ReturnsZero()
    {
        // Arrange
        var familyMember = new FamilyMember();

        // Act
        var rate = familyMember.GetCompletionRate();

        // Assert
        Assert.That(rate, Is.EqualTo(0));
    }

    [Test]
    public void GetCompletionRate_AllCompleted_Returns100()
    {
        // Arrange
        var familyMember = new FamilyMember
        {
            Assignments = new List<Assignment>
            {
                new Assignment { IsCompleted = true },
                new Assignment { IsCompleted = true },
                new Assignment { IsCompleted = true }
            }
        };

        // Act
        var rate = familyMember.GetCompletionRate();

        // Assert
        Assert.That(rate, Is.EqualTo(100));
    }

    [Test]
    public void GetCompletionRate_PartiallyCompleted_ReturnsCorrectPercentage()
    {
        // Arrange
        var familyMember = new FamilyMember
        {
            Assignments = new List<Assignment>
            {
                new Assignment { IsCompleted = true },
                new Assignment { IsCompleted = false },
                new Assignment { IsCompleted = true },
                new Assignment { IsCompleted = false }
            }
        };

        // Act
        var rate = familyMember.GetCompletionRate();

        // Assert
        Assert.That(rate, Is.EqualTo(50));
    }

    [Test]
    public void GetCompletionRate_NoneCompleted_ReturnsZero()
    {
        // Arrange
        var familyMember = new FamilyMember
        {
            Assignments = new List<Assignment>
            {
                new Assignment { IsCompleted = false },
                new Assignment { IsCompleted = false }
            }
        };

        // Act
        var rate = familyMember.GetCompletionRate();

        // Assert
        Assert.That(rate, Is.EqualTo(0));
    }
}
