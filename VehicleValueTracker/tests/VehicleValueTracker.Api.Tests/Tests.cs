using VehicleValueTracker.Api.Features.Vehicles;
using VehicleValueTracker.Api.Features.ValueAssessments;
using VehicleValueTracker.Api.Features.MarketComparisons;
using VehicleValueTracker.Core;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Tests;

[TestFixture]
public class VehicleCommandTests
{
    private Mock<IVehicleValueTrackerContext> _mockContext = null!;
    private Mock<ILogger<CreateVehicleCommandHandler>> _mockLogger = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IVehicleValueTrackerContext>();
        _mockLogger = new Mock<ILogger<CreateVehicleCommandHandler>>();
    }

    [Test]
    public async Task CreateVehicleCommand_ShouldCreateVehicle()
    {
        // Arrange
        var vehicles = new List<Vehicle>();
        var mockSet = TestHelpers.CreateMockDbSet(vehicles);
        _mockContext.Setup(c => c.Vehicles).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateVehicleCommandHandler(_mockContext.Object, _mockLogger.Object);
        var command = new CreateVehicleCommand
        {
            Make = "Toyota",
            Model = "Camry",
            Year = 2020,
            CurrentMileage = 25000,
            IsCurrentlyOwned = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Make, Is.EqualTo("Toyota"));
        Assert.That(result.Model, Is.EqualTo("Camry"));
        Assert.That(result.Year, Is.EqualTo(2020));
        Assert.That(result.CurrentMileage, Is.EqualTo(25000));
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateVehicleCommand_ShouldUpdateExistingVehicle()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var vehicle = new Vehicle
        {
            VehicleId = vehicleId,
            Make = "Honda",
            Model = "Accord",
            Year = 2019,
            CurrentMileage = 30000
        };
        var vehicles = new List<Vehicle> { vehicle };
        var mockSet = TestHelpers.CreateMockDbSet(vehicles);

        _mockContext.Setup(c => c.Vehicles).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var updateLogger = new Mock<ILogger<UpdateVehicleCommandHandler>>();
        var handler = new UpdateVehicleCommandHandler(_mockContext.Object, updateLogger.Object);
        var command = new UpdateVehicleCommand
        {
            VehicleId = vehicleId,
            Make = "Honda",
            Model = "Accord",
            Year = 2019,
            CurrentMileage = 35000,
            IsCurrentlyOwned = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.CurrentMileage, Is.EqualTo(35000));
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteVehicleCommand_ShouldReturnTrue_WhenVehicleExists()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var vehicle = new Vehicle { VehicleId = vehicleId, Make = "Ford", Model = "F-150", Year = 2021 };
        var vehicles = new List<Vehicle> { vehicle };
        var mockSet = TestHelpers.CreateMockDbSet(vehicles);

        _mockContext.Setup(c => c.Vehicles).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var deleteLogger = new Mock<ILogger<DeleteVehicleCommandHandler>>();
        var handler = new DeleteVehicleCommandHandler(_mockContext.Object, deleteLogger.Object);
        var command = new DeleteVehicleCommand { VehicleId = vehicleId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}

[TestFixture]
public class VehicleQueryTests
{
    private Mock<IVehicleValueTrackerContext> _mockContext = null!;
    private Mock<ILogger<GetVehiclesQueryHandler>> _mockLogger = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IVehicleValueTrackerContext>();
        _mockLogger = new Mock<ILogger<GetVehiclesQueryHandler>>();
    }

    [Test]
    public async Task GetVehiclesQuery_ShouldReturnAllVehicles()
    {
        // Arrange
        var vehicles = new List<Vehicle>
        {
            new Vehicle { VehicleId = Guid.NewGuid(), Make = "Toyota", Model = "Camry", Year = 2020 },
            new Vehicle { VehicleId = Guid.NewGuid(), Make = "Honda", Model = "Accord", Year = 2019 }
        };
        var mockSet = TestHelpers.CreateMockDbSet(vehicles);
        _mockContext.Setup(c => c.Vehicles).Returns(mockSet.Object);

        var handler = new GetVehiclesQueryHandler(_mockContext.Object, _mockLogger.Object);
        var query = new GetVehiclesQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task GetVehicleByIdQuery_ShouldReturnVehicle_WhenExists()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var vehicles = new List<Vehicle>
        {
            new Vehicle { VehicleId = vehicleId, Make = "Tesla", Model = "Model 3", Year = 2022 }
        };
        var mockSet = TestHelpers.CreateMockDbSet(vehicles);
        _mockContext.Setup(c => c.Vehicles).Returns(mockSet.Object);

        var byIdLogger = new Mock<ILogger<GetVehicleByIdQueryHandler>>();
        var handler = new GetVehicleByIdQueryHandler(_mockContext.Object, byIdLogger.Object);
        var query = new GetVehicleByIdQuery { VehicleId = vehicleId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.VehicleId, Is.EqualTo(vehicleId));
        Assert.That(result.Make, Is.EqualTo("Tesla"));
    }
}

[TestFixture]
public class ValueAssessmentCommandTests
{
    private Mock<IVehicleValueTrackerContext> _mockContext = null!;
    private Mock<ILogger<CreateValueAssessmentCommandHandler>> _mockLogger = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IVehicleValueTrackerContext>();
        _mockLogger = new Mock<ILogger<CreateValueAssessmentCommandHandler>>();
    }

    [Test]
    public async Task CreateValueAssessmentCommand_ShouldCreateAssessment()
    {
        // Arrange
        var assessments = new List<ValueAssessment>();
        var mockSet = TestHelpers.CreateMockDbSet(assessments);
        _mockContext.Setup(c => c.ValueAssessments).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateValueAssessmentCommandHandler(_mockContext.Object, _mockLogger.Object);
        var command = new CreateValueAssessmentCommand
        {
            VehicleId = Guid.NewGuid(),
            AssessmentDate = DateTime.UtcNow,
            EstimatedValue = 25000,
            MileageAtAssessment = 30000,
            ConditionGrade = ConditionGrade.Good
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.EstimatedValue, Is.EqualTo(25000));
        Assert.That(result.ConditionGrade, Is.EqualTo(ConditionGrade.Good));
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}

[TestFixture]
public class MarketComparisonCommandTests
{
    private Mock<IVehicleValueTrackerContext> _mockContext = null!;
    private Mock<ILogger<CreateMarketComparisonCommandHandler>> _mockLogger = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IVehicleValueTrackerContext>();
        _mockLogger = new Mock<ILogger<CreateMarketComparisonCommandHandler>>();
    }

    [Test]
    public async Task CreateMarketComparisonCommand_ShouldCreateComparison()
    {
        // Arrange
        var comparisons = new List<MarketComparison>();
        var mockSet = TestHelpers.CreateMockDbSet(comparisons);
        _mockContext.Setup(c => c.MarketComparisons).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateMarketComparisonCommandHandler(_mockContext.Object, _mockLogger.Object);
        var command = new CreateMarketComparisonCommand
        {
            VehicleId = Guid.NewGuid(),
            ComparisonDate = DateTime.UtcNow,
            ListingSource = "Autotrader",
            ComparableYear = 2020,
            ComparableMake = "Toyota",
            ComparableModel = "Camry",
            ComparableMileage = 28000,
            AskingPrice = 24500,
            IsActive = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ListingSource, Is.EqualTo("Autotrader"));
        Assert.That(result.AskingPrice, Is.EqualTo(24500));
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
