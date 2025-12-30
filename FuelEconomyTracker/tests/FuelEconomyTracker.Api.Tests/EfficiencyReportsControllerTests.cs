// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using System.Net.Http.Json;
using FuelEconomyTracker.Api.Features.EfficiencyReports;
using FuelEconomyTracker.Api.Features.FillUps;
using FuelEconomyTracker.Api.Features.Vehicles;
using NUnit.Framework;

namespace FuelEconomyTracker.Api.Tests;

[TestFixture]
public class EfficiencyReportsControllerTests
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

        // Create some fill-ups for testing
        await _client.PostAsJsonAsync("/api/fillups", new CreateFillUpRequest
        {
            VehicleId = _vehicleId,
            FillUpDate = DateTime.UtcNow.AddDays(-10),
            Odometer = 10000m,
            Gallons = 12.5m,
            PricePerGallon = 3.50m,
            IsFullTank = true
        });

        await _client.PostAsJsonAsync("/api/fillups", new CreateFillUpRequest
        {
            VehicleId = _vehicleId,
            FillUpDate = DateTime.UtcNow.AddDays(-5),
            Odometer = 10350m,
            Gallons = 11.8m,
            PricePerGallon = 3.60m,
            IsFullTank = true
        });
    }

    [TearDown]
    public void TearDown()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }

    [Test]
    public async Task GetEfficiencyReports_ReturnsEmptyList_WhenNoReportsExist()
    {
        // Act
        var response = await _client.GetAsync("/api/efficiencyreports");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var reports = await response.Content.ReadFromJsonAsync<List<EfficiencyReportDto>>();
        Assert.That(reports, Is.Not.Null);
        Assert.That(reports, Is.Empty);
    }

    [Test]
    public async Task GenerateReport_ReturnsGeneratedReport()
    {
        // Arrange
        var request = new GenerateEfficiencyReportRequest
        {
            VehicleId = _vehicleId,
            StartDate = DateTime.UtcNow.AddDays(-15),
            EndDate = DateTime.UtcNow,
            Notes = "Monthly report"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/efficiencyreports", request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        var report = await response.Content.ReadFromJsonAsync<EfficiencyReportDto>();
        Assert.That(report, Is.Not.Null);
        Assert.That(report.VehicleId, Is.EqualTo(_vehicleId));
        Assert.That(report.NumberOfFillUps, Is.EqualTo(2));
        Assert.That(report.TotalGallons, Is.GreaterThan(0));
    }

    [Test]
    public async Task GetEfficiencyReportById_ReturnsReport_WhenReportExists()
    {
        // Arrange
        var createRequest = new GenerateEfficiencyReportRequest
        {
            VehicleId = _vehicleId,
            StartDate = DateTime.UtcNow.AddDays(-15),
            EndDate = DateTime.UtcNow
        };

        var createResponse = await _client.PostAsJsonAsync("/api/efficiencyreports", createRequest);
        var createdReport = await createResponse.Content.ReadFromJsonAsync<EfficiencyReportDto>();

        // Act
        var response = await _client.GetAsync($"/api/efficiencyreports/{createdReport!.EfficiencyReportId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var report = await response.Content.ReadFromJsonAsync<EfficiencyReportDto>();
        Assert.That(report, Is.Not.Null);
        Assert.That(report.EfficiencyReportId, Is.EqualTo(createdReport.EfficiencyReportId));
    }

    [Test]
    public async Task DeleteEfficiencyReport_ReturnsNoContent()
    {
        // Arrange
        var createRequest = new GenerateEfficiencyReportRequest
        {
            VehicleId = _vehicleId,
            StartDate = DateTime.UtcNow.AddDays(-15),
            EndDate = DateTime.UtcNow
        };

        var createResponse = await _client.PostAsJsonAsync("/api/efficiencyreports", createRequest);
        var createdReport = await createResponse.Content.ReadFromJsonAsync<EfficiencyReportDto>();

        // Act
        var response = await _client.DeleteAsync($"/api/efficiencyreports/{createdReport!.EfficiencyReportId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}
