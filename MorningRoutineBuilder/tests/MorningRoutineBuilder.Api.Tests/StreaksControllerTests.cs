// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using System.Net.Http.Json;
using MorningRoutineBuilder.Api.Features.Routines;
using MorningRoutineBuilder.Api.Features.Streaks;

namespace MorningRoutineBuilder.Api.Tests;

[TestFixture]
public class StreaksControllerTests
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
    public async Task GetStreaks_ReturnsEmptyList_WhenNoStreaksExist()
    {
        // Act
        var response = await _client.GetAsync("/api/streaks");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var streaks = await response.Content.ReadFromJsonAsync<List<StreakDto>>();
        Assert.That(streaks, Is.Not.Null);
        Assert.That(streaks, Is.Empty);
    }

    [Test]
    public async Task CreateStreak_ReturnsCreatedStreak()
    {
        // Arrange
        var request = new CreateStreakRequest
        {
            RoutineId = _routineId,
            UserId = _userId
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/streaks", request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        var streak = await response.Content.ReadFromJsonAsync<StreakDto>();
        Assert.That(streak, Is.Not.Null);
        Assert.That(streak.RoutineId, Is.EqualTo(_routineId));
        Assert.That(streak.CurrentStreak, Is.EqualTo(0));
        Assert.That(streak.IsActive, Is.True);
    }

    [Test]
    public async Task GetStreakById_ReturnsStreak_WhenStreakExists()
    {
        // Arrange
        var createRequest = new CreateStreakRequest
        {
            RoutineId = _routineId,
            UserId = _userId
        };

        var createResponse = await _client.PostAsJsonAsync("/api/streaks", createRequest);
        var createdStreak = await createResponse.Content.ReadFromJsonAsync<StreakDto>();

        // Act
        var response = await _client.GetAsync($"/api/streaks/{createdStreak!.StreakId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var streak = await response.Content.ReadFromJsonAsync<StreakDto>();
        Assert.That(streak, Is.Not.Null);
        Assert.That(streak.StreakId, Is.EqualTo(createdStreak.StreakId));
    }

    [Test]
    public async Task UpdateStreak_ReturnsUpdatedStreak()
    {
        // Arrange
        var createRequest = new CreateStreakRequest
        {
            RoutineId = _routineId,
            UserId = _userId
        };

        var createResponse = await _client.PostAsJsonAsync("/api/streaks", createRequest);
        var createdStreak = await createResponse.Content.ReadFromJsonAsync<StreakDto>();

        var updateRequest = new UpdateStreakRequest
        {
            CurrentStreak = 5,
            LongestStreak = 10,
            LastCompletionDate = DateTime.UtcNow,
            StreakStartDate = DateTime.UtcNow.AddDays(-5),
            IsActive = true
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/streaks/{createdStreak!.StreakId}", updateRequest);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var streak = await response.Content.ReadFromJsonAsync<StreakDto>();
        Assert.That(streak, Is.Not.Null);
        Assert.That(streak.CurrentStreak, Is.EqualTo(5));
        Assert.That(streak.LongestStreak, Is.EqualTo(10));
    }

    [Test]
    public async Task DeleteStreak_ReturnsNoContent()
    {
        // Arrange
        var createRequest = new CreateStreakRequest
        {
            RoutineId = _routineId,
            UserId = _userId
        };

        var createResponse = await _client.PostAsJsonAsync("/api/streaks", createRequest);
        var createdStreak = await createResponse.Content.ReadFromJsonAsync<StreakDto>();

        // Act
        var response = await _client.DeleteAsync($"/api/streaks/{createdStreak!.StreakId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}
