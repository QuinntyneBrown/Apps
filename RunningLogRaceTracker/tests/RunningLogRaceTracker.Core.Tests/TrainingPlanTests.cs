namespace RunningLogRaceTracker.Core.Tests;

public class TrainingPlanTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTrainingPlan()
    {
        // Arrange & Act
        var plan = new TrainingPlan
        {
            TrainingPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Marathon Training Plan",
            RaceId = Guid.NewGuid(),
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 4, 15),
            WeeklyMileageGoal = 50m,
            PlanDetails = "{\"weeks\": 16}",
            IsActive = true,
            Notes = "16-week plan"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(plan.TrainingPlanId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(plan.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(plan.Name, Is.EqualTo("Marathon Training Plan"));
            Assert.That(plan.RaceId, Is.Not.Null);
            Assert.That(plan.WeeklyMileageGoal, Is.EqualTo(50m));
            Assert.That(plan.IsActive, Is.True);
        });
    }

    [Test]
    public void GetDurationInWeeks_ValidDates_ReturnsCorrectDuration()
    {
        // Arrange
        var plan = new TrainingPlan
        {
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 1, 29) // 28 days = 4 weeks
        };

        // Act
        var weeks = plan.GetDurationInWeeks();

        // Assert
        Assert.That(weeks, Is.EqualTo(4));
    }

    [Test]
    public void GetDurationInWeeks_PartialWeek_RoundsDown()
    {
        // Arrange
        var plan = new TrainingPlan
        {
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 1, 25) // 24 days = 3.43 weeks
        };

        // Act
        var weeks = plan.GetDurationInWeeks();

        // Assert
        Assert.That(weeks, Is.EqualTo(3));
    }

    [Test]
    public void GetDurationInWeeks_SixteenWeeks_ReturnsCorrectDuration()
    {
        // Arrange
        var plan = new TrainingPlan
        {
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 4, 22) // ~16 weeks
        };

        // Act
        var weeks = plan.GetDurationInWeeks();

        // Assert
        Assert.That(weeks, Is.EqualTo(16));
    }

    [Test]
    public void IsInProgress_CurrentDateInRange_ReturnsTrue()
    {
        // Arrange
        var plan = new TrainingPlan
        {
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow.AddDays(7),
            IsActive = true
        };

        // Act
        var inProgress = plan.IsInProgress();

        // Assert
        Assert.That(inProgress, Is.True);
    }

    [Test]
    public void IsInProgress_NotStartedYet_ReturnsFalse()
    {
        // Arrange
        var plan = new TrainingPlan
        {
            StartDate = DateTime.UtcNow.AddDays(7),
            EndDate = DateTime.UtcNow.AddDays(70),
            IsActive = true
        };

        // Act
        var inProgress = plan.IsInProgress();

        // Assert
        Assert.That(inProgress, Is.False);
    }

    [Test]
    public void IsInProgress_AlreadyEnded_ReturnsFalse()
    {
        // Arrange
        var plan = new TrainingPlan
        {
            StartDate = DateTime.UtcNow.AddDays(-70),
            EndDate = DateTime.UtcNow.AddDays(-7),
            IsActive = true
        };

        // Act
        var inProgress = plan.IsInProgress();

        // Assert
        Assert.That(inProgress, Is.False);
    }

    [Test]
    public void IsInProgress_NotActive_ReturnsFalse()
    {
        // Arrange
        var plan = new TrainingPlan
        {
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow.AddDays(7),
            IsActive = false
        };

        // Act
        var inProgress = plan.IsInProgress();

        // Assert
        Assert.That(inProgress, Is.False);
    }

    [Test]
    public void IsInProgress_StartDateEqualsToday_ReturnsTrue()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var plan = new TrainingPlan
        {
            StartDate = today,
            EndDate = today.AddDays(30),
            IsActive = true
        };

        // Act
        var inProgress = plan.IsInProgress();

        // Assert
        Assert.That(inProgress, Is.True);
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var plan = new TrainingPlan();

        // Assert
        Assert.That(plan.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void IsActive_DefaultValue_IsTrue()
    {
        // Arrange & Act
        var plan = new TrainingPlan();

        // Assert
        Assert.That(plan.IsActive, Is.True);
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSet()
    {
        // Arrange & Act
        var plan = new TrainingPlan();

        // Assert
        Assert.That(plan.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        Assert.That(plan.CreatedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void RaceId_CanBeNull()
    {
        // Arrange & Act
        var plan = new TrainingPlan
        {
            RaceId = null
        };

        // Assert
        Assert.That(plan.RaceId, Is.Null);
    }

    [Test]
    public void WeeklyMileageGoal_CanBeNull()
    {
        // Arrange & Act
        var plan = new TrainingPlan
        {
            WeeklyMileageGoal = null
        };

        // Assert
        Assert.That(plan.WeeklyMileageGoal, Is.Null);
    }

    [Test]
    public void PlanDetails_CanBeNull()
    {
        // Arrange & Act
        var plan = new TrainingPlan
        {
            PlanDetails = null
        };

        // Assert
        Assert.That(plan.PlanDetails, Is.Null);
    }

    [Test]
    public void Race_NavigationProperty_CanBeSet()
    {
        // Arrange
        var plan = new TrainingPlan();
        var race = new Race
        {
            RaceId = Guid.NewGuid(),
            Name = "Boston Marathon"
        };

        // Act
        plan.Race = race;

        // Assert
        Assert.That(plan.Race, Is.Not.Null);
        Assert.That(plan.Race.Name, Is.EqualTo("Boston Marathon"));
    }

    [Test]
    public void GetDurationInWeeks_OneDay_ReturnsZero()
    {
        // Arrange
        var plan = new TrainingPlan
        {
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 1, 2) // 1 day
        };

        // Act
        var weeks = plan.GetDurationInWeeks();

        // Assert
        Assert.That(weeks, Is.EqualTo(0));
    }
}
