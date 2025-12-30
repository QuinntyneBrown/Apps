using PetCareManager.Api.Features.Pets;
using PetCareManager.Core;
using Microsoft.Extensions.Logging;
using Moq;

namespace PetCareManager.Api.Tests.Features.Pets;

[TestFixture]
public class GetPetsQueryTests
{
    private Mock<IPetCareManagerContext> _contextMock = null!;
    private Mock<ILogger<GetPetsQueryHandler>> _loggerMock = null!;
    private GetPetsQueryHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IPetCareManagerContext>();
        _loggerMock = new Mock<ILogger<GetPetsQueryHandler>>();
        _handler = new GetPetsQueryHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_WithUserId_ReturnsUserPets()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var pets = new List<Pet>
        {
            new Pet { PetId = Guid.NewGuid(), UserId = userId, Name = "Max", PetType = PetType.Dog },
            new Pet { PetId = Guid.NewGuid(), UserId = userId, Name = "Bella", PetType = PetType.Cat },
            new Pet { PetId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Charlie", PetType = PetType.Dog },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(pets);
        _contextMock.Setup(c => c.Pets).Returns(mockDbSet.Object);

        var query = new GetPetsQuery { UserId = userId };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        var resultList = result.ToList();
        Assert.That(resultList, Has.Count.EqualTo(2));
        Assert.That(resultList.All(p => p.UserId == userId), Is.True);
    }

    [Test]
    public async Task Handle_WithPetType_ReturnsFilteredPets()
    {
        // Arrange
        var pets = new List<Pet>
        {
            new Pet { PetId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Max", PetType = PetType.Dog },
            new Pet { PetId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Bella", PetType = PetType.Cat },
            new Pet { PetId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Charlie", PetType = PetType.Dog },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(pets);
        _contextMock.Setup(c => c.Pets).Returns(mockDbSet.Object);

        var query = new GetPetsQuery { PetType = PetType.Dog };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        var resultList = result.ToList();
        Assert.That(resultList, Has.Count.EqualTo(2));
        Assert.That(resultList.All(p => p.PetType == PetType.Dog), Is.True);
    }
}
