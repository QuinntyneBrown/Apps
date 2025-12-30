// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using System.Net.Http.Json;
using MorningRoutineBuilder.Api.Features.Routines;

namespace MorningRoutineBuilder.Api.Tests;

[TestFixture]
public class RoutinesControllerTests
{
    private MorningRoutineBuilderWebApplicationFactory _factory = null!;
    private HttpClient _client = null!;
    private Guid _userId;

    [SetUp]
    public void Setup()
    {
        _factory = new MorningRoutineBuilderWebApplicationFactory();
        _client = _factory.CreateClient();
        _userId = Guid.NewGuid();
    }

    [TearDown]
    public void TearDown()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }

    [Test]
    public async Task GetRoutines_ReturnsEmptyList_WhenNoRoutinesExist()
    {
        // Act
        var response = await _client.GetAsync("/api/routines");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var routines = await response.Content.ReadFromJsonAsync<List<RoutineDto>>();
        Assert.That(routines, Is.Not.Null);
        Assert.That(routines, Is.Empty);
    }

    [Test]
    public async Task CreateRoutine_ReturnsCreatedRoutine()
    {
        // Arrange
        var request = new CreateRoutineRequest
        {
            UserId = _userId,
            Name = "Morning Routine",
            Description = "My daily morning routine",
            TargetStartTime = new TimeSpan(6, 0, 0),
            EstimatedDurationMinutes = 60
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/routines", request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        var routine = await response.Content.ReadFromJsonAsync<RoutineDto>();
        Assert.That(routine, Is.Not.Null);
        Assert.That(routine.Name, Is.EqualTo("Morning Routine"));
        Assert.That(routine.EstimatedDurationMinutes, Is.EqualTo(60));
        Assert.That(routine.IsActive, Is.True);
    }

    [Test]
    public async Task GetRoutineById_ReturnsRoutine_WhenRoutineExists()
    {
        // Arrange
        var createRequest = new CreateRoutineRequest
        {
            UserId = _userId,
            Name = "Morning Routine",
            TargetStartTime = new TimeSpan(6, 0, 0),
            EstimatedDurationMinutes = 60
        };

        var createResponse = await _client.PostAsJsonAsync("/api/routines", createRequest);
        var createdRoutine = await createResponse.Content.ReadFromJsonAsync<RoutineDto>();

        // Act
        var response = await _client.GetAsync($"/api/routines/{createdRoutine!.RoutineId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var routine = await response.Content.ReadFromJsonAsync<RoutineDto>();
        Assert.That(routine, Is.Not.Null);
        Assert.That(routine.RoutineId, Is.EqualTo(createdRoutine.RoutineId));
    }

    [Test]
    public async Task UpdateRoutine_ReturnsUpdatedRoutine()
    {
        // Arrange
        var createRequest = new CreateRoutineRequest
        {
            UserId = _userId,
            Name = "Morning Routine",
            TargetStartTime = new TimeSpan(6, 0, 0),
            EstimatedDurationMinutes = 60
        };

        var createResponse = await _client.PostAsJsonAsync("/api/routines", createRequest);
        var createdRoutine = await createResponse.Content.ReadFromJsonAsync<RoutineDto>();

        var updateRequest = new UpdateRoutineRequest
        {
            Name = "Updated Morning Routine",
            TargetStartTime = new TimeSpan(7, 0, 0),
            EstimatedDurationMinutes = 90,
            IsActive = true
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/routines/{createdRoutine!.RoutineId}", updateRequest);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var routine = await response.Content.ReadFromJsonAsync<RoutineDto>();
        Assert.That(routine, Is.Not.Null);
        Assert.That(routine.Name, Is.EqualTo("Updated Morning Routine"));
        Assert.That(routine.EstimatedDurationMinutes, Is.EqualTo(90));
    }

    [Test]
    public async Task DeleteRoutine_ReturnsNoContent()
    {
        // Arrange
        var createRequest = new CreateRoutineRequest
        {
            UserId = _userId,
            Name = "Morning Routine",
            TargetStartTime = new TimeSpan(6, 0, 0),
            EstimatedDurationMinutes = 60
        };

        var createResponse = await _client.PostAsJsonAsync("/api/routines", createRequest);
        var createdRoutine = await createResponse.Content.ReadFromJsonAsync<RoutineDto>();

        // Act
        var response = await _client.DeleteAsync($"/api/routines/{createdRoutine!.RoutineId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}
