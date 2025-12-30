using PersonalMissionStatementBuilder.Api.Features.Values;
using PersonalMissionStatementBuilder.Core;

namespace PersonalMissionStatementBuilder.Api.Tests.Features.Values;

[TestFixture]
public class ValueDtoTests
{
    [Test]
    public void ToDto_ValidValue_MapsAllProperties()
    {
        // Arrange
        var valueId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var missionStatementId = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow.AddDays(1);

        var value = new Value
        {
            ValueId = valueId,
            MissionStatementId = missionStatementId,
            UserId = userId,
            Name = "Integrity",
            Description = "Acting with honesty and strong moral principles",
            Priority = 1,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt,
        };

        // Act
        var dto = value.ToDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.ValueId, Is.EqualTo(valueId));
        Assert.That(dto.MissionStatementId, Is.EqualTo(missionStatementId));
        Assert.That(dto.UserId, Is.EqualTo(userId));
        Assert.That(dto.Name, Is.EqualTo("Integrity"));
        Assert.That(dto.Description, Is.EqualTo("Acting with honesty and strong moral principles"));
        Assert.That(dto.Priority, Is.EqualTo(1));
        Assert.That(dto.CreatedAt, Is.EqualTo(createdAt));
        Assert.That(dto.UpdatedAt, Is.EqualTo(updatedAt));
    }

    [Test]
    public void ToDto_ValueWithNullableFields_MapsCorrectly()
    {
        // Arrange
        var value = new Value
        {
            ValueId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Compassion",
            Priority = 2,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = value.ToDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.MissionStatementId, Is.Null);
        Assert.That(dto.Description, Is.Null);
        Assert.That(dto.UpdatedAt, Is.Null);
    }
}
