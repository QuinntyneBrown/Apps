// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using System.Net.Http.Json;
using MorningRoutineBuilder.Api.Features.Routines;
using MorningRoutineBuilder.Api.Features.RoutineTasks;
using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Tests;

[TestFixture]
public class RoutineTasksControllerTests
{
    private MorningRoutineBuilderWebApplicationFactory _factory = null!;
    private HttpClient _client = null!;
    private Guid _routineId;

    [SetUp]
    public async Task Setup()
    {
        _factory = new MorningRoutineBuilderWebApplicationFactory();
        _client = _factory.CreateClient();

        // Create a routine for testing
        var routineRequest = new CreateRoutineRequest
        {
            UserId = Guid.NewGuid(),
            Name = "Morning Routine",
            TargetStartTime = new TimeSpan(6, 0, 0),
            EstimatedDurationMinutes = 60
        };

        var routineResponse = await _client.PostAsJsonAsync("/api/routines", routineRequest);
        var routine = await routineResponse.Content.ReadFromJsonAsync<RoutineDto>();
        _routineId = routine!.RoutineId;
    }

    [TearDown]
    public void TearDown()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }

    [Test]
    public async Task GetRoutineTasks_ReturnsEmptyList_WhenNoTasksExist()
    {
        // Act
        var response = await _client.GetAsync("/api/routinetasks");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var tasks = await response.Content.ReadFromJsonAsync<List<RoutineTaskDto>>();
        Assert.That(tasks, Is.Not.Null);
        Assert.That(tasks, Is.Empty);
    }

    [Test]
    public async Task CreateRoutineTask_ReturnsCreatedTask()
    {
        // Arrange
        var request = new CreateRoutineTaskRequest
        {
            RoutineId = _routineId,
            Name = "Meditation",
            TaskType = TaskType.Meditation,
            Description = "10 minutes of meditation",
            EstimatedDurationMinutes = 10,
            SortOrder = 1,
            IsOptional = false
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/routinetasks", request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        var task = await response.Content.ReadFromJsonAsync<RoutineTaskDto>();
        Assert.That(task, Is.Not.Null);
        Assert.That(task.Name, Is.EqualTo("Meditation"));
        Assert.That(task.TaskType, Is.EqualTo(TaskType.Meditation));
        Assert.That(task.IsOptional, Is.False);
    }

    [Test]
    public async Task GetRoutineTaskById_ReturnsTask_WhenTaskExists()
    {
        // Arrange
        var createRequest = new CreateRoutineTaskRequest
        {
            RoutineId = _routineId,
            Name = "Exercise",
            TaskType = TaskType.Exercise,
            EstimatedDurationMinutes = 30,
            SortOrder = 1,
            IsOptional = false
        };

        var createResponse = await _client.PostAsJsonAsync("/api/routinetasks", createRequest);
        var createdTask = await createResponse.Content.ReadFromJsonAsync<RoutineTaskDto>();

        // Act
        var response = await _client.GetAsync($"/api/routinetasks/{createdTask!.RoutineTaskId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var task = await response.Content.ReadFromJsonAsync<RoutineTaskDto>();
        Assert.That(task, Is.Not.Null);
        Assert.That(task.RoutineTaskId, Is.EqualTo(createdTask.RoutineTaskId));
    }

    [Test]
    public async Task DeleteRoutineTask_ReturnsNoContent()
    {
        // Arrange
        var createRequest = new CreateRoutineTaskRequest
        {
            RoutineId = _routineId,
            Name = "Reading",
            TaskType = TaskType.Reading,
            EstimatedDurationMinutes = 15,
            SortOrder = 1,
            IsOptional = true
        };

        var createResponse = await _client.PostAsJsonAsync("/api/routinetasks", createRequest);
        var createdTask = await createResponse.Content.ReadFromJsonAsync<RoutineTaskDto>();

        // Act
        var response = await _client.DeleteAsync($"/api/routinetasks/{createdTask!.RoutineTaskId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}
