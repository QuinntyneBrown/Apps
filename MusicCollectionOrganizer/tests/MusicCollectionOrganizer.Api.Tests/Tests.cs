using MusicCollectionOrganizer.Api.Features.Albums;
using MusicCollectionOrganizer.Api.Features.Artists;
using MusicCollectionOrganizer.Api.Features.ListeningLogs;

namespace MusicCollectionOrganizer.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void AlbumDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var album = new Core.Album
        {
            AlbumId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Album",
            ArtistId = Guid.NewGuid(),
            Artist = new Core.Artist { Name = "Test Artist" },
            Format = Core.Format.CD,
            Genre = Core.Genre.Rock,
            ReleaseYear = 2024,
            Label = "Test Label",
            PurchasePrice = 19.99m,
            PurchaseDate = new DateTime(2024, 1, 15),
            Notes = "Test Notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = album.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.AlbumId, Is.EqualTo(album.AlbumId));
            Assert.That(dto.UserId, Is.EqualTo(album.UserId));
            Assert.That(dto.Title, Is.EqualTo(album.Title));
            Assert.That(dto.ArtistId, Is.EqualTo(album.ArtistId));
            Assert.That(dto.ArtistName, Is.EqualTo(album.Artist.Name));
            Assert.That(dto.Format, Is.EqualTo(album.Format));
            Assert.That(dto.Genre, Is.EqualTo(album.Genre));
            Assert.That(dto.ReleaseYear, Is.EqualTo(album.ReleaseYear));
            Assert.That(dto.Label, Is.EqualTo(album.Label));
            Assert.That(dto.PurchasePrice, Is.EqualTo(album.PurchasePrice));
            Assert.That(dto.PurchaseDate, Is.EqualTo(album.PurchaseDate));
            Assert.That(dto.Notes, Is.EqualTo(album.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(album.CreatedAt));
        });
    }

    [Test]
    public void ArtistDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var artist = new Core.Artist
        {
            ArtistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Artist",
            Biography = "Test Biography",
            Country = "USA",
            FormedYear = 1990,
            Website = "https://testartist.com",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = artist.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ArtistId, Is.EqualTo(artist.ArtistId));
            Assert.That(dto.UserId, Is.EqualTo(artist.UserId));
            Assert.That(dto.Name, Is.EqualTo(artist.Name));
            Assert.That(dto.Biography, Is.EqualTo(artist.Biography));
            Assert.That(dto.Country, Is.EqualTo(artist.Country));
            Assert.That(dto.FormedYear, Is.EqualTo(artist.FormedYear));
            Assert.That(dto.Website, Is.EqualTo(artist.Website));
            Assert.That(dto.CreatedAt, Is.EqualTo(artist.CreatedAt));
        });
    }

    [Test]
    public void ListeningLogDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var listeningLog = new Core.ListeningLog
        {
            ListeningLogId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid(),
            Album = new Core.Album { Title = "Test Album" },
            ListeningDate = new DateTime(2024, 6, 15),
            Rating = 5,
            Notes = "Great listening session",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = listeningLog.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ListeningLogId, Is.EqualTo(listeningLog.ListeningLogId));
            Assert.That(dto.UserId, Is.EqualTo(listeningLog.UserId));
            Assert.That(dto.AlbumId, Is.EqualTo(listeningLog.AlbumId));
            Assert.That(dto.AlbumTitle, Is.EqualTo(listeningLog.Album.Title));
            Assert.That(dto.ListeningDate, Is.EqualTo(listeningLog.ListeningDate));
            Assert.That(dto.Rating, Is.EqualTo(listeningLog.Rating));
            Assert.That(dto.Notes, Is.EqualTo(listeningLog.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(listeningLog.CreatedAt));
        });
    }
}
