// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SubscriptionAuditTool.Core.Tests;

public class CategoryTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesCategory()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var name = "Streaming Services";
        var description = "Video and music streaming subscriptions";
        var colorCode = "#FF5733";

        // Act
        var category = new Category
        {
            CategoryId = categoryId,
            Name = name,
            Description = description,
            ColorCode = colorCode
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(category.CategoryId, Is.EqualTo(categoryId));
            Assert.That(category.Name, Is.EqualTo(name));
            Assert.That(category.Description, Is.EqualTo(description));
            Assert.That(category.ColorCode, Is.EqualTo(colorCode));
        });
    }

    [Test]
    public void DefaultValues_NewCategory_HasExpectedDefaults()
    {
        // Act
        var category = new Category();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(category.Name, Is.EqualTo(string.Empty));
            Assert.That(category.Subscriptions, Is.Not.Null);
            Assert.That(category.Subscriptions, Is.Empty);
        });
    }

    [Test]
    public void CalculateTotalMonthlyCost_NoSubscriptions_ReturnsZero()
    {
        // Arrange
        var category = new Category();

        // Act
        var total = category.CalculateTotalMonthlyCost();

        // Assert
        Assert.That(total, Is.EqualTo(0));
    }

    [Test]
    public void CalculateTotalMonthlyCost_MonthlySubscriptions_ReturnsSum()
    {
        // Arrange
        var category = new Category
        {
            Subscriptions = new List<Subscription>
            {
                new Subscription { Cost = 10.99m, BillingCycle = BillingCycle.Monthly, Status = SubscriptionStatus.Active },
                new Subscription { Cost = 15.99m, BillingCycle = BillingCycle.Monthly, Status = SubscriptionStatus.Active }
            }
        };

        // Act
        var total = category.CalculateTotalMonthlyCost();

        // Assert
        Assert.That(total, Is.EqualTo(26.98m));
    }

    [Test]
    public void CalculateTotalMonthlyCost_AnnualSubscription_ReturnsMonthlyEquivalent()
    {
        // Arrange
        var category = new Category
        {
            Subscriptions = new List<Subscription>
            {
                new Subscription { Cost = 120m, BillingCycle = BillingCycle.Annual, Status = SubscriptionStatus.Active }
            }
        };

        // Act
        var total = category.CalculateTotalMonthlyCost();

        // Assert
        Assert.That(total, Is.EqualTo(10m));
    }

    [Test]
    public void CalculateTotalMonthlyCost_QuarterlySubscription_ReturnsMonthlyEquivalent()
    {
        // Arrange
        var category = new Category
        {
            Subscriptions = new List<Subscription>
            {
                new Subscription { Cost = 30m, BillingCycle = BillingCycle.Quarterly, Status = SubscriptionStatus.Active }
            }
        };

        // Act
        var total = category.CalculateTotalMonthlyCost();

        // Assert
        Assert.That(total, Is.EqualTo(10m));
    }

    [Test]
    public void CalculateTotalMonthlyCost_WeeklySubscription_ReturnsMonthlyEquivalent()
    {
        // Arrange
        var category = new Category
        {
            Subscriptions = new List<Subscription>
            {
                new Subscription { Cost = 5m, BillingCycle = BillingCycle.Weekly, Status = SubscriptionStatus.Active }
            }
        };

        // Act
        var total = category.CalculateTotalMonthlyCost();

        // Assert
        Assert.That(total, Is.EqualTo(260m / 12)); // (5 * 52) / 12
    }

    [Test]
    public void CalculateTotalMonthlyCost_MixedBillingCycles_ReturnsCorrectTotal()
    {
        // Arrange
        var category = new Category
        {
            Subscriptions = new List<Subscription>
            {
                new Subscription { Cost = 10m, BillingCycle = BillingCycle.Monthly, Status = SubscriptionStatus.Active },
                new Subscription { Cost = 120m, BillingCycle = BillingCycle.Annual, Status = SubscriptionStatus.Active },
                new Subscription { Cost = 30m, BillingCycle = BillingCycle.Quarterly, Status = SubscriptionStatus.Active }
            }
        };

        // Act
        var total = category.CalculateTotalMonthlyCost();

        // Assert
        Assert.That(total, Is.EqualTo(30m)); // 10 + (120/12) + (30*4/12)
    }

    [Test]
    public void CalculateTotalMonthlyCost_IgnoresCancelledSubscriptions()
    {
        // Arrange
        var category = new Category
        {
            Subscriptions = new List<Subscription>
            {
                new Subscription { Cost = 10m, BillingCycle = BillingCycle.Monthly, Status = SubscriptionStatus.Active },
                new Subscription { Cost = 20m, BillingCycle = BillingCycle.Monthly, Status = SubscriptionStatus.Cancelled }
            }
        };

        // Act
        var total = category.CalculateTotalMonthlyCost();

        // Assert
        Assert.That(total, Is.EqualTo(10m));
    }

    [Test]
    public void CalculateTotalMonthlyCost_IgnoresPausedSubscriptions()
    {
        // Arrange
        var category = new Category
        {
            Subscriptions = new List<Subscription>
            {
                new Subscription { Cost = 10m, BillingCycle = BillingCycle.Monthly, Status = SubscriptionStatus.Active },
                new Subscription { Cost = 15m, BillingCycle = BillingCycle.Monthly, Status = SubscriptionStatus.Paused }
            }
        };

        // Act
        var total = category.CalculateTotalMonthlyCost();

        // Assert
        Assert.That(total, Is.EqualTo(10m));
    }

    [Test]
    public void CalculateTotalMonthlyCost_IgnoresPendingSubscriptions()
    {
        // Arrange
        var category = new Category
        {
            Subscriptions = new List<Subscription>
            {
                new Subscription { Cost = 10m, BillingCycle = BillingCycle.Monthly, Status = SubscriptionStatus.Active },
                new Subscription { Cost = 25m, BillingCycle = BillingCycle.Monthly, Status = SubscriptionStatus.Pending }
            }
        };

        // Act
        var total = category.CalculateTotalMonthlyCost();

        // Assert
        Assert.That(total, Is.EqualTo(10m));
    }

    [Test]
    public void Subscriptions_CanAddSubscriptions_ToCollection()
    {
        // Arrange
        var category = new Category();
        var subscription1 = new Subscription { ServiceName = "Netflix" };
        var subscription2 = new Subscription { ServiceName = "Spotify" };

        // Act
        category.Subscriptions.Add(subscription1);
        category.Subscriptions.Add(subscription2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(category.Subscriptions, Has.Count.EqualTo(2));
            Assert.That(category.Subscriptions, Contains.Item(subscription1));
            Assert.That(category.Subscriptions, Contains.Item(subscription2));
        });
    }

    [Test]
    public void Description_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var category = new Category
        {
            Description = null
        };

        // Assert
        Assert.That(category.Description, Is.Null);
    }

    [Test]
    public void ColorCode_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var category = new Category
        {
            ColorCode = null
        };

        // Assert
        Assert.That(category.ColorCode, Is.Null);
    }

    [Test]
    public void ColorCode_CanStoreHexValue()
    {
        // Arrange
        var colorCode = "#3498DB";

        // Act
        var category = new Category
        {
            ColorCode = colorCode
        };

        // Assert
        Assert.That(category.ColorCode, Is.EqualTo(colorCode));
    }

    [Test]
    public void Name_CanStoreValue()
    {
        // Arrange
        var name = "Entertainment";

        // Act
        var category = new Category
        {
            Name = name
        };

        // Assert
        Assert.That(category.Name, Is.EqualTo(name));
    }
}
