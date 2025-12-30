// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MorningRoutineBuilder.Core.Tests;

public class CompletionLogTests
{
    [Test]
    public void GetCompletionPercentage_ValidValues_ReturnsCorrectPercentage()
    {
        var log = new CompletionLog { CompletionLogId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid(), CompletionDate = DateTime.UtcNow, TasksCompleted = 8, TotalTasks = 10 };
        Assert.That(log.GetCompletionPercentage(), Is.EqualTo(80.0));
    }

    [Test]
    public void GetCompletionPercentage_ZeroTotalTasks_ReturnsZero()
    {
        var log = new CompletionLog { CompletionLogId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid(), CompletionDate = DateTime.UtcNow, TasksCompleted = 5, TotalTasks = 0 };
        Assert.That(log.GetCompletionPercentage(), Is.EqualTo(0));
    }

    [Test]
    public void IsFullyCompleted_AllTasksCompleted_ReturnsTrue()
    {
        var log = new CompletionLog { CompletionLogId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid(), CompletionDate = DateTime.UtcNow, TasksCompleted = 10, TotalTasks = 10 };
        Assert.That(log.IsFullyCompleted(), Is.True);
    }

    [Test]
    public void IsFullyCompleted_SomeTasksIncomplete_ReturnsFalse()
    {
        var log = new CompletionLog { CompletionLogId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid(), CompletionDate = DateTime.UtcNow, TasksCompleted = 8, TotalTasks = 10 };
        Assert.That(log.IsFullyCompleted(), Is.False);
    }

    [Test]
    public void GetActualDurationMinutes_ValidTimes_ReturnsCorrectDuration()
    {
        var start = DateTime.UtcNow;
        var end = start.AddMinutes(45);
        var log = new CompletionLog { CompletionLogId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid(), CompletionDate = DateTime.UtcNow, ActualStartTime = start, ActualEndTime = end };
        Assert.That(log.GetActualDurationMinutes(), Is.EqualTo(45.0));
    }

    [Test]
    public void GetActualDurationMinutes_MissingTimes_ReturnsNull()
    {
        var log = new CompletionLog { CompletionLogId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid(), CompletionDate = DateTime.UtcNow };
        Assert.That(log.GetActualDurationMinutes(), Is.Null);
    }
}

public class RoutineTests
{
    [Test]
    public void GetTotalTasks_ReturnsTaskCount()
    {
        var routine = new Routine { RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Morning Routine" };
        routine.Tasks.Add(new RoutineTask { RoutineTaskId = Guid.NewGuid(), RoutineId = routine.RoutineId, Name = "Exercise" });
        routine.Tasks.Add(new RoutineTask { RoutineTaskId = Guid.NewGuid(), RoutineId = routine.RoutineId, Name = "Meditate" });
        Assert.That(routine.GetTotalTasks(), Is.EqualTo(2));
    }

    [Test]
    public void Activate_WhenCalled_SetsIsActiveToTrue()
    {
        var routine = new Routine { RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Test", IsActive = false };
        routine.Activate();
        Assert.That(routine.IsActive, Is.True);
    }

    [Test]
    public void Deactivate_WhenCalled_SetsIsActiveToFalse()
    {
        var routine = new Routine { RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Test", IsActive = true };
        routine.Deactivate();
        Assert.That(routine.IsActive, Is.False);
    }
}

public class StreakTests
{
    [Test]
    public void IncrementStreak_FirstCompletion_SetsStreakToOne()
    {
        var streak = new Streak { StreakId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid() };
        var completionDate = DateTime.UtcNow;
        streak.IncrementStreak(completionDate);
        Assert.Multiple(() =>
        {
            Assert.That(streak.CurrentStreak, Is.EqualTo(1));
            Assert.That(streak.LongestStreak, Is.EqualTo(1));
            Assert.That(streak.LastCompletionDate, Is.EqualTo(completionDate));
        });
    }

    [Test]
    public void IncrementStreak_ConsecutiveDays_IncrementsStreak()
    {
        var streak = new Streak { StreakId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid() };
        var day1 = new DateTime(2025, 1, 1);
        var day2 = new DateTime(2025, 1, 2);
        streak.IncrementStreak(day1);
        streak.IncrementStreak(day2);
        Assert.That(streak.CurrentStreak, Is.EqualTo(2));
    }

    [Test]
    public void IncrementStreak_SkippedDay_ResetsStreak()
    {
        var streak = new Streak { StreakId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid() };
        var day1 = new DateTime(2025, 1, 1);
        var day3 = new DateTime(2025, 1, 3);
        streak.IncrementStreak(day1);
        streak.IncrementStreak(day3);
        Assert.That(streak.CurrentStreak, Is.EqualTo(1));
    }

    [Test]
    public void ResetStreak_WhenCalled_ResetsValues()
    {
        var streak = new Streak { StreakId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid(), CurrentStreak = 5 };
        streak.ResetStreak();
        Assert.Multiple(() =>
        {
            Assert.That(streak.CurrentStreak, Is.EqualTo(0));
            Assert.That(streak.StreakStartDate, Is.Null);
            Assert.That(streak.IsActive, Is.False);
        });
    }

    [Test]
    public void IsStreakBroken_MoreThanOneDayAgo_ReturnsTrue()
    {
        var streak = new Streak { StreakId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid(), LastCompletionDate = DateTime.UtcNow.AddDays(-2) };
        Assert.That(streak.IsStreakBroken(), Is.True);
    }

    [Test]
    public void IsStreakBroken_Yesterday_ReturnsFalse()
    {
        var streak = new Streak { StreakId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), UserId = Guid.NewGuid(), LastCompletionDate = DateTime.UtcNow.AddDays(-1) };
        Assert.That(streak.IsStreakBroken(), Is.False);
    }
}

public class RoutineTaskTests
{
    [Test]
    public void IsRequired_WhenOptional_ReturnsFalse()
    {
        var task = new RoutineTask { RoutineTaskId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), Name = "Optional Task", IsOptional = true };
        Assert.That(task.IsRequired(), Is.False);
    }

    [Test]
    public void IsRequired_WhenNotOptional_ReturnsTrue()
    {
        var task = new RoutineTask { RoutineTaskId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), Name = "Required Task", IsOptional = false };
        Assert.That(task.IsRequired(), Is.True);
    }

    [Test]
    public void TaskType_CanBeSetToAllValues()
    {
        var task = new RoutineTask { RoutineTaskId = Guid.NewGuid(), RoutineId = Guid.NewGuid(), Name = "Test" };
        task.TaskType = TaskType.Exercise;
        Assert.That(task.TaskType, Is.EqualTo(TaskType.Exercise));
        task.TaskType = TaskType.Meditation;
        Assert.That(task.TaskType, Is.EqualTo(TaskType.Meditation));
        task.TaskType = TaskType.Journaling;
        Assert.That(task.TaskType, Is.EqualTo(TaskType.Journaling));
        task.TaskType = TaskType.Reading;
        Assert.That(task.TaskType, Is.EqualTo(TaskType.Reading));
        task.TaskType = TaskType.Breakfast;
        Assert.That(task.TaskType, Is.EqualTo(TaskType.Breakfast));
        task.TaskType = TaskType.Hygiene;
        Assert.That(task.TaskType, Is.EqualTo(TaskType.Hygiene));
        task.TaskType = TaskType.Planning;
        Assert.That(task.TaskType, Is.EqualTo(TaskType.Planning));
        task.TaskType = TaskType.Gratitude;
        Assert.That(task.TaskType, Is.EqualTo(TaskType.Gratitude));
        task.TaskType = TaskType.Learning;
        Assert.That(task.TaskType, Is.EqualTo(TaskType.Learning));
        task.TaskType = TaskType.Other;
        Assert.That(task.TaskType, Is.EqualTo(TaskType.Other));
    }
}

public class TaskTypeTests
{
    [Test]
    public void TaskType_AllValues_HaveCorrectIntValues()
    {
        Assert.That((int)TaskType.Exercise, Is.EqualTo(0));
        Assert.That((int)TaskType.Meditation, Is.EqualTo(1));
        Assert.That((int)TaskType.Journaling, Is.EqualTo(2));
        Assert.That((int)TaskType.Reading, Is.EqualTo(3));
        Assert.That((int)TaskType.Breakfast, Is.EqualTo(4));
        Assert.That((int)TaskType.Hygiene, Is.EqualTo(5));
        Assert.That((int)TaskType.Planning, Is.EqualTo(6));
        Assert.That((int)TaskType.Gratitude, Is.EqualTo(7));
        Assert.That((int)TaskType.Learning, Is.EqualTo(8));
        Assert.That((int)TaskType.Other, Is.EqualTo(9));
    }
}

public class DomainEventsTests
{
    [Test]
    public void RoutineCompletedEvent_ValidParameters_CreatesEvent()
    {
        var evt = new RoutineCompletedEvent
        {
            CompletionLogId = Guid.NewGuid(),
            RoutineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletionDate = DateTime.UtcNow,
            TasksCompleted = 8,
            TotalTasks = 10,
            Timestamp = DateTime.UtcNow
        };
        Assert.That(evt.TasksCompleted, Is.EqualTo(8));
    }

    [Test]
    public void RoutineCreatedEvent_ValidParameters_CreatesEvent()
    {
        var evt = new RoutineCreatedEvent
        {
            RoutineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Morning Routine",
            TargetStartTime = new TimeSpan(6, 0, 0),
            Timestamp = DateTime.UtcNow
        };
        Assert.That(evt.Name, Is.EqualTo("Morning Routine"));
    }

    [Test]
    public void StreakExtendedEvent_ValidParameters_CreatesEvent()
    {
        var evt = new StreakExtendedEvent
        {
            StreakId = Guid.NewGuid(),
            RoutineId = Guid.NewGuid(),
            CurrentStreak = 10,
            LongestStreak = 15,
            Timestamp = DateTime.UtcNow
        };
        Assert.Multiple(() =>
        {
            Assert.That(evt.CurrentStreak, Is.EqualTo(10));
            Assert.That(evt.LongestStreak, Is.EqualTo(15));
        });
    }
}
