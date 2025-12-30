namespace DigitalLegacyPlanner.Core.Tests;

public class LegacyInstructionTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesLegacyInstruction()
    {
        // Arrange
        var instructionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Access to Safe";
        var content = "Combination is stored with attorney";
        var category = "Financial";
        var priority = 1;
        var assignedTo = "Executor";
        var executionTiming = "Immediately";

        // Act
        var instruction = new LegacyInstruction
        {
            LegacyInstructionId = instructionId,
            UserId = userId,
            Title = title,
            Content = content,
            Category = category,
            Priority = priority,
            AssignedTo = assignedTo,
            ExecutionTiming = executionTiming
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(instruction.LegacyInstructionId, Is.EqualTo(instructionId));
            Assert.That(instruction.UserId, Is.EqualTo(userId));
            Assert.That(instruction.Title, Is.EqualTo(title));
            Assert.That(instruction.Content, Is.EqualTo(content));
            Assert.That(instruction.Category, Is.EqualTo(category));
            Assert.That(instruction.Priority, Is.EqualTo(priority));
            Assert.That(instruction.AssignedTo, Is.EqualTo(assignedTo));
            Assert.That(instruction.ExecutionTiming, Is.EqualTo(executionTiming));
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var instruction = new LegacyInstruction();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(instruction.Title, Is.EqualTo(string.Empty));
            Assert.That(instruction.Content, Is.EqualTo(string.Empty));
            Assert.That(instruction.Priority, Is.EqualTo(0));
            Assert.That(instruction.Category, Is.Null);
            Assert.That(instruction.AssignedTo, Is.Null);
            Assert.That(instruction.ExecutionTiming, Is.Null);
            Assert.That(instruction.LastUpdatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(instruction.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void UpdateContent_UpdatesContentAndTimestamp()
    {
        // Arrange
        var instruction = new LegacyInstruction
        {
            LegacyInstructionId = Guid.NewGuid(),
            Content = "Original content"
        };
        var newContent = "Updated content";
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        instruction.UpdateContent(newContent);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(instruction.Content, Is.EqualTo(newContent));
            Assert.That(instruction.LastUpdatedAt, Is.GreaterThan(beforeUpdate));
        });
    }

    [Test]
    public void UpdateContent_WithEmptyString_UpdatesContent()
    {
        // Arrange
        var instruction = new LegacyInstruction
        {
            LegacyInstructionId = Guid.NewGuid(),
            Content = "Original content"
        };

        // Act
        instruction.UpdateContent(string.Empty);

        // Assert
        Assert.That(instruction.Content, Is.EqualTo(string.Empty));
    }

    [Test]
    public void UpdateContent_CalledMultipleTimes_UpdatesTimestamp()
    {
        // Arrange
        var instruction = new LegacyInstruction
        {
            LegacyInstructionId = Guid.NewGuid(),
            Content = "Original"
        };

        instruction.UpdateContent("First update");
        var firstUpdate = instruction.LastUpdatedAt;

        System.Threading.Thread.Sleep(10);

        // Act
        instruction.UpdateContent("Second update");

        // Assert
        Assert.That(instruction.LastUpdatedAt, Is.GreaterThanOrEqualTo(firstUpdate));
    }

    [Test]
    public void Priority_CanBeSet()
    {
        // Arrange & Act
        var instruction = new LegacyInstruction
        {
            Priority = 5
        };

        // Assert
        Assert.That(instruction.Priority, Is.EqualTo(5));
    }

    [Test]
    public void Category_CanBeSet()
    {
        // Arrange
        var category = "Medical";
        var instruction = new LegacyInstruction();

        // Act
        instruction.Category = category;

        // Assert
        Assert.That(instruction.Category, Is.EqualTo(category));
    }

    [Test]
    public void AssignedTo_CanBeSet()
    {
        // Arrange
        var assignedTo = "Family Trustee";
        var instruction = new LegacyInstruction();

        // Act
        instruction.AssignedTo = assignedTo;

        // Assert
        Assert.That(instruction.AssignedTo, Is.EqualTo(assignedTo));
    }

    [Test]
    public void ExecutionTiming_CanBeSet()
    {
        // Arrange
        var executionTiming = "After 30 days";
        var instruction = new LegacyInstruction();

        // Act
        instruction.ExecutionTiming = executionTiming;

        // Assert
        Assert.That(instruction.ExecutionTiming, Is.EqualTo(executionTiming));
    }

    [Test]
    public void CreatedAt_IsSetOnCreation()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var instruction = new LegacyInstruction();

        // Assert
        Assert.That(instruction.CreatedAt, Is.GreaterThan(beforeCreation));
    }

    [Test]
    public void UpdateContent_WithLongText_UpdatesCorrectly()
    {
        // Arrange
        var instruction = new LegacyInstruction();
        var longContent = new string('A', 1000);

        // Act
        instruction.UpdateContent(longContent);

        // Assert
        Assert.That(instruction.Content, Is.EqualTo(longContent));
    }
}
