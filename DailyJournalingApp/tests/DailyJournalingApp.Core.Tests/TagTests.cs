namespace DailyJournalingApp.Core.Tests;

public class TagTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTag()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Gratitude";
        var color = "#FF5733";
        var usageCount = 5;

        // Act
        var tag = new Tag
        {
            TagId = tagId,
            UserId = userId,
            Name = name,
            Color = color,
            UsageCount = usageCount
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tag.TagId, Is.EqualTo(tagId));
            Assert.That(tag.UserId, Is.EqualTo(userId));
            Assert.That(tag.Name, Is.EqualTo(name));
            Assert.That(tag.Color, Is.EqualTo(color));
            Assert.That(tag.UsageCount, Is.EqualTo(usageCount));
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var tag = new Tag();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tag.Name, Is.EqualTo(string.Empty));
            Assert.That(tag.UsageCount, Is.EqualTo(0));
            Assert.That(tag.Color, Is.Null);
            Assert.That(tag.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void IncrementUsageCount_IncrementsFromZero()
    {
        // Arrange
        var tag = new Tag
        {
            TagId = Guid.NewGuid(),
            UsageCount = 0
        };

        // Act
        tag.IncrementUsageCount();

        // Assert
        Assert.That(tag.UsageCount, Is.EqualTo(1));
    }

    [Test]
    public void IncrementUsageCount_IncrementsFromExistingValue()
    {
        // Arrange
        var tag = new Tag
        {
            TagId = Guid.NewGuid(),
            UsageCount = 10
        };

        // Act
        tag.IncrementUsageCount();

        // Assert
        Assert.That(tag.UsageCount, Is.EqualTo(11));
    }

    [Test]
    public void IncrementUsageCount_CalledMultipleTimes_IncrementsCorrectly()
    {
        // Arrange
        var tag = new Tag
        {
            TagId = Guid.NewGuid(),
            UsageCount = 5
        };

        // Act
        tag.IncrementUsageCount();
        tag.IncrementUsageCount();
        tag.IncrementUsageCount();

        // Assert
        Assert.That(tag.UsageCount, Is.EqualTo(8));
    }

    [Test]
    public void Color_CanBeSet()
    {
        // Arrange
        var color = "#3498DB";
        var tag = new Tag();

        // Act
        tag.Color = color;

        // Assert
        Assert.That(tag.Color, Is.EqualTo(color));
    }

    [Test]
    public void Color_CanBeNull()
    {
        // Arrange & Act
        var tag = new Tag
        {
            Color = null
        };

        // Assert
        Assert.That(tag.Color, Is.Null);
    }

    [Test]
    public void Name_CanBeSet()
    {
        // Arrange
        var name = "Work";
        var tag = new Tag();

        // Act
        tag.Name = name;

        // Assert
        Assert.That(tag.Name, Is.EqualTo(name));
    }

    [Test]
    public void UsageCount_CanBeSetDirectly()
    {
        // Arrange & Act
        var tag = new Tag
        {
            UsageCount = 100
        };

        // Assert
        Assert.That(tag.UsageCount, Is.EqualTo(100));
    }

    [Test]
    public void UserId_CanBeSet()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tag = new Tag();

        // Act
        tag.UserId = userId;

        // Assert
        Assert.That(tag.UserId, Is.EqualTo(userId));
    }

    [Test]
    public void CreatedAt_IsSetOnCreation()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var tag = new Tag();

        // Assert
        Assert.That(tag.CreatedAt, Is.GreaterThan(beforeCreation));
    }
}
