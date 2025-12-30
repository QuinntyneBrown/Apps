// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Tests.Features.Persons;

[TestFixture]
public class CreatePersonTests
{
    private Mock<IFamilyTreeBuilderContext> _mockContext = null!;
    private Mock<DbSet<Person>> _mockPersonsDbSet = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IFamilyTreeBuilderContext>();
        _mockPersonsDbSet = new Mock<DbSet<Person>>();
        _mockContext.Setup(c => c.Persons).Returns(_mockPersonsDbSet.Object);
    }

    [Test]
    public async Task Handle_ShouldCreatePerson_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreatePerson.Command
        {
            UserId = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Gender = Gender.Male,
            DateOfBirth = new DateTime(1990, 1, 1),
            BirthPlace = "New York"
        };

        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreatePerson.Handler(_mockContext.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FirstName, Is.EqualTo("John"));
        Assert.That(result.LastName, Is.EqualTo("Doe"));
        Assert.That(result.Gender, Is.EqualTo(Gender.Male));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        _mockPersonsDbSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once);
        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
