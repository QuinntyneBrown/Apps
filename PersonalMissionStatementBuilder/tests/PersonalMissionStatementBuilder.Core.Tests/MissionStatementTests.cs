namespace PersonalMissionStatementBuilder.Core.Tests;

public class MissionStatementTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMissionStatement()
    {
        // Arrange
        var missionStatementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var statementDate = DateTime.UtcNow;

        // Act
        var statement = new MissionStatement
        {
            MissionStatementId = missionStatementId,
            UserId = userId,
            Title = "My Life Mission",
            Text = "To live a life of purpose and meaning",
            Version = 1,
            IsCurrentVersion = true,
            StatementDate = statementDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(statement.MissionStatementId, Is.EqualTo(missionStatementId));
            Assert.That(statement.UserId, Is.EqualTo(userId));
            Assert.That(statement.Title, Is.EqualTo("My Life Mission"));
            Assert.That(statement.Text, Is.EqualTo("To live a life of purpose and meaning"));
            Assert.That(statement.Version, Is.EqualTo(1));
            Assert.That(statement.IsCurrentVersion, Is.True);
            Assert.That(statement.StatementDate, Is.EqualTo(statementDate));
            Assert.That(statement.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(statement.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void CreateNewVersion_SetsIsCurrentVersionToFalse()
    {
        // Arrange
        var statement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "My Life Mission",
            Text = "To live a life of purpose",
            Version = 1,
            IsCurrentVersion = true
        };

        // Act
        statement.CreateNewVersion();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(statement.IsCurrentVersion, Is.False);
            Assert.That(statement.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void CreateNewVersion_SetsUpdatedAt()
    {
        // Arrange
        var statement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "My Life Mission",
            Text = "To live a life of purpose",
            Version = 1,
            IsCurrentVersion = true,
            UpdatedAt = null
        };

        // Act
        statement.CreateNewVersion();

        // Assert
        Assert.That(statement.UpdatedAt, Is.Not.Null);
    }

    [Test]
    public void UpdateText_ValidText_UpdatesTextAndTimestamp()
    {
        // Arrange
        var statement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "My Life Mission",
            Text = "Original text",
            Version = 1
        };

        // Act
        statement.UpdateText("Updated text");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(statement.Text, Is.EqualTo("Updated text"));
            Assert.That(statement.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void UpdateText_EmptyString_UpdatesText()
    {
        // Arrange
        var statement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "My Life Mission",
            Text = "Original text",
            Version = 1
        };

        // Act
        statement.UpdateText("");

        // Assert
        Assert.That(statement.Text, Is.EqualTo(""));
    }

    [Test]
    public void UpdateText_MultipleUpdates_UpdatesTimestampEachTime()
    {
        // Arrange
        var statement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "My Life Mission",
            Text = "Original text",
            Version = 1
        };

        // Act
        statement.UpdateText("First update");
        var firstUpdate = statement.UpdatedAt;

        System.Threading.Thread.Sleep(10); // Small delay to ensure different timestamps

        statement.UpdateText("Second update");
        var secondUpdate = statement.UpdatedAt;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(statement.Text, Is.EqualTo("Second update"));
            Assert.That(secondUpdate, Is.Not.Null);
            Assert.That(secondUpdate, Is.GreaterThanOrEqualTo(firstUpdate));
        });
    }

    [Test]
    public void MissionStatement_DefaultVersion_IsOne()
    {
        // Arrange & Act
        var statement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "My Life Mission",
            Text = "To live a life of purpose"
        };

        // Assert
        Assert.That(statement.Version, Is.EqualTo(1));
    }

    [Test]
    public void MissionStatement_DefaultIsCurrentVersion_IsTrue()
    {
        // Arrange & Act
        var statement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "My Life Mission",
            Text = "To live a life of purpose"
        };

        // Assert
        Assert.That(statement.IsCurrentVersion, Is.True);
    }

    [Test]
    public void MissionStatement_CanHaveValues()
    {
        // Arrange
        var statement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "My Life Mission",
            Text = "To live a life of purpose",
            Values = new List<Value>
            {
                new Value { ValueId = Guid.NewGuid(), Name = "Integrity" },
                new Value { ValueId = Guid.NewGuid(), Name = "Excellence" }
            }
        };

        // Assert
        Assert.That(statement.Values.Count, Is.EqualTo(2));
    }

    [Test]
    public void MissionStatement_CanHaveGoals()
    {
        // Arrange
        var statement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "My Life Mission",
            Text = "To live a life of purpose",
            Goals = new List<Goal>
            {
                new Goal { GoalId = Guid.NewGuid(), Title = "Learn a new skill" },
                new Goal { GoalId = Guid.NewGuid(), Title = "Build a business" }
            }
        };

        // Assert
        Assert.That(statement.Goals.Count, Is.EqualTo(2));
    }
}
