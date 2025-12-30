// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Api.Features.Albums;
using FamilyPhotoAlbumOrganizer.Api.Features.Photos;
using FamilyPhotoAlbumOrganizer.Api.Features.Tags;
using FamilyPhotoAlbumOrganizer.Api.Features.PersonTags;
using FamilyPhotoAlbumOrganizer.Core;
using FluentAssertions;
using Moq;

namespace FamilyPhotoAlbumOrganizer.Api.Tests;

/// <summary>
/// Tests for the FamilyPhotoAlbumOrganizer API commands and queries.
/// </summary>
public class AlbumCommandTests
{
    private Mock<IFamilyPhotoAlbumOrganizerContext> _mockContext;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IFamilyPhotoAlbumOrganizerContext>();
    }

    [Test]
    public async Task CreateAlbumCommand_ShouldCreateAlbum()
    {
        // Arrange
        var command = new CreateAlbumCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Test Album",
            Description = "Test Description",
            CreatedDate = DateTime.UtcNow
        };

        var validator = new CreateAlbumCommandValidator();
        var mockAlbums = new Mock<Microsoft.EntityFrameworkCore.DbSet<Album>>();
        _mockContext.Setup(c => c.Albums).Returns(mockAlbums.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateAlbumCommandHandler(_mockContext.Object, validator);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        result.Description.Should().Be(command.Description);
        result.UserId.Should().Be(command.UserId);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void CreateAlbumCommandValidator_ShouldRequireUserId()
    {
        // Arrange
        var validator = new CreateAlbumCommandValidator();
        var command = new CreateAlbumCommand
        {
            UserId = Guid.Empty,
            Name = "Test Album"
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "UserId");
    }

    [Test]
    public void CreateAlbumCommandValidator_ShouldRequireName()
    {
        // Arrange
        var validator = new CreateAlbumCommandValidator();
        var command = new CreateAlbumCommand
        {
            UserId = Guid.NewGuid(),
            Name = string.Empty
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }
}

/// <summary>
/// Tests for Photo commands.
/// </summary>
public class PhotoCommandTests
{
    private Mock<IFamilyPhotoAlbumOrganizerContext> _mockContext;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IFamilyPhotoAlbumOrganizerContext>();
    }

    [Test]
    public async Task CreatePhotoCommand_ShouldCreatePhoto()
    {
        // Arrange
        var command = new CreatePhotoCommand
        {
            UserId = Guid.NewGuid(),
            FileName = "test.jpg",
            FileUrl = "https://example.com/test.jpg",
            Caption = "Test Photo"
        };

        var validator = new CreatePhotoCommandValidator();
        var mockPhotos = new Mock<Microsoft.EntityFrameworkCore.DbSet<Photo>>();
        _mockContext.Setup(c => c.Photos).Returns(mockPhotos.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreatePhotoCommandHandler(_mockContext.Object, validator);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.FileName.Should().Be(command.FileName);
        result.FileUrl.Should().Be(command.FileUrl);
        result.Caption.Should().Be(command.Caption);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void CreatePhotoCommandValidator_ShouldRequireFileName()
    {
        // Arrange
        var validator = new CreatePhotoCommandValidator();
        var command = new CreatePhotoCommand
        {
            UserId = Guid.NewGuid(),
            FileName = string.Empty,
            FileUrl = "https://example.com/test.jpg"
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "FileName");
    }
}

/// <summary>
/// Tests for Tag commands.
/// </summary>
public class TagCommandTests
{
    private Mock<IFamilyPhotoAlbumOrganizerContext> _mockContext;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IFamilyPhotoAlbumOrganizerContext>();
    }

    [Test]
    public async Task CreateTagCommand_ShouldCreateTag()
    {
        // Arrange
        var command = new CreateTagCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Vacation"
        };

        var validator = new CreateTagCommandValidator();
        var mockTags = new Mock<Microsoft.EntityFrameworkCore.DbSet<Tag>>();
        _mockContext.Setup(c => c.Tags).Returns(mockTags.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateTagCommandHandler(_mockContext.Object, validator);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        result.UserId.Should().Be(command.UserId);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void CreateTagCommandValidator_ShouldRequireName()
    {
        // Arrange
        var validator = new CreateTagCommandValidator();
        var command = new CreateTagCommand
        {
            UserId = Guid.NewGuid(),
            Name = string.Empty
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }
}

/// <summary>
/// Tests for PersonTag commands.
/// </summary>
public class PersonTagCommandTests
{
    private Mock<IFamilyPhotoAlbumOrganizerContext> _mockContext;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IFamilyPhotoAlbumOrganizerContext>();
    }

    [Test]
    public async Task CreatePersonTagCommand_ShouldCreatePersonTag()
    {
        // Arrange
        var command = new CreatePersonTagCommand
        {
            PhotoId = Guid.NewGuid(),
            PersonName = "John Doe",
            CoordinateX = 100,
            CoordinateY = 200
        };

        var validator = new CreatePersonTagCommandValidator();
        var mockPersonTags = new Mock<Microsoft.EntityFrameworkCore.DbSet<PersonTag>>();
        _mockContext.Setup(c => c.PersonTags).Returns(mockPersonTags.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreatePersonTagCommandHandler(_mockContext.Object, validator);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.PersonName.Should().Be(command.PersonName);
        result.PhotoId.Should().Be(command.PhotoId);
        result.CoordinateX.Should().Be(command.CoordinateX);
        result.CoordinateY.Should().Be(command.CoordinateY);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void CreatePersonTagCommandValidator_ShouldRequirePersonName()
    {
        // Arrange
        var validator = new CreatePersonTagCommandValidator();
        var command = new CreatePersonTagCommand
        {
            PhotoId = Guid.NewGuid(),
            PersonName = string.Empty
        };

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "PersonName");
    }
}
