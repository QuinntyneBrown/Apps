// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using System.Net.Http.Json;
using MorningRoutineBuilder.Api.Features.CompletionLogs;
using MorningRoutineBuilder.Api.Features.Routines;

namespace MorningRoutineBuilder.Api.Tests;

[TestFixture]
public class CompletionLogsControllerTests
{
    private MorningRoutineBuilderWebApplicationFactory _factory = null!;
    private HttpClient _client = null!;
    private Guid _routineId;
    private Guid _userId;

    [SetUp]
    public async Task Setup()
    {
        _factory = new MorningRoutineBuilderWebApplicationFactory();
        _client = _factory.CreateClient();
        _userId = Guid.NewGuid();

        // Create a routine for testing
        var routineRequest = new CreateRoutineRequest
        {
            UserId = _userId,
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
    public async Task GetCompletionLogs_ReturnsEmptyList_WhenNoLogsExist()
    {
        // Act
        var response = await _client.GetAsync("/api/completionlogs");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var logs = await response.Content.ReadFromJsonAsync<List<CompletionLogDto>>();
        Assert.That(logs, Is.Not.Null);
        Assert.That(logs, Is.Empty);
    }

    [Test]
    public async Task CreateCompletionLog_ReturnsCreatedLog()
    {
        // Arrange
        var request = new CreateCompletionLogRequest
        {
            RoutineId = _routineId,
            UserId = _userId,
            CompletionDate = DateTime.UtcNow,
            ActualStartTime = DateTime.UtcNow.AddHours(-1),
            ActualEndTime = DateTime.UtcNow,
            TasksCompleted = 5,
            TotalTasks = 5,
            Notes = "Completed all tasks",
            MoodRating = 9
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/completionlogs", request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        var log = await response.Content.ReadFromJsonAsync<CompletionLogDto>();
        Assert.That(log, Is.Not.Null);
        Assert.That(log.TasksCompleted, Is.EqualTo(5));
        Assert.That(log.MoodRating, Is.EqualTo(9));
    }

    [Test]
    public async Task GetCompletionLogById_ReturnsLog_WhenLogExists()
    {
        // Arrange
        var createRequest = new CreateCompletionLogRequest
        {
            RoutineId = _routineId,
            UserId = _userId,
            CompletionDate = DateTime.UtcNow,
            TasksCompleted = 3,
            TotalTasks = 5
        };

        var createResponse = await _client.PostAsJsonAsync("/api/completionlogs", createRequest);
        var createdLog = await createResponse.Content.ReadFromJsonAsync<CompletionLogDto>();

        // Act
        var response = await _client.GetAsync($"/api/completionlogs/{createdLog!.CompletionLogId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var log = await response.Content.ReadFromJsonAsync<CompletionLogDto>();
        Assert.That(log, Is.Not.Null);
        Assert.That(log.CompletionLogId, Is.EqualTo(createdLog.CompletionLogId));
    }

    [Test]
    public async Task DeleteCompletionLog_ReturnsNoContent()
    {
        // Arrange
        var createRequest = new CreateCompletionLogRequest
        {
            RoutineId = _routineId,
            UserId = _userId,
            CompletionDate = DateTime.UtcNow,
            TasksCompleted = 4,
            TotalTasks = 5
        };

        var createResponse = await _client.PostAsJsonAsync("/api/completionlogs", createRequest);
        var createdLog = await createResponse.Content.ReadFromJsonAsync<CompletionLogDto>();

        // Act
        var response = await _client.DeleteAsync($"/api/completionlogs/{createdLog!.CompletionLogId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}
