using PersonalMissionStatementBuilder.Api.Features.MissionStatements;
using PersonalMissionStatementBuilder.Core;

namespace PersonalMissionStatementBuilder.Api.Tests.Features.MissionStatements;

[TestFixture]
public class MissionStatementDtoTests
{
    [Test]
    public void ToDto_ValidMissionStatement_MapsAllProperties()
    {
        // Arrange
        var missionStatementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow.AddDays(1);
        var statementDate = DateTime.UtcNow.AddDays(-10);

        var missionStatement = new MissionStatement
        {
            MissionStatementId = missionStatementId,
            UserId = userId,
            Title = "My Life Mission",
            Text = "To live a meaningful life helping others",
            Version = 2,
            IsCurrentVersion = true,
            StatementDate = statementDate,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt,
        };

        // Act
        var dto = missionStatement.ToDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.MissionStatementId, Is.EqualTo(missionStatementId));
        Assert.That(dto.UserId, Is.EqualTo(userId));
        Assert.That(dto.Title, Is.EqualTo("My Life Mission"));
        Assert.That(dto.Text, Is.EqualTo("To live a meaningful life helping others"));
        Assert.That(dto.Version, Is.EqualTo(2));
        Assert.That(dto.IsCurrentVersion, Is.True);
        Assert.That(dto.StatementDate, Is.EqualTo(statementDate));
        Assert.That(dto.CreatedAt, Is.EqualTo(createdAt));
        Assert.That(dto.UpdatedAt, Is.EqualTo(updatedAt));
    }

    [Test]
    public void ToDto_MissionStatementWithNullUpdatedAt_MapsCorrectly()
    {
        // Arrange
        var missionStatement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Simple Mission",
            Text = "Do good",
            Version = 1,
            IsCurrentVersion = true,
            StatementDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
        };

        // Act
        var dto = missionStatement.ToDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.UpdatedAt, Is.Null);
    }
}
