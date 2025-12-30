using HydrationTracker.Api.Features.Intake;
using HydrationTracker.Api.Features.Goal;
using HydrationTracker.Api.Features.Reminder;
using HydrationTracker.Core;
using HydrationTracker.Infrastructure;

namespace HydrationTracker.Api.Tests;

public class DtoMappingTests
{
    [Test]
    public void IntakeToDto_ShouldMapCorrectly()
    {
        // Arrange
        var intake = new Intake
        {
            IntakeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BeverageType = BeverageType.Water,
            AmountMl = 500m,
            IntakeTime = DateTime.UtcNow,
            Notes = "Test intake",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = intake.ToDto();

        // Assert
        Assert.That(dto.IntakeId, Is.EqualTo(intake.IntakeId));
        Assert.That(dto.UserId, Is.EqualTo(intake.UserId));
        Assert.That(dto.BeverageType, Is.EqualTo(intake.BeverageType));
        Assert.That(dto.AmountMl, Is.EqualTo(intake.AmountMl));
        Assert.That(dto.IntakeTime, Is.EqualTo(intake.IntakeTime));
        Assert.That(dto.Notes, Is.EqualTo(intake.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(intake.CreatedAt));
    }

    [Test]
    public void GoalToDto_ShouldMapCorrectly()
    {
        // Arrange
        var goal = new Core.Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DailyGoalMl = 2000m,
            StartDate = DateTime.UtcNow,
            IsActive = true,
            Notes = "Test goal",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = goal.ToDto();

        // Assert
        Assert.That(dto.GoalId, Is.EqualTo(goal.GoalId));
        Assert.That(dto.UserId, Is.EqualTo(goal.UserId));
        Assert.That(dto.DailyGoalMl, Is.EqualTo(goal.DailyGoalMl));
        Assert.That(dto.StartDate, Is.EqualTo(goal.StartDate));
        Assert.That(dto.IsActive, Is.EqualTo(goal.IsActive));
        Assert.That(dto.Notes, Is.EqualTo(goal.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(goal.CreatedAt));
    }

    [Test]
    public void ReminderToDto_ShouldMapCorrectly()
    {
        // Arrange
        var reminder = new Core.Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderTime = TimeSpan.FromHours(8),
            Message = "Test reminder",
            IsEnabled = true,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = reminder.ToDto();

        // Assert
        Assert.That(dto.ReminderId, Is.EqualTo(reminder.ReminderId));
        Assert.That(dto.UserId, Is.EqualTo(reminder.UserId));
        Assert.That(dto.ReminderTime, Is.EqualTo(reminder.ReminderTime));
        Assert.That(dto.Message, Is.EqualTo(reminder.Message));
        Assert.That(dto.IsEnabled, Is.EqualTo(reminder.IsEnabled));
        Assert.That(dto.CreatedAt, Is.EqualTo(reminder.CreatedAt));
    }
}

public class IntakeFeatureTests
{
    private HydrationTrackerContext _context = null!;
    private Guid _testDatabaseId;

    [SetUp]
    public void Setup()
    {
        _testDatabaseId = Guid.NewGuid();
        var options = TestHelpers.CreateInMemoryDbContextOptions<HydrationTrackerContext>(_testDatabaseId.ToString());
        _context = new HydrationTrackerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateIntakeCommand_ShouldCreateIntake()
    {
        // Arrange
        var handler = new CreateIntakeCommandHandler(_context);
        var command = new CreateIntakeCommand(
            Guid.NewGuid(),
            BeverageType.Water,
            500m,
            DateTime.UtcNow,
            "Test intake");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.BeverageType, Is.EqualTo(command.BeverageType));
        Assert.That(result.AmountMl, Is.EqualTo(command.AmountMl));
        Assert.That(result.Notes, Is.EqualTo(command.Notes));

        var savedIntake = await _context.Intakes.FindAsync(result.IntakeId);
        Assert.That(savedIntake, Is.Not.Null);
    }

    [Test]
    public async Task GetIntakesQuery_ShouldReturnAllIntakes()
    {
        // Arrange
        var intake1 = new Intake
        {
            IntakeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BeverageType = BeverageType.Water,
            AmountMl = 500m,
            IntakeTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        var intake2 = new Intake
        {
            IntakeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BeverageType = BeverageType.Coffee,
            AmountMl = 300m,
            IntakeTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Intakes.AddRange(intake1, intake2);
        await _context.SaveChangesAsync();

        var handler = new GetIntakesQueryHandler(_context);
        var query = new GetIntakesQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetIntakeByIdQuery_ShouldReturnIntake()
    {
        // Arrange
        var intake = new Intake
        {
            IntakeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BeverageType = BeverageType.Water,
            AmountMl = 500m,
            IntakeTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Intakes.Add(intake);
        await _context.SaveChangesAsync();

        var handler = new GetIntakeByIdQueryHandler(_context);
        var query = new GetIntakeByIdQuery(intake.IntakeId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.IntakeId, Is.EqualTo(intake.IntakeId));
    }

    [Test]
    public void GetIntakeByIdQuery_ShouldThrowWhenNotFound()
    {
        // Arrange
        var handler = new GetIntakeByIdQueryHandler(_context);
        var query = new GetIntakeByIdQuery(Guid.NewGuid());

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }

    [Test]
    public async Task UpdateIntakeCommand_ShouldUpdateIntake()
    {
        // Arrange
        var intake = new Intake
        {
            IntakeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BeverageType = BeverageType.Water,
            AmountMl = 500m,
            IntakeTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Intakes.Add(intake);
        await _context.SaveChangesAsync();

        var handler = new UpdateIntakeCommandHandler(_context);
        var command = new UpdateIntakeCommand(
            intake.IntakeId,
            intake.UserId,
            BeverageType.Coffee,
            600m,
            DateTime.UtcNow,
            "Updated");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.BeverageType, Is.EqualTo(BeverageType.Coffee));
        Assert.That(result.AmountMl, Is.EqualTo(600m));
        Assert.That(result.Notes, Is.EqualTo("Updated"));
    }

    [Test]
    public async Task DeleteIntakeCommand_ShouldDeleteIntake()
    {
        // Arrange
        var intake = new Intake
        {
            IntakeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BeverageType = BeverageType.Water,
            AmountMl = 500m,
            IntakeTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Intakes.Add(intake);
        await _context.SaveChangesAsync();

        var handler = new DeleteIntakeCommandHandler(_context);
        var command = new DeleteIntakeCommand(intake.IntakeId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedIntake = await _context.Intakes.FindAsync(intake.IntakeId);
        Assert.That(deletedIntake, Is.Null);
    }
}

public class GoalFeatureTests
{
    private HydrationTrackerContext _context = null!;
    private Guid _testDatabaseId;

    [SetUp]
    public void Setup()
    {
        _testDatabaseId = Guid.NewGuid();
        var options = TestHelpers.CreateInMemoryDbContextOptions<HydrationTrackerContext>(_testDatabaseId.ToString());
        _context = new HydrationTrackerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateGoalCommand_ShouldCreateGoal()
    {
        // Arrange
        var handler = new CreateGoalCommandHandler(_context);
        var command = new CreateGoalCommand(
            Guid.NewGuid(),
            2000m,
            DateTime.UtcNow,
            true,
            "Test goal");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.DailyGoalMl, Is.EqualTo(command.DailyGoalMl));
        Assert.That(result.IsActive, Is.EqualTo(command.IsActive));

        var savedGoal = await _context.Goals.FindAsync(result.GoalId);
        Assert.That(savedGoal, Is.Not.Null);
    }

    [Test]
    public async Task GetGoalsQuery_ShouldReturnAllGoals()
    {
        // Arrange
        var goal1 = new Core.Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DailyGoalMl = 2000m,
            StartDate = DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        var goal2 = new Core.Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DailyGoalMl = 2500m,
            StartDate = DateTime.UtcNow,
            IsActive = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Goals.AddRange(goal1, goal2);
        await _context.SaveChangesAsync();

        var handler = new GetGoalsQueryHandler(_context);
        var query = new GetGoalsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task UpdateGoalCommand_ShouldUpdateGoal()
    {
        // Arrange
        var goal = new Core.Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DailyGoalMl = 2000m,
            StartDate = DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        var handler = new UpdateGoalCommandHandler(_context);
        var command = new UpdateGoalCommand(
            goal.GoalId,
            goal.UserId,
            2500m,
            DateTime.UtcNow,
            false,
            "Updated goal");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.DailyGoalMl, Is.EqualTo(2500m));
        Assert.That(result.IsActive, Is.False);
        Assert.That(result.Notes, Is.EqualTo("Updated goal"));
    }

    [Test]
    public async Task DeleteGoalCommand_ShouldDeleteGoal()
    {
        // Arrange
        var goal = new Core.Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DailyGoalMl = 2000m,
            StartDate = DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        var handler = new DeleteGoalCommandHandler(_context);
        var command = new DeleteGoalCommand(goal.GoalId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedGoal = await _context.Goals.FindAsync(goal.GoalId);
        Assert.That(deletedGoal, Is.Null);
    }
}

public class ReminderFeatureTests
{
    private HydrationTrackerContext _context = null!;
    private Guid _testDatabaseId;

    [SetUp]
    public void Setup()
    {
        _testDatabaseId = Guid.NewGuid();
        var options = TestHelpers.CreateInMemoryDbContextOptions<HydrationTrackerContext>(_testDatabaseId.ToString());
        _context = new HydrationTrackerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateReminderCommand_ShouldCreateReminder()
    {
        // Arrange
        var handler = new CreateReminderCommandHandler(_context);
        var command = new CreateReminderCommand(
            Guid.NewGuid(),
            TimeSpan.FromHours(8),
            "Test reminder",
            true);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.ReminderTime, Is.EqualTo(command.ReminderTime));
        Assert.That(result.Message, Is.EqualTo(command.Message));
        Assert.That(result.IsEnabled, Is.EqualTo(command.IsEnabled));

        var savedReminder = await _context.Reminders.FindAsync(result.ReminderId);
        Assert.That(savedReminder, Is.Not.Null);
    }

    [Test]
    public async Task GetRemindersQuery_ShouldReturnAllReminders()
    {
        // Arrange
        var reminder1 = new Core.Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderTime = TimeSpan.FromHours(8),
            Message = "Morning reminder",
            IsEnabled = true,
            CreatedAt = DateTime.UtcNow
        };
        var reminder2 = new Core.Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderTime = TimeSpan.FromHours(20),
            Message = "Evening reminder",
            IsEnabled = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reminders.AddRange(reminder1, reminder2);
        await _context.SaveChangesAsync();

        var handler = new GetRemindersQueryHandler(_context);
        var query = new GetRemindersQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task UpdateReminderCommand_ShouldUpdateReminder()
    {
        // Arrange
        var reminder = new Core.Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderTime = TimeSpan.FromHours(8),
            Message = "Original message",
            IsEnabled = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        var handler = new UpdateReminderCommandHandler(_context);
        var command = new UpdateReminderCommand(
            reminder.ReminderId,
            reminder.UserId,
            TimeSpan.FromHours(12),
            "Updated message",
            false);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.ReminderTime, Is.EqualTo(TimeSpan.FromHours(12)));
        Assert.That(result.Message, Is.EqualTo("Updated message"));
        Assert.That(result.IsEnabled, Is.False);
    }

    [Test]
    public async Task DeleteReminderCommand_ShouldDeleteReminder()
    {
        // Arrange
        var reminder = new Core.Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ReminderTime = TimeSpan.FromHours(8),
            Message = "Test reminder",
            IsEnabled = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        var handler = new DeleteReminderCommandHandler(_context);
        var command = new DeleteReminderCommand(reminder.ReminderId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedReminder = await _context.Reminders.FindAsync(reminder.ReminderId);
        Assert.That(deletedReminder, Is.Null);
    }
}
