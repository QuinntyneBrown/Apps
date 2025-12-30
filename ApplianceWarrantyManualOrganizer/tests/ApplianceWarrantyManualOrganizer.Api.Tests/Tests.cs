using ApplianceWarrantyManualOrganizer.Api.Features.Appliances;
using ApplianceWarrantyManualOrganizer.Api.Features.Warranties;
using ApplianceWarrantyManualOrganizer.Api.Features.Manuals;
using ApplianceWarrantyManualOrganizer.Api.Features.ServiceRecords;
using ApplianceWarrantyManualOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ApplianceWarrantyManualOrganizer.Api.Tests;

public class ApplianceTests
{
    private Mock<IApplianceWarrantyManualOrganizerContext> _mockContext;
    private Mock<DbSet<Appliance>> _mockApplianceSet;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IApplianceWarrantyManualOrganizerContext>();
        _mockApplianceSet = new Mock<DbSet<Appliance>>();
    }

    [Test]
    public async Task CreateAppliance_ShouldReturnApplianceDto()
    {
        // Arrange
        var handler = new CreateAppliance.Handler(_mockContext.Object);
        var command = new CreateAppliance.Command
        {
            UserId = Guid.NewGuid(),
            Name = "Test Appliance",
            ApplianceType = ApplianceType.Refrigerator,
            Brand = "TestBrand"
        };

        _mockContext.Setup(c => c.Appliances).Returns(_mockApplianceSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Test Appliance"));
        Assert.That(result.ApplianceType, Is.EqualTo(ApplianceType.Refrigerator));
        Assert.That(result.Brand, Is.EqualTo("TestBrand"));
        _mockApplianceSet.Verify(m => m.Add(It.IsAny<Appliance>()), Times.Once);
        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void CreateAppliance_Validator_ShouldRequireName()
    {
        // Arrange
        var validator = new CreateAppliance.Validator();
        var command = new CreateAppliance.Command
        {
            UserId = Guid.NewGuid(),
            Name = "",
            ApplianceType = ApplianceType.Refrigerator
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.PropertyName == "Name"), Is.True);
    }
}

public class WarrantyTests
{
    private Mock<IApplianceWarrantyManualOrganizerContext> _mockContext;
    private Mock<DbSet<Warranty>> _mockWarrantySet;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IApplianceWarrantyManualOrganizerContext>();
        _mockWarrantySet = new Mock<DbSet<Warranty>>();
    }

    [Test]
    public async Task CreateWarranty_ShouldReturnWarrantyDto()
    {
        // Arrange
        var handler = new CreateWarranty.Handler(_mockContext.Object);
        var command = new CreateWarranty.Command
        {
            ApplianceId = Guid.NewGuid(),
            Provider = "Test Provider",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(1)
        };

        _mockContext.Setup(c => c.Warranties).Returns(_mockWarrantySet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Provider, Is.EqualTo("Test Provider"));
        _mockWarrantySet.Verify(m => m.Add(It.IsAny<Warranty>()), Times.Once);
        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void CreateWarranty_Validator_ShouldRequireApplianceId()
    {
        // Arrange
        var validator = new CreateWarranty.Validator();
        var command = new CreateWarranty.Command
        {
            ApplianceId = Guid.Empty,
            Provider = "Test Provider"
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.PropertyName == "ApplianceId"), Is.True);
    }
}

public class ManualTests
{
    private Mock<IApplianceWarrantyManualOrganizerContext> _mockContext;
    private Mock<DbSet<Manual>> _mockManualSet;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IApplianceWarrantyManualOrganizerContext>();
        _mockManualSet = new Mock<DbSet<Manual>>();
    }

    [Test]
    public async Task CreateManual_ShouldReturnManualDto()
    {
        // Arrange
        var handler = new CreateManual.Handler(_mockContext.Object);
        var command = new CreateManual.Command
        {
            ApplianceId = Guid.NewGuid(),
            Title = "User Manual",
            FileUrl = "https://example.com/manual.pdf",
            FileType = "PDF"
        };

        _mockContext.Setup(c => c.Manuals).Returns(_mockManualSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo("User Manual"));
        Assert.That(result.FileUrl, Is.EqualTo("https://example.com/manual.pdf"));
        _mockManualSet.Verify(m => m.Add(It.IsAny<Manual>()), Times.Once);
        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void CreateManual_Validator_ShouldRequireApplianceId()
    {
        // Arrange
        var validator = new CreateManual.Validator();
        var command = new CreateManual.Command
        {
            ApplianceId = Guid.Empty,
            Title = "Test Manual"
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.PropertyName == "ApplianceId"), Is.True);
    }
}

public class ServiceRecordTests
{
    private Mock<IApplianceWarrantyManualOrganizerContext> _mockContext;
    private Mock<DbSet<ServiceRecord>> _mockServiceRecordSet;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IApplianceWarrantyManualOrganizerContext>();
        _mockServiceRecordSet = new Mock<DbSet<ServiceRecord>>();
    }

    [Test]
    public async Task CreateServiceRecord_ShouldReturnServiceRecordDto()
    {
        // Arrange
        var handler = new CreateServiceRecord.Handler(_mockContext.Object);
        var command = new CreateServiceRecord.Command
        {
            ApplianceId = Guid.NewGuid(),
            ServiceDate = DateTime.UtcNow,
            ServiceProvider = "Test Service Provider",
            Description = "Test service description",
            Cost = 100.50m
        };

        _mockContext.Setup(c => c.ServiceRecords).Returns(_mockServiceRecordSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ServiceProvider, Is.EqualTo("Test Service Provider"));
        Assert.That(result.Cost, Is.EqualTo(100.50m));
        _mockServiceRecordSet.Verify(m => m.Add(It.IsAny<ServiceRecord>()), Times.Once);
        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void CreateServiceRecord_Validator_ShouldRequireServiceDate()
    {
        // Arrange
        var validator = new CreateServiceRecord.Validator();
        var command = new CreateServiceRecord.Command
        {
            ApplianceId = Guid.NewGuid(),
            ServiceDate = default,
            ServiceProvider = "Test Provider"
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.PropertyName == "ServiceDate"), Is.True);
    }

    [Test]
    public void CreateServiceRecord_Validator_ShouldRejectNegativeCost()
    {
        // Arrange
        var validator = new CreateServiceRecord.Validator();
        var command = new CreateServiceRecord.Command
        {
            ApplianceId = Guid.NewGuid(),
            ServiceDate = DateTime.UtcNow,
            ServiceProvider = "Test Provider",
            Cost = -10.0m
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.PropertyName == "Cost"), Is.True);
    }
}
