using SubscriptionAuditTool.Api.Features.Subscriptions;
using SubscriptionAuditTool.Api.Features.Categories;
using SubscriptionAuditTool.Core;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace SubscriptionAuditTool.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void SubscriptionDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var subscription = new Subscription
        {
            SubscriptionId = Guid.NewGuid(),
            ServiceName = "Netflix",
            Cost = 15.99m,
            BillingCycle = BillingCycle.Monthly,
            NextBillingDate = DateTime.UtcNow.AddDays(30),
            Status = SubscriptionStatus.Active,
            StartDate = DateTime.UtcNow.AddDays(-365),
            CancellationDate = null,
            CategoryId = Guid.NewGuid(),
            Notes = "Premium plan",
            Category = new Category { Name = "Entertainment" },
        };

        // Act
        var dto = subscription.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.SubscriptionId, Is.EqualTo(subscription.SubscriptionId));
            Assert.That(dto.ServiceName, Is.EqualTo(subscription.ServiceName));
            Assert.That(dto.Cost, Is.EqualTo(subscription.Cost));
            Assert.That(dto.BillingCycle, Is.EqualTo(subscription.BillingCycle));
            Assert.That(dto.NextBillingDate, Is.EqualTo(subscription.NextBillingDate));
            Assert.That(dto.Status, Is.EqualTo(subscription.Status));
            Assert.That(dto.StartDate, Is.EqualTo(subscription.StartDate));
            Assert.That(dto.CancellationDate, Is.EqualTo(subscription.CancellationDate));
            Assert.That(dto.CategoryId, Is.EqualTo(subscription.CategoryId));
            Assert.That(dto.Notes, Is.EqualTo(subscription.Notes));
            Assert.That(dto.AnnualCost, Is.EqualTo(subscription.CalculateAnnualCost()));
            Assert.That(dto.CategoryName, Is.EqualTo(subscription.Category.Name));
        });
    }

    [Test]
    public void CategoryDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var category = new Category
        {
            CategoryId = Guid.NewGuid(),
            Name = "Entertainment",
            Description = "Streaming and media services",
            ColorCode = "#FF5733",
            Subscriptions = new List<Subscription>
            {
                new Subscription
                {
                    Cost = 15.99m,
                    BillingCycle = BillingCycle.Monthly,
                    Status = SubscriptionStatus.Active,
                },
                new Subscription
                {
                    Cost = 9.99m,
                    BillingCycle = BillingCycle.Monthly,
                    Status = SubscriptionStatus.Active,
                },
            },
        };

        // Act
        var dto = category.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.CategoryId, Is.EqualTo(category.CategoryId));
            Assert.That(dto.Name, Is.EqualTo(category.Name));
            Assert.That(dto.Description, Is.EqualTo(category.Description));
            Assert.That(dto.ColorCode, Is.EqualTo(category.ColorCode));
            Assert.That(dto.TotalMonthlyCost, Is.EqualTo(category.CalculateTotalMonthlyCost()));
            Assert.That(dto.SubscriptionCount, Is.EqualTo(category.Subscriptions.Count));
        });
    }

    [Test]
    public async Task CreateSubscriptionCommand_CreatesSubscription()
    {
        // Arrange
        var subscriptions = new List<Subscription>();
        var mockContext = new Mock<ISubscriptionAuditToolContext>();
        var mockDbSet = TestHelpers.CreateMockDbSet(subscriptions);
        mockContext.Setup(c => c.Subscriptions).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var mockLogger = new Mock<ILogger<CreateSubscriptionCommandHandler>>();
        var handler = new CreateSubscriptionCommandHandler(mockContext.Object, mockLogger.Object);

        var command = new CreateSubscriptionCommand
        {
            ServiceName = "Spotify",
            Cost = 9.99m,
            BillingCycle = BillingCycle.Monthly,
            NextBillingDate = DateTime.UtcNow.AddDays(30),
            StartDate = DateTime.UtcNow,
            CategoryId = Guid.NewGuid(),
            Notes = "Premium plan",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.ServiceName, Is.EqualTo(command.ServiceName));
            Assert.That(result.Cost, Is.EqualTo(command.Cost));
            Assert.That(result.BillingCycle, Is.EqualTo(command.BillingCycle));
            Assert.That(result.Status, Is.EqualTo(SubscriptionStatus.Active));
            Assert.That(subscriptions, Has.Count.EqualTo(1));
        });

        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateCategoryCommand_CreatesCategory()
    {
        // Arrange
        var categories = new List<Category>();
        var mockContext = new Mock<ISubscriptionAuditToolContext>();
        var mockDbSet = TestHelpers.CreateMockDbSet(categories);
        mockContext.Setup(c => c.Categories).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var mockLogger = new Mock<ILogger<CreateCategoryCommandHandler>>();
        var handler = new CreateCategoryCommandHandler(mockContext.Object, mockLogger.Object);

        var command = new CreateCategoryCommand
        {
            Name = "Entertainment",
            Description = "Streaming services",
            ColorCode = "#FF5733",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo(command.Name));
            Assert.That(result.Description, Is.EqualTo(command.Description));
            Assert.That(result.ColorCode, Is.EqualTo(command.ColorCode));
            Assert.That(categories, Has.Count.EqualTo(1));
        });

        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetSubscriptionsQuery_ReturnsFilteredSubscriptions()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var subscriptions = new List<Subscription>
        {
            new Subscription
            {
                SubscriptionId = Guid.NewGuid(),
                ServiceName = "Netflix",
                Status = SubscriptionStatus.Active,
                BillingCycle = BillingCycle.Monthly,
                CategoryId = categoryId,
            },
            new Subscription
            {
                SubscriptionId = Guid.NewGuid(),
                ServiceName = "Hulu",
                Status = SubscriptionStatus.Cancelled,
                BillingCycle = BillingCycle.Monthly,
                CategoryId = categoryId,
            },
        };

        var mockContext = new Mock<ISubscriptionAuditToolContext>();
        var mockDbSet = TestHelpers.CreateMockDbSet(subscriptions);
        mockContext.Setup(c => c.Subscriptions).Returns(mockDbSet.Object);

        var mockLogger = new Mock<ILogger<GetSubscriptionsQueryHandler>>();
        var handler = new GetSubscriptionsQueryHandler(mockContext.Object, mockLogger.Object);

        var query = new GetSubscriptionsQuery
        {
            Status = SubscriptionStatus.Active,
        };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        var resultList = result.ToList();
        Assert.Multiple(() =>
        {
            Assert.That(resultList, Has.Count.EqualTo(1));
            Assert.That(resultList[0].ServiceName, Is.EqualTo("Netflix"));
            Assert.That(resultList[0].Status, Is.EqualTo(SubscriptionStatus.Active));
        });
    }

    [Test]
    public async Task DeleteSubscriptionCommand_DeletesSubscription()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        var subscriptions = new List<Subscription>
        {
            new Subscription
            {
                SubscriptionId = subscriptionId,
                ServiceName = "Netflix",
            },
        };

        var mockContext = new Mock<ISubscriptionAuditToolContext>();
        var mockDbSet = TestHelpers.CreateMockDbSet(subscriptions);
        mockContext.Setup(c => c.Subscriptions).Returns(mockDbSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var mockLogger = new Mock<ILogger<DeleteSubscriptionCommandHandler>>();
        var handler = new DeleteSubscriptionCommandHandler(mockContext.Object, mockLogger.Object);

        var command = new DeleteSubscriptionCommand { SubscriptionId = subscriptionId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(subscriptions, Is.Empty);
        });

        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteSubscriptionCommand_ReturnsFalse_WhenNotFound()
    {
        // Arrange
        var subscriptions = new List<Subscription>();
        var mockContext = new Mock<ISubscriptionAuditToolContext>();
        var mockDbSet = TestHelpers.CreateMockDbSet(subscriptions);
        mockContext.Setup(c => c.Subscriptions).Returns(mockDbSet.Object);

        var mockLogger = new Mock<ILogger<DeleteSubscriptionCommandHandler>>();
        var handler = new DeleteSubscriptionCommandHandler(mockContext.Object, mockLogger.Object);

        var command = new DeleteSubscriptionCommand { SubscriptionId = Guid.NewGuid() };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
