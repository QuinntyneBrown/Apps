// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeBuilder.Api.Tests.Controllers;

[TestFixture]
public class PersonsControllerTests
{
    private Mock<IMediator> _mockMediator = null!;
    private PersonsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new PersonsController(_mockMediator.Object);
    }

    [Test]
    public async Task GetPersons_ShouldReturnOkResult_WithListOfPersons()
    {
        // Arrange
        var persons = new List<PersonDto>
        {
            new PersonDto
            {
                PersonId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe"
            }
        };

        _mockMediator
            .Setup(m => m.Send(It.IsAny<GetPersons.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(persons);

        // Act
        var result = await _controller.GetPersons(null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(persons));
    }

    [Test]
    public async Task GetPersonById_ShouldReturnNotFound_WhenPersonDoesNotExist()
    {
        // Arrange
        _mockMediator
            .Setup(m => m.Send(It.IsAny<GetPersonById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PersonDto?)null);

        // Act
        var result = await _controller.GetPersonById(Guid.NewGuid());

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreatePerson_ShouldReturnCreatedAtAction_WithNewPerson()
    {
        // Arrange
        var personDto = new PersonDto
        {
            PersonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe"
        };

        _mockMediator
            .Setup(m => m.Send(It.IsAny<CreatePerson.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(personDto);

        var command = new CreatePerson.Command
        {
            UserId = personDto.UserId,
            FirstName = "John",
            LastName = "Doe"
        };

        // Act
        var result = await _controller.CreatePerson(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(personDto));
    }

    [Test]
    public async Task DeletePerson_ShouldReturnNoContent_WhenPersonExists()
    {
        // Arrange
        _mockMediator
            .Setup(m => m.Send(It.IsAny<DeletePerson.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeletePerson(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeletePerson_ShouldReturnNotFound_WhenPersonDoesNotExist()
    {
        // Arrange
        _mockMediator
            .Setup(m => m.Send(It.IsAny<DeletePerson.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeletePerson(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
