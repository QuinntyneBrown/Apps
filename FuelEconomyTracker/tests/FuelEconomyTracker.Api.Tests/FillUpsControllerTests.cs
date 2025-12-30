// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using System.Net.Http.Json;
using FuelEconomyTracker.Api.Features.FillUps;
using FuelEconomyTracker.Api.Features.Vehicles;
using NUnit.Framework;

namespace FuelEconomyTracker.Api.Tests;

[TestFixture]
public class FillUpsControllerTests
{
    private FuelEconomyTrackerWebApplicationFactory _factory = null!;
    private HttpClient _client = null!;
    private Guid _vehicleId;

    [SetUp]
    public async Task Setup()
    {
        _factory = new FuelEconomyTrackerWebApplicationFactory();
        _client = _factory.CreateClient();

        // Create a vehicle for testing
        var vehicleRequest = new CreateVehicleRequest
        {
            Make = "Toyota",
            Model = "Camry",
            Year = 2023
        };

        var vehicleResponse = await _client.PostAsJsonAsync("/api/vehicles", vehicleRequest);
        var vehicle = await vehicleResponse.Content.ReadFromJsonAsync<VehicleDto>();
        _vehicleId = vehicle!.VehicleId;
    }

    [TearDown]
    public void TearDown()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }

    [Test]
    public async Task GetFillUps_ReturnsEmptyList_WhenNoFillUpsExist()
    {
        // Act
        var response = await _client.GetAsync("/api/fillups");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var fillUps = await response.Content.ReadFromJsonAsync<List<FillUpDto>>();
        Assert.That(fillUps, Is.Not.Null);
        Assert.That(fillUps, Is.Empty);
    }

    [Test]
    public async Task CreateFillUp_ReturnsCreatedFillUp()
    {
        // Arrange
        var request = new CreateFillUpRequest
        {
            VehicleId = _vehicleId,
            FillUpDate = DateTime.UtcNow,
            Odometer = 10000m,
            Gallons = 12.5m,
            PricePerGallon = 3.50m,
            IsFullTank = true,
            FuelGrade = "Regular",
            GasStation = "Shell"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/fillups", request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        var fillUp = await response.Content.ReadFromJsonAsync<FillUpDto>();
        Assert.That(fillUp, Is.Not.Null);
        Assert.That(fillUp.VehicleId, Is.EqualTo(_vehicleId));
        Assert.That(fillUp.Gallons, Is.EqualTo(12.5m));
        Assert.That(fillUp.TotalCost, Is.EqualTo(43.75m));
    }

    [Test]
    public async Task GetFillUpById_ReturnsFillUp_WhenFillUpExists()
    {
        // Arrange
        var createRequest = new CreateFillUpRequest
        {
            VehicleId = _vehicleId,
            FillUpDate = DateTime.UtcNow,
            Odometer = 10000m,
            Gallons = 12.5m,
            PricePerGallon = 3.50m,
            IsFullTank = true
        };

        var createResponse = await _client.PostAsJsonAsync("/api/fillups", createRequest);
        var createdFillUp = await createResponse.Content.ReadFromJsonAsync<FillUpDto>();

        // Act
        var response = await _client.GetAsync($"/api/fillups/{createdFillUp!.FillUpId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var fillUp = await response.Content.ReadFromJsonAsync<FillUpDto>();
        Assert.That(fillUp, Is.Not.Null);
        Assert.That(fillUp.FillUpId, Is.EqualTo(createdFillUp.FillUpId));
    }

    [Test]
    public async Task DeleteFillUp_ReturnsNoContent()
    {
        // Arrange
        var createRequest = new CreateFillUpRequest
        {
            VehicleId = _vehicleId,
            FillUpDate = DateTime.UtcNow,
            Odometer = 10000m,
            Gallons = 12.5m,
            PricePerGallon = 3.50m,
            IsFullTank = true
        };

        var createResponse = await _client.PostAsJsonAsync("/api/fillups", createRequest);
        var createdFillUp = await createResponse.Content.ReadFromJsonAsync<FillUpDto>();

        // Act
        var response = await _client.DeleteAsync($"/api/fillups/{createdFillUp!.FillUpId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}
