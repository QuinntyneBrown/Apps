using PetCareManager.Api.Features.Pets;
using PetCareManager.Core;
using Microsoft.Extensions.Logging;
using Moq;

namespace PetCareManager.Api.Tests.Features.Pets;

[TestFixture]
public class CreatePetCommandTests
{
    private Mock<IPetCareManagerContext> _contextMock = null!;
    private Mock<ILogger<CreatePetCommandHandler>> _loggerMock = null!;
    private CreatePetCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IPetCareManagerContext>();
        _loggerMock = new Mock<ILogger<CreatePetCommandHandler>>();
        _handler = new CreatePetCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesPet()
    {
        // Arrange
        var command = new CreatePetCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Max",
            PetType = PetType.Dog,
            Breed = "Golden Retriever",
            DateOfBirth = new DateTime(2020, 1, 1),
            Color = "Golden",
            Weight = 30.5m,
            MicrochipNumber = "123456789",
        };

        var pets = new List<Pet>();
        var mockDbSet = TestHelpers.CreateMockDbSet(pets);
        _contextMock.Setup(c => c.Pets).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.PetType, Is.EqualTo(command.PetType));
        Assert.That(result.Breed, Is.EqualTo(command.Breed));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithMicrochipNumber_IncludesMicrochipNumber()
    {
        // Arrange
        var microchipNumber = "987654321";
        var command = new CreatePetCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Bella",
            PetType = PetType.Cat,
            MicrochipNumber = microchipNumber,
        };

        var pets = new List<Pet>();
        var mockDbSet = TestHelpers.CreateMockDbSet(pets);
        _contextMock.Setup(c => c.Pets).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.MicrochipNumber, Is.EqualTo(microchipNumber));
    }
}
