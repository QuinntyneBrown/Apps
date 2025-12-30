// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Api.Features.Projects;
using ClassicCarRestorationLog.Core;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace ClassicCarRestorationLog.Api.Tests.Features;

[TestFixture]
public class CreateProjectTests
{
    private Mock<IClassicCarRestorationLogContext> _contextMock;
    private Mock<DbSet<Project>> _projectsDbSetMock;

    [SetUp]
    public void SetUp()
    {
        _contextMock = new Mock<IClassicCarRestorationLogContext>();
        _projectsDbSetMock = new Mock<DbSet<Project>>();
        _contextMock.Setup(c => c.Projects).Returns(_projectsDbSetMock.Object);
    }

    [Test]
    public async Task Handle_CreatesProject_ReturnsProjectDto()
    {
        // Arrange
        var command = new CreateProject.Command
        {
            UserId = Guid.NewGuid(),
            CarMake = "Ford",
            CarModel = "Mustang",
            Year = 1967,
            Phase = ProjectPhase.Planning,
            EstimatedBudget = 50000
        };

        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateProject.Handler(_contextMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(command.CarMake, result.CarMake);
        Assert.AreEqual(command.CarModel, result.CarModel);
        Assert.AreEqual(command.Year, result.Year);
        Assert.AreEqual(command.Phase, result.Phase);
        Assert.AreEqual(command.EstimatedBudget, result.EstimatedBudget);
        _projectsDbSetMock.Verify(d => d.Add(It.IsAny<Project>()), Times.Once);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
