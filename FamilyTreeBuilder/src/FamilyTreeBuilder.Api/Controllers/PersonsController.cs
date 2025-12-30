// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Api.Features.Persons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonDto>>> GetPersons([FromQuery] Guid? userId)
    {
        var query = new GetPersons.Query { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetPersonById(Guid id)
    {
        var query = new GetPersonById.Query { PersonId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PersonDto>> CreatePerson([FromBody] CreatePerson.Command command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPersonById), new { id = result.PersonId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PersonDto>> UpdatePerson(Guid id, [FromBody] UpdatePerson.Command command)
    {
        command.PersonId = id;
        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePerson(Guid id)
    {
        var command = new DeletePerson.Command { PersonId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
