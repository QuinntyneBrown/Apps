using PhotographySessionLogger.Api.Features.Gears;
using PhotographySessionLogger.Api.Features.Projects;
using PhotographySessionLogger.Api.Features.Sessions;
using PhotographySessionLogger.Api.Features.Photos;

namespace PhotographySessionLogger.Api.Tests;

[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void GearDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var gear = new Core.Gear
        {
            GearId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Canon EOS R5",
            GearType = "Camera",
            Brand = "Canon",
            Model = "EOS R5",
            PurchaseDate = DateTime.UtcNow.AddMonths(-6),
            PurchasePrice = 3899.99m,
            Notes = "Professional mirrorless camera",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = gear.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.GearId, Is.EqualTo(gear.GearId));
            Assert.That(dto.UserId, Is.EqualTo(gear.UserId));
            Assert.That(dto.Name, Is.EqualTo(gear.Name));
            Assert.That(dto.GearType, Is.EqualTo(gear.GearType));
            Assert.That(dto.Brand, Is.EqualTo(gear.Brand));
            Assert.That(dto.Model, Is.EqualTo(gear.Model));
            Assert.That(dto.PurchaseDate, Is.EqualTo(gear.PurchaseDate));
            Assert.That(dto.PurchasePrice, Is.EqualTo(gear.PurchasePrice));
            Assert.That(dto.Notes, Is.EqualTo(gear.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(gear.CreatedAt));
        });
    }

    [Test]
    public void ProjectDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var project = new Core.Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Wedding Photography Project",
            Description = "Complete wedding photography package",
            DueDate = DateTime.UtcNow.AddMonths(2),
            IsCompleted = false,
            Notes = "Client requires 200+ edited photos",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = project.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ProjectId, Is.EqualTo(project.ProjectId));
            Assert.That(dto.UserId, Is.EqualTo(project.UserId));
            Assert.That(dto.Name, Is.EqualTo(project.Name));
            Assert.That(dto.Description, Is.EqualTo(project.Description));
            Assert.That(dto.DueDate, Is.EqualTo(project.DueDate));
            Assert.That(dto.IsCompleted, Is.EqualTo(project.IsCompleted));
            Assert.That(dto.Notes, Is.EqualTo(project.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(project.CreatedAt));
        });
    }

    [Test]
    public void SessionDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var session = new Core.Session
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Downtown Portrait Session",
            SessionType = Core.SessionType.Portrait,
            SessionDate = DateTime.UtcNow,
            Location = "Downtown Park",
            Client = "John Doe",
            Notes = "Golden hour shoot",
            CreatedAt = DateTime.UtcNow,
            Photos = new List<Core.Photo>
            {
                new Core.Photo { PhotoId = Guid.NewGuid() },
                new Core.Photo { PhotoId = Guid.NewGuid() },
            }
        };

        // Act
        var dto = session.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.SessionId, Is.EqualTo(session.SessionId));
            Assert.That(dto.UserId, Is.EqualTo(session.UserId));
            Assert.That(dto.Title, Is.EqualTo(session.Title));
            Assert.That(dto.SessionType, Is.EqualTo(session.SessionType));
            Assert.That(dto.SessionDate, Is.EqualTo(session.SessionDate));
            Assert.That(dto.Location, Is.EqualTo(session.Location));
            Assert.That(dto.Client, Is.EqualTo(session.Client));
            Assert.That(dto.Notes, Is.EqualTo(session.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(session.CreatedAt));
            Assert.That(dto.PhotoCount, Is.EqualTo(2));
        });
    }

    [Test]
    public void PhotoDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var photo = new Core.Photo
        {
            PhotoId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionId = Guid.NewGuid(),
            FileName = "IMG_1234.jpg",
            FilePath = "/photos/2024/IMG_1234.jpg",
            CameraSettings = "ISO 100, f/2.8, 1/250s",
            Rating = 5,
            Tags = "portrait,outdoor,golden-hour",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = photo.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.PhotoId, Is.EqualTo(photo.PhotoId));
            Assert.That(dto.UserId, Is.EqualTo(photo.UserId));
            Assert.That(dto.SessionId, Is.EqualTo(photo.SessionId));
            Assert.That(dto.FileName, Is.EqualTo(photo.FileName));
            Assert.That(dto.FilePath, Is.EqualTo(photo.FilePath));
            Assert.That(dto.CameraSettings, Is.EqualTo(photo.CameraSettings));
            Assert.That(dto.Rating, Is.EqualTo(photo.Rating));
            Assert.That(dto.Tags, Is.EqualTo(photo.Tags));
            Assert.That(dto.CreatedAt, Is.EqualTo(photo.CreatedAt));
        });
    }
}
