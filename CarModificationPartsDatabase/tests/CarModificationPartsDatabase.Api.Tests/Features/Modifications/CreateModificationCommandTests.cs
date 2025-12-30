// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Api.Tests;

/// <summary>
/// Unit tests for CreateModificationCommandHandler.
/// </summary>
[TestFixture]
public class CreateModificationCommandTests
{
    private CarModificationPartsDatabaseContext _context = null!;
    private Mock<ILogger<CreateModificationCommandHandler>> _loggerMock = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<CarModificationPartsDatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CarModificationPartsDatabaseContext(options);
        _loggerMock = new Mock<ILogger<CreateModificationCommandHandler>>();
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Handle creates a modification successfully.
    /// </summary>
    [Test]
    public async Task Handle_CreatesModification()
    {
        // Arrange
        var handler = new CreateModificationCommandHandler(_context, _loggerMock.Object);
        var command = new CreateModificationCommand
        {
            Name = "Turbocharger Kit",
            Category = ModCategory.ForcedInduction,
            Description = "High-performance turbocharger",
            Manufacturer = "Garrett",
            EstimatedCost = 2500.00m,
            DifficultyLevel = 5,
            EstimatedInstallationTime = 8.0m,
            PerformanceGain = "100+ HP increase",
            CompatibleVehicles = new List<string> { "Honda Civic Si 2020-2023" },
            RequiredTools = new List<string> { "Complete tool set", "Engine hoist" },
            Notes = "Professional installation recommended",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Turbocharger Kit"));
        Assert.That(result.Category, Is.EqualTo(ModCategory.ForcedInduction));
        Assert.That(result.EstimatedCost, Is.EqualTo(2500.00m));

        var savedEntity = await _context.Modifications.FindAsync(result.ModificationId);
        Assert.That(savedEntity, Is.Not.Null);
    }
}
