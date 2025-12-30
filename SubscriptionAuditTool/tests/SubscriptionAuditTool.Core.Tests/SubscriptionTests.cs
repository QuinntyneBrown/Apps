// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SubscriptionAuditTool.Core.Tests;

public class SubscriptionTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesSubscription()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        var serviceName = "Netflix";
        var cost = 15.99m;
        var billingCycle = BillingCycle.Monthly;
        var nextBillingDate = new DateTime(2024, 2, 1);
        var status = SubscriptionStatus.Active;
        var startDate = new DateTime(2024, 1, 1);
        var categoryId = Guid.NewGuid();
        var notes = "Premium plan";

        // Act
        var subscription = new Subscription
        {
            SubscriptionId = subscriptionId,
            ServiceName = serviceName,
            Cost = cost,
            BillingCycle = billingCycle,
            NextBillingDate = nextBillingDate,
            Status = status,
            StartDate = startDate,
            CategoryId = categoryId,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subscription.SubscriptionId, Is.EqualTo(subscriptionId));
            Assert.That(subscription.ServiceName, Is.EqualTo(serviceName));
            Assert.That(subscription.Cost, Is.EqualTo(cost));
            Assert.That(subscription.BillingCycle, Is.EqualTo(billingCycle));
            Assert.That(subscription.NextBillingDate, Is.EqualTo(nextBillingDate));
            Assert.That(subscription.Status, Is.EqualTo(status));
            Assert.That(subscription.StartDate, Is.EqualTo(startDate));
            Assert.That(subscription.CategoryId, Is.EqualTo(categoryId));
            Assert.That(subscription.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void DefaultValues_NewSubscription_HasExpectedDefaults()
    {
        // Act
        var subscription = new Subscription();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subscription.ServiceName, Is.EqualTo(string.Empty));
            Assert.That(subscription.Cost, Is.EqualTo(0));
            Assert.That(subscription.BillingCycle, Is.EqualTo(BillingCycle.Weekly));
            Assert.That(subscription.Status, Is.EqualTo(SubscriptionStatus.Active));
        });
    }

    [Test]
    public void CalculateAnnualCost_MonthlyBilling_ReturnsCorrectAmount()
    {
        // Arrange
        var subscription = new Subscription
        {
            Cost = 10m,
            BillingCycle = BillingCycle.Monthly
        };

        // Act
        var annualCost = subscription.CalculateAnnualCost();

        // Assert
        Assert.That(annualCost, Is.EqualTo(120m));
    }

    [Test]
    public void CalculateAnnualCost_QuarterlyBilling_ReturnsCorrectAmount()
    {
        // Arrange
        var subscription = new Subscription
        {
            Cost = 30m,
            BillingCycle = BillingCycle.Quarterly
        };

        // Act
        var annualCost = subscription.CalculateAnnualCost();

        // Assert
        Assert.That(annualCost, Is.EqualTo(120m));
    }

    [Test]
    public void CalculateAnnualCost_AnnualBilling_ReturnsCost()
    {
        // Arrange
        var subscription = new Subscription
        {
            Cost = 100m,
            BillingCycle = BillingCycle.Annual
        };

        // Act
        var annualCost = subscription.CalculateAnnualCost();

        // Assert
        Assert.That(annualCost, Is.EqualTo(100m));
    }

    [Test]
    public void CalculateAnnualCost_WeeklyBilling_ReturnsCorrectAmount()
    {
        // Arrange
        var subscription = new Subscription
        {
            Cost = 5m,
            BillingCycle = BillingCycle.Weekly
        };

        // Act
        var annualCost = subscription.CalculateAnnualCost();

        // Assert
        Assert.That(annualCost, Is.EqualTo(260m)); // 5 * 52
    }

    [Test]
    public void Cancel_ActiveSubscription_UpdatesStatusAndDate()
    {
        // Arrange
        var subscription = new Subscription
        {
            Status = SubscriptionStatus.Active
        };

        // Act
        subscription.Cancel();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subscription.Status, Is.EqualTo(SubscriptionStatus.Cancelled));
            Assert.That(subscription.CancellationDate, Is.Not.Null);
            Assert.That(subscription.CancellationDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Cancel_PausedSubscription_UpdatesStatusAndDate()
    {
        // Arrange
        var subscription = new Subscription
        {
            Status = SubscriptionStatus.Paused
        };

        // Act
        subscription.Cancel();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subscription.Status, Is.EqualTo(SubscriptionStatus.Cancelled));
            Assert.That(subscription.CancellationDate, Is.Not.Null);
        });
    }

    [Test]
    public void Reactivate_CancelledSubscription_UpdatesStatusAndClearsDate()
    {
        // Arrange
        var subscription = new Subscription
        {
            Status = SubscriptionStatus.Cancelled,
            CancellationDate = DateTime.UtcNow
        };

        // Act
        subscription.Reactivate();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subscription.Status, Is.EqualTo(SubscriptionStatus.Active));
            Assert.That(subscription.CancellationDate, Is.Null);
        });
    }

    [Test]
    public void Reactivate_PausedSubscription_UpdatesStatusAndClearsDate()
    {
        // Arrange
        var subscription = new Subscription
        {
            Status = SubscriptionStatus.Paused,
            CancellationDate = DateTime.UtcNow.AddDays(-5)
        };

        // Act
        subscription.Reactivate();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subscription.Status, Is.EqualTo(SubscriptionStatus.Active));
            Assert.That(subscription.CancellationDate, Is.Null);
        });
    }

    [Test]
    public void BillingCycle_AllEnumValues_CanBeAssigned()
    {
        // Arrange
        var subscription = new Subscription();

        // Act & Assert
        subscription.BillingCycle = BillingCycle.Weekly;
        Assert.That(subscription.BillingCycle, Is.EqualTo(BillingCycle.Weekly));

        subscription.BillingCycle = BillingCycle.Monthly;
        Assert.That(subscription.BillingCycle, Is.EqualTo(BillingCycle.Monthly));

        subscription.BillingCycle = BillingCycle.Quarterly;
        Assert.That(subscription.BillingCycle, Is.EqualTo(BillingCycle.Quarterly));

        subscription.BillingCycle = BillingCycle.Annual;
        Assert.That(subscription.BillingCycle, Is.EqualTo(BillingCycle.Annual));
    }

    [Test]
    public void Status_AllEnumValues_CanBeAssigned()
    {
        // Arrange
        var subscription = new Subscription();

        // Act & Assert
        subscription.Status = SubscriptionStatus.Active;
        Assert.That(subscription.Status, Is.EqualTo(SubscriptionStatus.Active));

        subscription.Status = SubscriptionStatus.Paused;
        Assert.That(subscription.Status, Is.EqualTo(SubscriptionStatus.Paused));

        subscription.Status = SubscriptionStatus.Cancelled;
        Assert.That(subscription.Status, Is.EqualTo(SubscriptionStatus.Cancelled));

        subscription.Status = SubscriptionStatus.Pending;
        Assert.That(subscription.Status, Is.EqualTo(SubscriptionStatus.Pending));
    }

    [Test]
    public void CancellationDate_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var subscription = new Subscription
        {
            CancellationDate = null
        };

        // Assert
        Assert.That(subscription.CancellationDate, Is.Null);
    }

    [Test]
    public void CategoryId_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var subscription = new Subscription
        {
            CategoryId = null
        };

        // Assert
        Assert.That(subscription.CategoryId, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var subscription = new Subscription
        {
            Notes = null
        };

        // Assert
        Assert.That(subscription.Notes, Is.Null);
    }

    [Test]
    public void Category_NavigationProperty_CanBeNull()
    {
        // Arrange & Act
        var subscription = new Subscription
        {
            Category = null
        };

        // Assert
        Assert.That(subscription.Category, Is.Null);
    }

    [Test]
    public void Category_NavigationProperty_CanBeSet()
    {
        // Arrange
        var category = new Category
        {
            CategoryId = Guid.NewGuid(),
            Name = "Streaming"
        };

        // Act
        var subscription = new Subscription
        {
            CategoryId = category.CategoryId,
            Category = category
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subscription.Category, Is.Not.Null);
            Assert.That(subscription.Category.Name, Is.EqualTo("Streaming"));
            Assert.That(subscription.CategoryId, Is.EqualTo(category.CategoryId));
        });
    }

    [Test]
    public void Cost_CanStoreDecimalValue()
    {
        // Arrange & Act
        var subscription = new Subscription
        {
            Cost = 9.99m
        };

        // Assert
        Assert.That(subscription.Cost, Is.EqualTo(9.99m));
    }

    [Test]
    public void NextBillingDate_CanStoreFutureDate()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddMonths(1);

        // Act
        var subscription = new Subscription
        {
            NextBillingDate = futureDate
        };

        // Assert
        Assert.That(subscription.NextBillingDate, Is.EqualTo(futureDate).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void StartDate_CanStorePastDate()
    {
        // Arrange
        var pastDate = new DateTime(2023, 1, 1);

        // Act
        var subscription = new Subscription
        {
            StartDate = pastDate
        };

        // Assert
        Assert.That(subscription.StartDate, Is.EqualTo(pastDate));
    }

    [Test]
    public void ServiceName_CanStoreValue()
    {
        // Arrange
        var serviceName = "Amazon Prime";

        // Act
        var subscription = new Subscription
        {
            ServiceName = serviceName
        };

        // Assert
        Assert.That(subscription.ServiceName, Is.EqualTo(serviceName));
    }
}
