// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using System.Net.Http.Json;
using FuelEconomyTracker.Api.Features.Vehicles;
using NUnit.Framework;

namespace FuelEconomyTracker.Api.Tests;

[TestFixture]
public class VehiclesControllerTests
{
    private FuelEconomyTrackerWebApplicationFactory _factory = null!;
    private HttpClient _client = null!;

    [SetUp]
    public void Setup()
    {
        _factory = new FuelEconomyTrackerWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }

    [Test]
    public async Task GetVehicles_ReturnsEmptyList_WhenNoVehiclesExist()
    {
        // Act
        var response = await _client.GetAsync("/api/vehicles");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var vehicles = await response.Content.ReadFromJsonAsync<List<VehicleDto>>();
        Assert.That(vehicles, Is.Not.Null);
        Assert.That(vehicles, Is.Empty);
    }

    [Test]
    public async Task CreateVehicle_ReturnsCreatedVehicle()
    {
        // Arrange
        var request = new CreateVehicleRequest
        {
            Make = "Toyota",
            Model = "Camry",
            Year = 2023,
            VIN = "1HGBH41JXMN109186",
            LicensePlate = "ABC123",
            TankCapacity = 15.8m,
            EPACityMPG = 28m,
            EPAHighwayMPG = 39m
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/vehicles", request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        var vehicle = await response.Content.ReadFromJsonAsync<VehicleDto>();
        Assert.That(vehicle, Is.Not.Null);
        Assert.That(vehicle.Make, Is.EqualTo("Toyota"));
        Assert.That(vehicle.Model, Is.EqualTo("Camry"));
        Assert.That(vehicle.Year, Is.EqualTo(2023));
    }

    [Test]
    public async Task GetVehicleById_ReturnsVehicle_WhenVehicleExists()
    {
        // Arrange
        var createRequest = new CreateVehicleRequest
        {
            Make = "Honda",
            Model = "Accord",
            Year = 2022
        };

        var createResponse = await _client.PostAsJsonAsync("/api/vehicles", createRequest);
        var createdVehicle = await createResponse.Content.ReadFromJsonAsync<VehicleDto>();

        // Act
        var response = await _client.GetAsync($"/api/vehicles/{createdVehicle!.VehicleId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var vehicle = await response.Content.ReadFromJsonAsync<VehicleDto>();
        Assert.That(vehicle, Is.Not.Null);
        Assert.That(vehicle.VehicleId, Is.EqualTo(createdVehicle.VehicleId));
        Assert.That(vehicle.Make, Is.EqualTo("Honda"));
    }

    [Test]
    public async Task UpdateVehicle_ReturnsUpdatedVehicle()
    {
        // Arrange
        var createRequest = new CreateVehicleRequest
        {
            Make = "Ford",
            Model = "F-150",
            Year = 2021
        };

        var createResponse = await _client.PostAsJsonAsync("/api/vehicles", createRequest);
        var createdVehicle = await createResponse.Content.ReadFromJsonAsync<VehicleDto>();

        var updateRequest = new UpdateVehicleRequest
        {
            Make = "Ford",
            Model = "F-150 Raptor",
            Year = 2021,
            IsActive = true
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/vehicles/{createdVehicle!.VehicleId}", updateRequest);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var vehicle = await response.Content.ReadFromJsonAsync<VehicleDto>();
        Assert.That(vehicle, Is.Not.Null);
        Assert.That(vehicle.Model, Is.EqualTo("F-150 Raptor"));
    }

    [Test]
    public async Task DeleteVehicle_ReturnsNoContent()
    {
        // Arrange
        var createRequest = new CreateVehicleRequest
        {
            Make = "Chevrolet",
            Model = "Silverado",
            Year = 2020
        };

        var createResponse = await _client.PostAsJsonAsync("/api/vehicles", createRequest);
        var createdVehicle = await createResponse.Content.ReadFromJsonAsync<VehicleDto>();

        // Act
        var response = await _client.DeleteAsync($"/api/vehicles/{createdVehicle!.VehicleId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}
