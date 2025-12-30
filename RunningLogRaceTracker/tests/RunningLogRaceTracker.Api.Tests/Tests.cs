using RunningLogRaceTracker.Api.Features.Run;
using RunningLogRaceTracker.Api.Features.Race;
using RunningLogRaceTracker.Api.Features.TrainingPlan;
using RunningLogRaceTracker.Core;
using RunningLogRaceTracker.Infrastructure;

namespace RunningLogRaceTracker.Api.Tests;

public class DtoMappingTests
{
    [Test]
    public void RunToDto_ShouldMapCorrectly()
    {
        // Arrange
        var run = new Run
        {
            RunId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Distance = 10.5m,
            DurationMinutes = 60,
            CompletedAt = DateTime.UtcNow,
            AveragePace = 5.71m,
            AverageHeartRate = 150,
            ElevationGain = 100,
            CaloriesBurned = 500,
            Route = "Test Route",
            Weather = "Sunny",
            Notes = "Great run",
            EffortRating = 7,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = run.ToDto();

        // Assert
        Assert.That(dto.RunId, Is.EqualTo(run.RunId));
        Assert.That(dto.UserId, Is.EqualTo(run.UserId));
        Assert.That(dto.Distance, Is.EqualTo(run.Distance));
        Assert.That(dto.DurationMinutes, Is.EqualTo(run.DurationMinutes));
        Assert.That(dto.CompletedAt, Is.EqualTo(run.CompletedAt));
        Assert.That(dto.AveragePace, Is.EqualTo(run.AveragePace));
        Assert.That(dto.AverageHeartRate, Is.EqualTo(run.AverageHeartRate));
        Assert.That(dto.ElevationGain, Is.EqualTo(run.ElevationGain));
        Assert.That(dto.CaloriesBurned, Is.EqualTo(run.CaloriesBurned));
        Assert.That(dto.Route, Is.EqualTo(run.Route));
        Assert.That(dto.Weather, Is.EqualTo(run.Weather));
        Assert.That(dto.Notes, Is.EqualTo(run.Notes));
        Assert.That(dto.EffortRating, Is.EqualTo(run.EffortRating));
        Assert.That(dto.CreatedAt, Is.EqualTo(run.CreatedAt));
    }

    [Test]
    public void RaceToDto_ShouldMapCorrectly()
    {
        // Arrange
        var race = new Race
        {
            RaceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Boston Marathon",
            RaceType = RaceType.Marathon,
            RaceDate = DateTime.UtcNow.AddDays(30),
            Location = "Boston, MA",
            Distance = 42.2m,
            FinishTimeMinutes = 240,
            GoalTimeMinutes = 250,
            Placement = 100,
            IsCompleted = true,
            Notes = "Personal best!",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = race.ToDto();

        // Assert
        Assert.That(dto.RaceId, Is.EqualTo(race.RaceId));
        Assert.That(dto.UserId, Is.EqualTo(race.UserId));
        Assert.That(dto.Name, Is.EqualTo(race.Name));
        Assert.That(dto.RaceType, Is.EqualTo(race.RaceType));
        Assert.That(dto.RaceDate, Is.EqualTo(race.RaceDate));
        Assert.That(dto.Location, Is.EqualTo(race.Location));
        Assert.That(dto.Distance, Is.EqualTo(race.Distance));
        Assert.That(dto.FinishTimeMinutes, Is.EqualTo(race.FinishTimeMinutes));
        Assert.That(dto.GoalTimeMinutes, Is.EqualTo(race.GoalTimeMinutes));
        Assert.That(dto.Placement, Is.EqualTo(race.Placement));
        Assert.That(dto.IsCompleted, Is.EqualTo(race.IsCompleted));
        Assert.That(dto.Notes, Is.EqualTo(race.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(race.CreatedAt));
    }

    [Test]
    public void TrainingPlanToDto_ShouldMapCorrectly()
    {
        // Arrange
        var trainingPlan = new TrainingPlan
        {
            TrainingPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Marathon Training Plan",
            RaceId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(90),
            WeeklyMileageGoal = 50m,
            PlanDetails = "12-week marathon plan",
            IsActive = true,
            Notes = "Progressive overload",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = trainingPlan.ToDto();

        // Assert
        Assert.That(dto.TrainingPlanId, Is.EqualTo(trainingPlan.TrainingPlanId));
        Assert.That(dto.UserId, Is.EqualTo(trainingPlan.UserId));
        Assert.That(dto.Name, Is.EqualTo(trainingPlan.Name));
        Assert.That(dto.RaceId, Is.EqualTo(trainingPlan.RaceId));
        Assert.That(dto.StartDate, Is.EqualTo(trainingPlan.StartDate));
        Assert.That(dto.EndDate, Is.EqualTo(trainingPlan.EndDate));
        Assert.That(dto.WeeklyMileageGoal, Is.EqualTo(trainingPlan.WeeklyMileageGoal));
        Assert.That(dto.PlanDetails, Is.EqualTo(trainingPlan.PlanDetails));
        Assert.That(dto.IsActive, Is.EqualTo(trainingPlan.IsActive));
        Assert.That(dto.Notes, Is.EqualTo(trainingPlan.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(trainingPlan.CreatedAt));
    }
}

public class RunFeatureTests
{
    private RunningLogRaceTrackerContext _context = null!;
    private Guid _testDatabaseId;

    [SetUp]
    public void Setup()
    {
        _testDatabaseId = Guid.NewGuid();
        var options = TestHelpers.CreateInMemoryDbContextOptions<RunningLogRaceTrackerContext>(_testDatabaseId.ToString());
        _context = new RunningLogRaceTrackerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateRunCommand_ShouldCreateRun()
    {
        // Arrange
        var handler = new CreateRunCommandHandler(_context);
        var command = new CreateRunCommand(
            Guid.NewGuid(),
            10.5m,
            60,
            DateTime.UtcNow,
            5.71m,
            150,
            100,
            500,
            "Test Route",
            "Sunny",
            "Great run",
            7);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.Distance, Is.EqualTo(command.Distance));
        Assert.That(result.DurationMinutes, Is.EqualTo(command.DurationMinutes));
        Assert.That(result.AveragePace, Is.EqualTo(command.AveragePace));

        var savedRun = await _context.Runs.FindAsync(result.RunId);
        Assert.That(savedRun, Is.Not.Null);
    }

    [Test]
    public async Task GetRunsQuery_ShouldReturnAllRuns()
    {
        // Arrange
        var run1 = new Run
        {
            RunId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Distance = 10.5m,
            DurationMinutes = 60,
            CompletedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        var run2 = new Run
        {
            RunId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Distance = 5.0m,
            DurationMinutes = 30,
            CompletedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Runs.AddRange(run1, run2);
        await _context.SaveChangesAsync();

        var handler = new GetRunsQueryHandler(_context);
        var query = new GetRunsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetRunByIdQuery_ShouldReturnRun()
    {
        // Arrange
        var run = new Run
        {
            RunId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Distance = 10.5m,
            DurationMinutes = 60,
            CompletedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Runs.Add(run);
        await _context.SaveChangesAsync();

        var handler = new GetRunByIdQueryHandler(_context);
        var query = new GetRunByIdQuery(run.RunId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.RunId, Is.EqualTo(run.RunId));
    }

    [Test]
    public void GetRunByIdQuery_ShouldThrowWhenNotFound()
    {
        // Arrange
        var handler = new GetRunByIdQueryHandler(_context);
        var query = new GetRunByIdQuery(Guid.NewGuid());

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }

    [Test]
    public async Task UpdateRunCommand_ShouldUpdateRun()
    {
        // Arrange
        var run = new Run
        {
            RunId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Distance = 10.5m,
            DurationMinutes = 60,
            CompletedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Runs.Add(run);
        await _context.SaveChangesAsync();

        var handler = new UpdateRunCommandHandler(_context);
        var command = new UpdateRunCommand(
            run.RunId,
            run.UserId,
            15.0m,
            90,
            DateTime.UtcNow,
            6.0m,
            160,
            150,
            600,
            "Updated Route",
            "Rainy",
            "Updated notes",
            8);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Distance, Is.EqualTo(15.0m));
        Assert.That(result.DurationMinutes, Is.EqualTo(90));
        Assert.That(result.Route, Is.EqualTo("Updated Route"));
    }

    [Test]
    public async Task DeleteRunCommand_ShouldDeleteRun()
    {
        // Arrange
        var run = new Run
        {
            RunId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Distance = 10.5m,
            DurationMinutes = 60,
            CompletedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Runs.Add(run);
        await _context.SaveChangesAsync();

        var handler = new DeleteRunCommandHandler(_context);
        var command = new DeleteRunCommand(run.RunId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedRun = await _context.Runs.FindAsync(run.RunId);
        Assert.That(deletedRun, Is.Null);
    }
}

public class RaceFeatureTests
{
    private RunningLogRaceTrackerContext _context = null!;
    private Guid _testDatabaseId;

    [SetUp]
    public void Setup()
    {
        _testDatabaseId = Guid.NewGuid();
        var options = TestHelpers.CreateInMemoryDbContextOptions<RunningLogRaceTrackerContext>(_testDatabaseId.ToString());
        _context = new RunningLogRaceTrackerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateRaceCommand_ShouldCreateRace()
    {
        // Arrange
        var handler = new CreateRaceCommandHandler(_context);
        var command = new CreateRaceCommand(
            Guid.NewGuid(),
            "Boston Marathon",
            RaceType.Marathon,
            DateTime.UtcNow.AddDays(30),
            "Boston, MA",
            42.2m,
            240,
            250,
            100,
            false,
            "Upcoming race");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.RaceType, Is.EqualTo(command.RaceType));
        Assert.That(result.Location, Is.EqualTo(command.Location));

        var savedRace = await _context.Races.FindAsync(result.RaceId);
        Assert.That(savedRace, Is.Not.Null);
    }

    [Test]
    public async Task GetRacesQuery_ShouldReturnAllRaces()
    {
        // Arrange
        var race1 = new Race
        {
            RaceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Boston Marathon",
            RaceType = RaceType.Marathon,
            RaceDate = DateTime.UtcNow.AddDays(30),
            Location = "Boston, MA",
            Distance = 42.2m,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };
        var race2 = new Race
        {
            RaceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "5K Fun Run",
            RaceType = RaceType.FiveK,
            RaceDate = DateTime.UtcNow.AddDays(15),
            Location = "Local Park",
            Distance = 5.0m,
            IsCompleted = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Races.AddRange(race1, race2);
        await _context.SaveChangesAsync();

        var handler = new GetRacesQueryHandler(_context);
        var query = new GetRacesQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetRaceByIdQuery_ShouldReturnRace()
    {
        // Arrange
        var race = new Race
        {
            RaceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Boston Marathon",
            RaceType = RaceType.Marathon,
            RaceDate = DateTime.UtcNow.AddDays(30),
            Location = "Boston, MA",
            Distance = 42.2m,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Races.Add(race);
        await _context.SaveChangesAsync();

        var handler = new GetRaceByIdQueryHandler(_context);
        var query = new GetRaceByIdQuery(race.RaceId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.RaceId, Is.EqualTo(race.RaceId));
        Assert.That(result.Name, Is.EqualTo(race.Name));
    }

    [Test]
    public void GetRaceByIdQuery_ShouldThrowWhenNotFound()
    {
        // Arrange
        var handler = new GetRaceByIdQueryHandler(_context);
        var query = new GetRaceByIdQuery(Guid.NewGuid());

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }

    [Test]
    public async Task UpdateRaceCommand_ShouldUpdateRace()
    {
        // Arrange
        var race = new Race
        {
            RaceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Boston Marathon",
            RaceType = RaceType.Marathon,
            RaceDate = DateTime.UtcNow.AddDays(30),
            Location = "Boston, MA",
            Distance = 42.2m,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Races.Add(race);
        await _context.SaveChangesAsync();

        var handler = new UpdateRaceCommandHandler(_context);
        var command = new UpdateRaceCommand(
            race.RaceId,
            race.UserId,
            "Updated Marathon",
            RaceType.Marathon,
            DateTime.UtcNow.AddDays(45),
            "New Location",
            42.2m,
            235,
            250,
            95,
            true,
            "Completed successfully");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Name, Is.EqualTo("Updated Marathon"));
        Assert.That(result.Location, Is.EqualTo("New Location"));
        Assert.That(result.IsCompleted, Is.True);
    }

    [Test]
    public async Task DeleteRaceCommand_ShouldDeleteRace()
    {
        // Arrange
        var race = new Race
        {
            RaceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Boston Marathon",
            RaceType = RaceType.Marathon,
            RaceDate = DateTime.UtcNow.AddDays(30),
            Location = "Boston, MA",
            Distance = 42.2m,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Races.Add(race);
        await _context.SaveChangesAsync();

        var handler = new DeleteRaceCommandHandler(_context);
        var command = new DeleteRaceCommand(race.RaceId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedRace = await _context.Races.FindAsync(race.RaceId);
        Assert.That(deletedRace, Is.Null);
    }
}

public class TrainingPlanFeatureTests
{
    private RunningLogRaceTrackerContext _context = null!;
    private Guid _testDatabaseId;

    [SetUp]
    public void Setup()
    {
        _testDatabaseId = Guid.NewGuid();
        var options = TestHelpers.CreateInMemoryDbContextOptions<RunningLogRaceTrackerContext>(_testDatabaseId.ToString());
        _context = new RunningLogRaceTrackerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateTrainingPlanCommand_ShouldCreateTrainingPlan()
    {
        // Arrange
        var handler = new CreateTrainingPlanCommandHandler(_context);
        var command = new CreateTrainingPlanCommand(
            Guid.NewGuid(),
            "Marathon Training Plan",
            Guid.NewGuid(),
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(90),
            50m,
            "12-week plan",
            true,
            "Progressive overload");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.WeeklyMileageGoal, Is.EqualTo(command.WeeklyMileageGoal));
        Assert.That(result.IsActive, Is.EqualTo(command.IsActive));

        var savedPlan = await _context.TrainingPlans.FindAsync(result.TrainingPlanId);
        Assert.That(savedPlan, Is.Not.Null);
    }

    [Test]
    public async Task GetTrainingPlansQuery_ShouldReturnAllTrainingPlans()
    {
        // Arrange
        var plan1 = new TrainingPlan
        {
            TrainingPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Marathon Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(90),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        var plan2 = new TrainingPlan
        {
            TrainingPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "5K Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30),
            IsActive = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.TrainingPlans.AddRange(plan1, plan2);
        await _context.SaveChangesAsync();

        var handler = new GetTrainingPlansQueryHandler(_context);
        var query = new GetTrainingPlansQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetTrainingPlanByIdQuery_ShouldReturnTrainingPlan()
    {
        // Arrange
        var plan = new TrainingPlan
        {
            TrainingPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Marathon Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(90),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.TrainingPlans.Add(plan);
        await _context.SaveChangesAsync();

        var handler = new GetTrainingPlanByIdQueryHandler(_context);
        var query = new GetTrainingPlanByIdQuery(plan.TrainingPlanId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TrainingPlanId, Is.EqualTo(plan.TrainingPlanId));
        Assert.That(result.Name, Is.EqualTo(plan.Name));
    }

    [Test]
    public void GetTrainingPlanByIdQuery_ShouldThrowWhenNotFound()
    {
        // Arrange
        var handler = new GetTrainingPlanByIdQueryHandler(_context);
        var query = new GetTrainingPlanByIdQuery(Guid.NewGuid());

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }

    [Test]
    public async Task UpdateTrainingPlanCommand_ShouldUpdateTrainingPlan()
    {
        // Arrange
        var plan = new TrainingPlan
        {
            TrainingPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Marathon Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(90),
            WeeklyMileageGoal = 50m,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.TrainingPlans.Add(plan);
        await _context.SaveChangesAsync();

        var handler = new UpdateTrainingPlanCommandHandler(_context);
        var command = new UpdateTrainingPlanCommand(
            plan.TrainingPlanId,
            plan.UserId,
            "Updated Marathon Plan",
            Guid.NewGuid(),
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(120),
            60m,
            "Updated plan details",
            false,
            "Updated notes");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Name, Is.EqualTo("Updated Marathon Plan"));
        Assert.That(result.WeeklyMileageGoal, Is.EqualTo(60m));
        Assert.That(result.IsActive, Is.False);
    }

    [Test]
    public async Task DeleteTrainingPlanCommand_ShouldDeleteTrainingPlan()
    {
        // Arrange
        var plan = new TrainingPlan
        {
            TrainingPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Marathon Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(90),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.TrainingPlans.Add(plan);
        await _context.SaveChangesAsync();

        var handler = new DeleteTrainingPlanCommandHandler(_context);
        var command = new DeleteTrainingPlanCommand(plan.TrainingPlanId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedPlan = await _context.TrainingPlans.FindAsync(plan.TrainingPlanId);
        Assert.That(deletedPlan, Is.Null);
    }
}
