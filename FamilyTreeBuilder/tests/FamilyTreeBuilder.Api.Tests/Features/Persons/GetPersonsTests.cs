// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Tests.Features.Persons;

[TestFixture]
public class GetPersonsTests
{
    private IFamilyTreeBuilderContext _context = null!;
    private List<Person> _persons = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FamilyTreeBuilder.Infrastructure.FamilyTreeBuilderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new FamilyTreeBuilder.Infrastructure.FamilyTreeBuilderContext(options);
        _context = context;

        var userId = Guid.NewGuid();
        _persons = new List<Person>
        {
            new Person
            {
                PersonId = Guid.NewGuid(),
                UserId = userId,
                FirstName = "John",
                LastName = "Doe",
                Gender = Gender.Male
            },
            new Person
            {
                PersonId = Guid.NewGuid(),
                UserId = userId,
                FirstName = "Jane",
                LastName = "Doe",
                Gender = Gender.Female
            }
        };

        _context.Persons.AddRange(_persons);
        _context.SaveChangesAsync().Wait();
    }

    [Test]
    public async Task Handle_ShouldReturnAllPersons_WhenNoUserIdProvided()
    {
        // Arrange
        var query = new GetPersons.Query();
        var handler = new GetPersons.Handler(_context);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task Handle_ShouldReturnFilteredPersons_WhenUserIdProvided()
    {
        // Arrange
        var userId = _persons.First().UserId;
        var query = new GetPersons.Query { UserId = userId };
        var handler = new GetPersons.Handler(_context);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.All(p => p.UserId == userId), Is.True);
    }
}
