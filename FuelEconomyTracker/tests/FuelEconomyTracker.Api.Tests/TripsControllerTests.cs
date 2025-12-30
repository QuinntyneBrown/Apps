// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using System.Net.Http.Json;
using FuelEconomyTracker.Api.Features.Trips;
using FuelEconomyTracker.Api.Features.Vehicles;
using NUnit.Framework;

namespace FuelEconomyTracker.Api.Tests;

[TestFixture]
public class TripsControllerTests
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
    public async Task GetTrips_ReturnsEmptyList_WhenNoTripsExist()
    {
        // Act
        var response = await _client.GetAsync("/api/trips");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var trips = await response.Content.ReadFromJsonAsync<List<TripDto>>();
        Assert.That(trips, Is.Not.Null);
        Assert.That(trips, Is.Empty);
    }

    [Test]
    public async Task CreateTrip_ReturnsCreatedTrip()
    {
        // Arrange
        var request = new CreateTripRequest
        {
            VehicleId = _vehicleId,
            StartDate = DateTime.UtcNow,
            StartOdometer = 10000m,
            Purpose = "Business Meeting",
            StartLocation = "Home"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/trips", request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        var trip = await response.Content.ReadFromJsonAsync<TripDto>();
        Assert.That(trip, Is.Not.Null);
        Assert.That(trip.VehicleId, Is.EqualTo(_vehicleId));
        Assert.That(trip.Purpose, Is.EqualTo("Business Meeting"));
    }

    [Test]
    public async Task CompleteTrip_ReturnsCompletedTrip()
    {
        // Arrange
        var createRequest = new CreateTripRequest
        {
            VehicleId = _vehicleId,
            StartDate = DateTime.UtcNow.AddHours(-2),
            StartOdometer = 10000m,
            Purpose = "Business Meeting",
            StartLocation = "Home"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/trips", createRequest);
        var createdTrip = await createResponse.Content.ReadFromJsonAsync<TripDto>();

        var completeRequest = new CompleteTripRequest
        {
            EndDate = DateTime.UtcNow,
            EndOdometer = 10050m,
            EndLocation = "Office"
        };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/trips/{createdTrip!.TripId}/complete", completeRequest);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var trip = await response.Content.ReadFromJsonAsync<TripDto>();
        Assert.That(trip, Is.Not.Null);
        Assert.That(trip.EndOdometer, Is.EqualTo(10050m));
        Assert.That(trip.Distance, Is.EqualTo(50m));
    }

    [Test]
    public async Task DeleteTrip_ReturnsNoContent()
    {
        // Arrange
        var createRequest = new CreateTripRequest
        {
            VehicleId = _vehicleId,
            StartDate = DateTime.UtcNow,
            StartOdometer = 10000m
        };

        var createResponse = await _client.PostAsJsonAsync("/api/trips", createRequest);
        var createdTrip = await createResponse.Content.ReadFromJsonAsync<TripDto>();

        // Act
        var response = await _client.DeleteAsync($"/api/trips/{createdTrip!.TripId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}
