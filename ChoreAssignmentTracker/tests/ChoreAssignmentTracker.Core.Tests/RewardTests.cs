// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core.Tests;

public class RewardTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var reward = new Reward();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reward.Name, Is.EqualTo(string.Empty));
            Assert.That(reward.PointCost, Is.EqualTo(0));
            Assert.That(reward.IsAvailable, Is.True);
            Assert.That(reward.RedeemedByFamilyMemberId, Is.Null);
            Assert.That(reward.RedeemedDate, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var rewardId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var reward = new Reward
        {
            RewardId = rewardId,
            UserId = userId,
            Name = "Extra Screen Time",
            Description = "30 minutes extra screen time",
            PointCost = 50,
            Category = "Entertainment",
            IsAvailable = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reward.RewardId, Is.EqualTo(rewardId));
            Assert.That(reward.UserId, Is.EqualTo(userId));
            Assert.That(reward.Name, Is.EqualTo("Extra Screen Time"));
            Assert.That(reward.Description, Is.EqualTo("30 minutes extra screen time"));
            Assert.That(reward.PointCost, Is.EqualTo(50));
            Assert.That(reward.Category, Is.EqualTo("Entertainment"));
            Assert.That(reward.IsAvailable, Is.True);
        });
    }

    [Test]
    public void Redeem_ValidFamilyMember_SetsRedemptionDetails()
    {
        // Arrange
        var familyMemberId = Guid.NewGuid();
        var reward = new Reward
        {
            IsAvailable = true,
            RedeemedByFamilyMemberId = null,
            RedeemedDate = null
        };

        // Act
        reward.Redeem(familyMemberId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reward.RedeemedByFamilyMemberId, Is.EqualTo(familyMemberId));
            Assert.That(reward.RedeemedDate, Is.Not.Null);
            Assert.That(reward.RedeemedDate.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(reward.IsAvailable, Is.False);
        });
    }

    [Test]
    public void Redeem_AlreadyRedeemed_UpdatesRedemptionDetails()
    {
        // Arrange
        var originalFamilyMemberId = Guid.NewGuid();
        var newFamilyMemberId = Guid.NewGuid();
        var originalDate = DateTime.UtcNow.AddDays(-1);
        var reward = new Reward
        {
            IsAvailable = false,
            RedeemedByFamilyMemberId = originalFamilyMemberId,
            RedeemedDate = originalDate
        };

        // Act
        reward.Redeem(newFamilyMemberId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reward.RedeemedByFamilyMemberId, Is.EqualTo(newFamilyMemberId));
            Assert.That(reward.RedeemedDate, Is.Not.EqualTo(originalDate));
            Assert.That(reward.RedeemedDate.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(reward.IsAvailable, Is.False);
        });
    }

    [Test]
    public void Redeem_EmptyGuid_SetsRedemptionWithEmptyGuid()
    {
        // Arrange
        var reward = new Reward
        {
            IsAvailable = true
        };

        // Act
        reward.Redeem(Guid.Empty);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reward.RedeemedByFamilyMemberId, Is.EqualTo(Guid.Empty));
            Assert.That(reward.RedeemedDate, Is.Not.Null);
            Assert.That(reward.IsAvailable, Is.False);
        });
    }
}
